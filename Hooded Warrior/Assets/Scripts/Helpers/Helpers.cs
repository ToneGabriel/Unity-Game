using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        Time.timeScale = (float)(int)value / (int)TimeScale.Default;
    }

    public static AsyncOperation LoadScene(SceneNames sceneID)
    {
        return SceneManager.LoadSceneAsync((int)sceneID, LoadSceneMode.Additive);
    }

    public static AsyncOperation UnloadScene(SceneNames sceneID)
    {
        return SceneManager.UnloadSceneAsync((int)sceneID);
    }

    public static string[] GetTypeNames(object[] objects)
    {
        string[] ret = new string[objects.Length];

        for (int i = 0; i < ret.Length; ++i)
            ret[i] = objects[i].GetType().Name;

        return ret;
    }

    public static string[] GetMethodNames<FuncSignature>(object target, params Type[] attributes)  // FuncSignature is a Func<> delegate
    where FuncSignature : class
    {
        if (!attributes.All(type => typeof(Attribute).IsAssignableFrom(type)))
            throw new ArgumentException("Types are not Attribute type!");

        List<string> methodList = new List<string>();
        MethodInfo funcMethod = typeof(FuncSignature).GetMethod("Invoke");
        MethodInfo[] methods = target.GetType().GetMethods(BindingFlags.Public |
                                                                BindingFlags.NonPublic |
                                                                BindingFlags.Instance);

        if (attributes != null)
        {
            foreach (var method in methods)
                foreach (var attribute in attributes)
                    if (method.GetCustomAttribute(attribute) != null &&
                        IsMethodSignatureMatching(method, funcMethod))
                        methodList.Add(method.Name);
        }
        else // no attributes passed
        {
            foreach (var method in methods)
                if (IsMethodSignatureMatching(method, funcMethod))
                    methodList.Add(method.Name);
        }

        return methodList.ToArray();
    }

    public static FuncSignature CreateDelegateFromMethod<FuncSignature>(object target, string methodName, params Type[] types)
    where FuncSignature : class
    {
        MethodInfo delegateInfo = typeof(FuncSignature).GetMethod("Invoke");
        MethodInfo targetMethodInfo = target.GetType().GetMethod(methodName,
                                                                    types.Length,
                                                                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                                                                    null,
                                                                    CallingConventions.Standard,
                                                                    types,
                                                                    null);

        // Ensure the method exists
        if (targetMethodInfo == null)
            throw new ArgumentException($"Method '{methodName}' not found on target object.");

        // Check if the method signature matches the delegate type
        if (IsMethodSignatureMatching(targetMethodInfo, delegateInfo))
            throw new ArgumentException($"The method '{methodName}' does not match the signature of delegate type '{typeof(FuncSignature).Name}'.");

        return Delegate.CreateDelegate(typeof(FuncSignature), target, targetMethodInfo) as FuncSignature;
    }

    // Method to compare method signature with delegate signature
    public static bool IsMethodSignatureMatching(MethodInfo first, MethodInfo second)
    {
        // Compare return types
        if (first.ReturnType != second.ReturnType)
            return false;

        // Compare parameter counts
        ParameterInfo[] firstParameters = first.GetParameters();
        ParameterInfo[] secondParameters = second.GetParameters();

        if (firstParameters.Length != secondParameters.Length)
            return false;

        // Compare parameter types
        for (int i = 0; i < firstParameters.Length; ++i)
            if (firstParameters[i].ParameterType != secondParameters[i].ParameterType)
                return false;

        // Signatures match
        return true;
    }
}