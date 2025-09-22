using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandlerService : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}