using UnityEngine;

[DefaultExecutionOrder(-100)] // Run very early
public class InputConflictResolver : MonoBehaviour
{
    [Header("Conflict Resolution")]
    public bool disableCameraControls = true;
    public bool logConflicts = true;
    public bool disableCamController = true; // Specifically target CamController
    
    void Awake()
    {
        if (disableCameraControls)
        {
            DisableConflictingCameraControls();
        }
    }
    
    void DisableConflictingCameraControls()
    {
        // Find all cameras in the scene
        Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        
        foreach (Camera cam in cameras)
        {
            // Look for camera control scripts that might use A/D keys
            MonoBehaviour[] scripts = cam.GetComponents<MonoBehaviour>();
            
            foreach (MonoBehaviour script in scripts)
            {
                string scriptName = script.GetType().Name.ToLower();
                
                // Disable common camera control scripts that might conflict
                if (scriptName.Contains("camera") && 
                    (scriptName.Contains("control") || scriptName.Contains("movement") || scriptName.Contains("input")))
                {
                    if (logConflicts)
                    {
                        Debug.Log($"Disabling potential conflicting camera script: {script.GetType().Name} on {cam.name}");
                    }
                    
                    script.enabled = false;
                }
                
                // Specifically target CamController
                if (disableCamController && scriptName.Contains("camcontroller"))
                {
                    if (logConflicts)
                    {
                        Debug.Log($"Disabling CamController script on {cam.name}");
                    }
                    
                    script.enabled = false;
                }
            }
        }
        
        // Also check for any scripts on empty GameObjects that might handle camera input
        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        
        foreach (MonoBehaviour script in allScripts)
        {
            string scriptName = script.GetType().Name.ToLower();
            
            // Look for scripts that might handle camera or input
            if ((scriptName.Contains("camera") || scriptName.Contains("input")) && 
                scriptName.Contains("control"))
            {
                if (logConflicts)
                {
                    Debug.Log($"Found potential input conflict script: {script.GetType().Name} on {script.gameObject.name}");
                }
            }
            
            // Specifically target CamController anywhere in the scene
            if (disableCamController && scriptName.Contains("camcontroller"))
            {
                if (logConflicts)
                {
                    Debug.Log($"Disabling CamController script on {script.gameObject.name}");
                }
                
                script.enabled = false;
            }
        }
        
        Debug.Log("Input conflict resolution completed");
    }
    
    void Start()
    {
        // Double-check for any remaining conflicts
        CheckForRemainingConflicts();
    }
    
    void CheckForRemainingConflicts()
    {
        // Check if there are any scripts using A/D keys
        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        
        foreach (MonoBehaviour script in allScripts)
        {
            // Skip our own scripts
            if (script.GetType() == typeof(InputConflictResolver) || 
                script.GetType() == typeof(PlayerMovement))
            {
                continue;
            }
            
            // Look for any scripts that might be using keyboard input
            string scriptName = script.GetType().Name.ToLower();
            if (scriptName.Contains("input") || scriptName.Contains("keyboard") || scriptName.Contains("key"))
            {
                Debug.Log($"Found potential input script: {script.GetType().Name} on {script.gameObject.name}");
            }
            
            // Check for CamController specifically
            if (scriptName.Contains("camcontroller"))
            {
                Debug.Log($"Found CamController: {script.GetType().Name} on {script.gameObject.name} - Enabled: {script.enabled}");
            }
        }
    }
} 