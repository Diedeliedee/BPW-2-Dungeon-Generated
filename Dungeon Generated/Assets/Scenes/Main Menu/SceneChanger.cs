using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
