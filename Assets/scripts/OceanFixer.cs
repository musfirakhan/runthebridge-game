using UnityEngine;

public class OceanFixer : MonoBehaviour
{
    void Start()
    {
        // Force ocean system to reinitialize
        var oceanRenderer = FindObjectOfType<Crest.OceanRenderer>();
        if (oceanRenderer != null)
        {
            oceanRenderer.enabled = false;
            oceanRenderer.enabled = true;
            Debug.Log("Ocean Renderer reinitialized");
        }
        
        // Force all ocean-related components to refresh
        var oceanComponents = FindObjectsOfType<MonoBehaviour>();
        foreach (var comp in oceanComponents)
        {
            if (comp.GetType().Namespace != null && comp.GetType().Namespace.Contains("Crest"))
            {
                comp.enabled = false;
                comp.enabled = true;
                Debug.Log($"Reinitialized Crest component: {comp.GetType().Name}");
            }
        }
        
        // Force time to ensure animation is running
        Time.timeScale = 1f;
        Debug.Log("Ocean system reinitialization complete");
    }
} 