using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum SceneType {None, Menu, Game };

[Serializable]
public class SceneData  {

    public UnityEngine.Object Scene;
    public SceneType Type;
}
