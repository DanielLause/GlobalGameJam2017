using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : UnitySingleton<SceneController>
{
    public delegate void SceneChanged(SceneType sceneType);
    public event SceneChanged OnSceneChanged;

    private SceneType activeSceneType;
    private Scene activeScene;
    private SceneCollection sceneCollection;

    void Awake()
    {
        sceneCollection = SceneCollection.Instance;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        activeSceneType = sceneCollection.GetSceneType(sceneName);
        activeScene = SceneManager.GetSceneByName(sceneName);

        if (OnSceneChanged != null)
            OnSceneChanged(activeSceneType);
    }

    public void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
        activeSceneType = sceneCollection.GetSceneType(sceneName);
        activeScene = SceneManager.GetSceneByName(sceneName);

        if (OnSceneChanged != null)
            OnSceneChanged( activeSceneType);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
        activeSceneType = sceneCollection.GetSceneType(sceneBuildIndex);
        activeScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);

        if (OnSceneChanged != null)
            OnSceneChanged(activeSceneType);
    }

    public void LoadScene(int sceneBuildIndex, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneBuildIndex, mode);
        activeSceneType = sceneCollection.GetSceneType(sceneBuildIndex);
        activeScene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);

        if (OnSceneChanged != null)
            OnSceneChanged(activeSceneType);
    }

    /// <summary>
    /// Load first scene with type in SceneCollection
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(SceneType sceneType)
    {
        string scene = sceneCollection.GetFirtSceneNameWithType(sceneType);
        SceneManager.LoadScene(scene);
        activeScene = SceneManager.GetSceneByName( scene);
        activeSceneType = sceneCollection.GetSceneType(scene);

        if (OnSceneChanged != null)
            OnSceneChanged(activeSceneType);


    }

    public Scene GetActiveScene()
    {
        return activeScene;
    }

    public SceneType GetActiveSceneType()
    {
        return activeSceneType;
    }
}
