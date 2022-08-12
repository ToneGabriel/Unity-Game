using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class GameplayData
{
    public GameObject GameplayCanvas;            // Reference to gameplay UI canvas
    public GameObject PausePrompt;

    public Button PauseResumeGameButton;
    public Button PauseSaveGameButton;
    public Button PauseLoadGameButton;
    public Button PauseQuitGameButton;
}
