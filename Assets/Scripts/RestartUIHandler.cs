using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUIHandler : MonoBehaviour
{
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
