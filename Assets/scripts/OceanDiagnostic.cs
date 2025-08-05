using UnityEngine;
using System.Collections;

public class OceanDiagnostic : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DiagnoseOcean());
    }

    IEnumerator DiagnoseOcean()
    {
        yield return new WaitForSeconds(1f); // Wait a bit for everything to initialize
        
        Debug.Log("=== OCEAN DIAGNOSTIC START ===");
        
        // Check if we're in play mode
        Debug.Log($"In Play Mode: {Application.isPlaying}");
        
        // Check time scale
        Debug.Log($"Time Scale: {Time.timeScale}");
        
        // Find OceanRenderer
        var oceanRenderer = FindObjectOfType<Crest.OceanRenderer>();
        if (oceanRenderer != null)
        {
            Debug.Log($"OceanRenderer found: {oceanRenderer.name}");
            Debug.Log($"OceanRenderer enabled: {oceanRenderer.enabled}");
            Debug.Log($"OceanRenderer active: {oceanRenderer.gameObject.activeInHierarchy}");
        }
        else
        {
            Debug.LogError("No OceanRenderer found in scene!");
        }
        
        // Find all Crest components
        var allComponents = FindObjectsOfType<MonoBehaviour>();
        int crestCount = 0;
        foreach (var comp in allComponents)
        {
            if (comp.GetType().Namespace != null && comp.GetType().Namespace.Contains("Crest"))
            {
                crestCount++;
                Debug.Log($"Crest component: {comp.GetType().Name} on {comp.gameObject.name} - Enabled: {comp.enabled}");
            }
        }
        Debug.Log($"Total Crest components found: {crestCount}");
        
        // Check for ocean materials
        var oceanMaterials = FindObjectsOfType<Renderer>();
        foreach (var renderer in oceanMaterials)
        {
            if (renderer.material != null && renderer.material.shader != null)
            {
                if (renderer.material.shader.name.Contains("Crest") || renderer.material.shader.name.Contains("Ocean"))
                {
                    Debug.Log($"Ocean material found: {renderer.material.name} on {renderer.gameObject.name}");
                    Debug.Log($"Shader: {renderer.material.shader.name}");
                }
            }
        }
        
        Debug.Log("=== OCEAN DIAGNOSTIC END ===");
        
        // Try to force reinitialize
        if (oceanRenderer != null)
        {
            Debug.Log("Attempting to force reinitialize ocean...");
            oceanRenderer.enabled = false;
            yield return new WaitForEndOfFrame();
            oceanRenderer.enabled = true;
            Debug.Log("Ocean reinitialization attempted");
        }
    }
} 