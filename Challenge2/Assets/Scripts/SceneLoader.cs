using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] ScenesToLoad;
    public string[] ScenesToUnload;

    public void LoadScenes()
    {
        foreach(string sceneName in ScenesToUnload)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }

        foreach(string sceneName in ScenesToLoad)
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
        if(ScenesToLoad.Length > 0 || ScenesToUnload.Length > 0)
        {
            LoadScenes();
        }
    }

    public void LoadSingleSceneImmediate(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
