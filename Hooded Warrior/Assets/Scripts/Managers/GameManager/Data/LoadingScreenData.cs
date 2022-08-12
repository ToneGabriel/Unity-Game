using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class LoadingScreenData
{
    public GameObject LoaderCanvas;
    public Image ProgressBar;
    public SceneLoader[] SceneLoaders;            // Array of scene loaders by trigger enter
}