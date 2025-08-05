using UnityEngine;
using UnityEngine.SceneManagement;

public class audio : MonoBehaviour
{
    private AudioSource audioSource;
    
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource != null)
        {
            // Check if we're in scene 1 (index 1)
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                audioSource.Play();
                Debug.Log("Audio started playing in scene 1");
            }
            else
            {
                // Start playing the audio when scene loads (for other scenes)
                audioSource.Play();
                Debug.Log("Audio started playing");
            }
        }
        else
        {
            Debug.LogError("AudioSource component not found!");
        }
    }
    
    void OnDestroy()
    {
        // Stop audio when the GameObject is destroyed (scene ends)
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped - scene ending");
        }
    }
    
    // Optional: Method to manually stop audio
    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped manually");
        }
    }
    
    // Optional: Method to manually start audio
    public void StartAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Audio started manually");
        }
    }
}
