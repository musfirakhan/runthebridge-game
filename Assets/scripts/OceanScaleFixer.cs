using UnityEngine;

[DefaultExecutionOrder(-100)] // Run before other scripts
public class OceanScaleFixer : MonoBehaviour
{
    [Header("Auto Fix Settings")]
    public bool autoFixOnStart = true;
    public bool fixDepthCacheScale = true;
    public bool fixAllOceanScales = true;
    
    [Header("Scale Settings")]
    public float targetYScale = 1.0f;
    public float tolerance = 0.1f; // How much deviation is allowed
    
    void Awake()
    {
        // Fix immediately in Awake to prevent validation errors
        FixOceanScales();
    }
    
    void Start()
    {
        if (autoFixOnStart)
        {
            // Double-check and fix again in Start
            FixOceanScales();
        }
    }
    
    [ContextMenu("Fix Ocean Scales")]
    public void FixOceanScales()
    {
        Debug.Log("=== OCEAN SCALE FIXER START ===");
        
        if (fixDepthCacheScale)
        {
            FixDepthCacheScale();
        }
        
        if (fixAllOceanScales)
        {
            FixAllOceanObjectScales();
        }
        
        Debug.Log("=== OCEAN SCALE FIXER COMPLETE ===");
    }
    
    void FixDepthCacheScale()
    {
        // Find all objects with OceanDepthCache component
        var depthCaches = FindObjectsByType<Crest.OceanDepthCache>(FindObjectsSortMode.None);
        
        foreach (var depthCache in depthCaches)
        {
            Transform cacheTransform = depthCache.transform;
            Vector3 currentScale = cacheTransform.localScale;
            
            // Check if Y scale needs fixing
            if (Mathf.Abs(currentScale.y - targetYScale) > tolerance)
            {
                Debug.Log($"Fixing OceanDepthCache scale on {depthCache.name}");
                Debug.Log($"Old scale: {currentScale} -> New scale: ({currentScale.x}, {targetYScale}, {currentScale.z})");
                
                // Fix the Y scale while preserving X and Z
                cacheTransform.localScale = new Vector3(currentScale.x, targetYScale, currentScale.z);
            }
            else
            {
                Debug.Log($"OceanDepthCache {depthCache.name} scale is already correct: {currentScale}");
            }
        }
        
        if (depthCaches.Length == 0)
        {
            Debug.Log("No OceanDepthCache components found");
        }
    }
    
    void FixAllOceanObjectScales()
    {
        // Find all objects with "Ocean" in their name or with Crest components
        var allObjects = FindObjectsByType<Transform>(FindObjectsSortMode.None);
        
        foreach (var obj in allObjects)
        {
            // Check if this is an ocean-related object
            bool isOceanObject = false;
            
            // Check name
            if (obj.name.ToLower().Contains("ocean") || obj.name.ToLower().Contains("crest"))
            {
                isOceanObject = true;
            }
            
            // Check for Crest components
            var crestComponents = obj.GetComponents<MonoBehaviour>();
            foreach (var comp in crestComponents)
            {
                if (comp.GetType().Namespace != null && comp.GetType().Namespace.Contains("Crest"))
                {
                    isOceanObject = true;
                    break;
                }
            }
            
            if (isOceanObject)
            {
                Vector3 currentScale = obj.localScale;
                
                // Fix Y scale if it's significantly different from 1.0
                if (Mathf.Abs(currentScale.y - 1.0f) > tolerance)
                {
                    Debug.Log($"Fixing ocean object scale: {obj.name}");
                    Debug.Log($"Old scale: {currentScale} -> New scale: ({currentScale.x}, 1.0, {currentScale.z})");
                    
                    obj.localScale = new Vector3(currentScale.x, 1.0f, currentScale.z);
                }
            }
        }
    }
    
    // Public method to fix specific object
    public void FixSpecificObject(Transform targetObject)
    {
        if (targetObject != null)
        {
            Vector3 currentScale = targetObject.localScale;
            targetObject.localScale = new Vector3(currentScale.x, targetYScale, currentScale.z);
            Debug.Log($"Fixed scale for {targetObject.name}: {currentScale} -> {targetObject.localScale}");
        }
    }
    
    // Public method to validate all ocean objects
    public void ValidateOceanObjects()
    {
        Debug.Log("=== OCEAN OBJECT VALIDATION ===");
        
        var depthCaches = FindObjectsByType<Crest.OceanDepthCache>(FindObjectsSortMode.None);
        Debug.Log($"Found {depthCaches.Length} OceanDepthCache components");
        
        foreach (var depthCache in depthCaches)
        {
            Vector3 scale = depthCache.transform.localScale;
            Debug.Log($"{depthCache.name}: Scale = {scale}, Y scale correct: {Mathf.Abs(scale.y - targetYScale) <= tolerance}");
        }
        
        Debug.Log("=== VALIDATION COMPLETE ===");
    }
} 