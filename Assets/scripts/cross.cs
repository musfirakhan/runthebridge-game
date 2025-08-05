using UnityEngine;
using UnityEngine.SceneManagement;

public class cross : MonoBehaviour
{
    // Function called when cross button is pressed - goes to scene 0
    public void CrossButton()
    {
        SceneManager.LoadScene(0);
    }
}
