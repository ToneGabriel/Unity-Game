using UnityEditor;
using UnityEngine;


public class SceneEnumGenerator : Editor
{
    [MenuItem("EditorHelper/Update Scene Enum")]
    public static void UpdateAnimatorParameters()
    {
        // Generate the new content for SceneNames enum
        string newEnumName  = "SceneNames";
        string newContent   = "public enum " + newEnumName + "\n{\n";

        foreach (var scene in EditorBuildSettings.scenes)
            newContent += $"    {System.IO.Path.GetFileNameWithoutExtension(scene.path)},\n";

        newContent += "}\n";

        Helpers.GenerateFile(newEnumName, newContent);
    }
}
