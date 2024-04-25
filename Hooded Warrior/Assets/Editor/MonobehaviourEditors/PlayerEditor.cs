using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private string[] privateMethodNames;
    private int selectedMethodIndex1 = 0;
    private int selectedMethodIndex2 = 0;

    private void OnEnable()
    {
        // Get all private methods of TestClass
        MethodInfo[] methods = typeof(TestClass).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

        // Filter out methods that have no parameters
        methods = Array.FindAll(methods, m => m.GetParameters().Length == 0);

        // Extract method names
        privateMethodNames = new string[methods.Length];
        for (int i = 0; i < methods.Length; i++)
        {
            privateMethodNames[i] = methods[i].Name;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Access the Player instance
        Player player = (Player)target;

        // Draw the dropdowns side by side
        EditorGUILayout.LabelField("Private Methods", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dropdown 1", GUILayout.Width(100));
        selectedMethodIndex1 = EditorGUILayout.Popup(selectedMethodIndex1, privateMethodNames);
        EditorGUILayout.LabelField("Dropdown 2", GUILayout.Width(100));
        selectedMethodIndex2 = EditorGUILayout.Popup(selectedMethodIndex2, privateMethodNames);
        GUILayout.EndHorizontal();




        //// Draw the first dropdown for private methods
        //EditorGUILayout.LabelField("Private Methods 1", EditorStyles.boldLabel);
        //EditorGUI.indentLevel++;
        //selectedMethodIndex1 = EditorGUILayout.Popup(selectedMethodIndex1, privateMethodNames);
        //EditorGUI.indentLevel--;

        //// Draw the second dropdown for private methods
        //EditorGUILayout.LabelField("Private Methods 2", EditorStyles.boldLabel);
        //EditorGUI.indentLevel++;
        //selectedMethodIndex2 = EditorGUILayout.Popup(selectedMethodIndex2, privateMethodNames);
        //EditorGUI.indentLevel--;











        //// Assign the selected private method to Player's func variables when a button is clicked
        //if (GUILayout.Button("Assign Selected Methods"))
        //{
        //    MethodInfo selectedMethod1 = typeof(TestClass).GetMethod(privateMethodNames[selectedMethodIndex1], BindingFlags.Instance | BindingFlags.NonPublic);
        //    MethodInfo selectedMethod2 = typeof(TestClass).GetMethod(privateMethodNames[selectedMethodIndex2], BindingFlags.Instance | BindingFlags.NonPublic);

        //    if (selectedMethod1 != null)
        //    {
        //        player.selectedMethod1 = Delegate.CreateDelegate(typeof(Func<object>), player.testClass, selectedMethod1) as Func<object>;
        //    }

        //    if (selectedMethod2 != null)
        //    {
        //        player.selectedMethod2 = Delegate.CreateDelegate(typeof(Func<object>), player.testClass, selectedMethod2) as Func<object>;
        //    }
        //}
    }
}




[Serializable]
public class TestClass
{
    public int a;

    private void do_something()
    {

    }
}
