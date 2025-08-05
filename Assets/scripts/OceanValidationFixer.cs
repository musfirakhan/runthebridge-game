using UnityEngine;

[DefaultExecutionOrder(-200)] // Run even earlier than OceanScaleFixer
public class OceanValidationFixer : MonoBehaviour
{
    [Header("Auto Fix Settings")]
    public bool autoFixOnAwake = true;
    public bool suppressValidationErrors = true;
    
    void Awake()
    {
        if (autoFixOnAwake)
        {
            FixOceanDepthCacheScales();
        }
    }
    
    void Start()
    {
        // Double-check in Start as well
        FixOceanDepthCacheScales();
    }
    
    [ContextMenu("Fix Ocean Depth Cache Scales")]
    public void FixOceanDepthCacheScales()
    {
        Debug.Log("=== OCEAN VALIDATION FIXER START ===");
        
        // Find all OceanDepthCache components
        var depthCaches = FindObjectsByType<Crest.OceanDepthCache>(FindObjectsSortMode.None);
        
        foreach (var depthCache in depthCaches)
        {
            Transform cacheTransform = depthCache.transform;
            Vector3 currentScale = cacheTransform.localScale;
            
            // Check if Y scale is not 1.0 (using the same tolerance as the validation)
            if (!Mathf.Approximately(currentScale.y, 1f))
            {
                Debug.Log($"Fixing OceanDepthCache scale on {depthCache.name}");
                Debug.Log($"Old scale: {currentScale} -> New scale: ({currentScale.x}, 1.0, {currentScale.z})");
                
                // Fix the Y scale while preserving X and Z
                cacheTransform.localScale = new Vector3(currentScale.x, 1.0f, currentScale.z);
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
        
        Debug.Log("=== OCEAN VALIDATION FIXER COMPLETE ===");
    }
    
    // Public method to fix a specific OceanDepthCache
    public void FixSpecificOceanDepthCache(Crest.OceanDepthCache depthCache)
    {
        if (depthCache != null)
        {
            Transform cacheTransform = depthCache.transform;
            Vector3 currentScale = cacheTransform.localScale;
            
            if (!Mathf.Approximately(currentScale.y, 1f))
            {
                cacheTransform.localScale = new Vector3(currentScale.x, 1.0f, currentScale.z);
                Debug.Log($"Fixed scale for {depthCache.name}: {currentScale} -> {cacheTransform.localScale}");
            }
        }
    }
    
    // Public method to validate all OceanDepthCache scales
    public void ValidateOceanDepthCacheScales()
    {
        Debug.Log("=== OCEAN DEPTH CACHE VALIDATION ===");
        
        var depthCaches = FindObjectsByType<Crest.OceanDepthCache>(FindObjectsSortMode.None);
        Debug.Log($"Found {depthCaches.Length} OceanDepthCache components");
        
        bool allValid = true;
        foreach (var depthCache in depthCaches)
        {
            Vector3 scale = depthCache.transform.localScale;
            bool isValid = Mathf.Approximately(scale.y, 1f);
            Debug.Log($"{depthCache.name}: Scale = {scale}, Y scale valid: {isValid}");
            
            if (!isValid)
            {
                allValid = false;
            }
        }
        
        Debug.Log($"All OceanDepthCache scales valid: {allValid}");
        Debug.Log("=== VALIDATION COMPLETE ===");
    }
} 