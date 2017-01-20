using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlackView : MonoBehaviour {

    public CanvasGroup CanvasGroup { get; set; }

    void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

}
