using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Helpers
{
    public static void GenerateFile(string name, string content)
    {
        // Write the new content to .cs file
        // The file is first created if does not exists
        string path = Application.dataPath + $"/Scripts/Generated/" + name + ".cs";
        File.WriteAllText(path, content);
    }

    public static void ChangeTimeScale(TimeScale value)
    {
        Time.timeScale = (int)value / (int)TimeScale.Default;
    }

    public static AsyncOperation LoadScene(SceneNames sceneID)
    {
        return SceneManager.LoadSceneAsync((int)sceneID, LoadSceneMode.Additive);
    }

    public static AsyncOperation UnloadScene(SceneNames sceneID)
    {
        return SceneManager.UnloadSceneAsync((int)sceneID);
    }
}