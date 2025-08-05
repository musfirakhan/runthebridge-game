using UnityEngine;
using UnityEngine.SceneManagement;

public class tryagian : MonoBehaviour
{
    // Function to try again - loads scene 1
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
}
