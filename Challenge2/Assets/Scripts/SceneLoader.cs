using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] SceneToLoad;
    public string[] SceneToUnLoad;
    public void LoadScenes()
    {
        foreach (string sceneName in SceneToUnLoad)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
               
            }
        }
        foreach (string sceneName in SceneToLoad)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.isLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (SceneToLoad.Length >0 || SceneToUnLoad.Length >0)
        {
            LoadScenes();
        }
    }
    public void LoadSingleSceneImmediate(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
