using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneCollection : UnitySingleton<SceneCollection>
{

    public List<SceneData> Scenes;

    public SceneType GetSceneType(string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        return Scenes.Find(x => scene.name == x.Scene.name).Type;
    }

    public SceneType GetSceneType(int sceneBuildIndex)
    {
        var scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
        return Scenes.Find(x => scene.name == x.Scene.name).Type;
    }

    public string GetFirtSceneNameWithType(SceneType sceneType)
    {
        string t = "";
        try
        {
             t = Scenes.Find(x => sceneType == x.Type).Scene.name;
        }
        catch (NullReferenceException)
        {

            Debug.LogError(string.Format("Theres no scene with type {0} in SceneCollection",sceneType));
        }

        return t;
    }

}
