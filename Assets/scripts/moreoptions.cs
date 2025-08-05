using UnityEngine;
using UnityEngine.SceneManagement;

public class moreoptions : MonoBehaviour
{
    // Method to go back to the first scene (0th scene)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
