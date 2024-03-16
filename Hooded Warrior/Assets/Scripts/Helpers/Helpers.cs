using System.IO;
using UnityEditor;
using UnityEngine;


public static class Helpers
{
    public static void GenerateFile(string name, string content)
    {
        // Write the new content to .cs file
        // The file is first created if does not exists
        string path = Application.dataPath + $"/Scripts/Generated/" + name + ".cs";
        File.WriteAllText(path, content);
    }
}
