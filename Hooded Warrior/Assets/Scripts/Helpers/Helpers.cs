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

    public static string[] GetMethodNames<FuncSignature>(object target, params Type[] attributes)
    where FuncSignature : class
    {
        // Return an array of strings with all method names from target object...
        // ...with a certain signature and attributes.
        // FuncSignature is a Func<> delegate.

        if (!attributes.All(type => typeof(Attribute).IsAssignableFrom(type)))
            throw new ArgumentException("Types are not Attribute type!");

        List<string> methodList         = new List<string>();
        MethodInfo delegateInfo         = typeof(FuncSignature).GetMethod("Invoke");
        MethodInfo[] targetMethodInfo   = target.GetType().GetMethods(  BindingFlags.Public |
                                                                        BindingFlags.NonPublic |
                                                                        BindingFlags.Instance);

        // Filter all methods found on target with the "Invoke" method from Func<>...
        // ...and attributes (if applicable)

        if (null == attributes) // no attributes
        {
            foreach (var method in targetMethodInfo)
                if (IsMethodSignatureMatching(method, delegateInfo))
                    methodList.Add(method.Name);
        }
        else
        {
            foreach (var method in targetMethodInfo)
                foreach (var attribute in attributes)
                    if (null != method.GetCustomAttribute(attribute) && IsMethodSignatureMatching(method, delegateInfo))
                        methodList.Add(method.Name);
        }

        return methodList.ToArray();
    }

    public static FuncSignature CreateDelegateFromMethod<FuncSignature>(object target, string methodName, params Type[] types)
    where FuncSignature : class
    {
        // Create delegate from method on target that has name and parameters.
        // FuncSignature is a Func<> delegate.

        MethodInfo delegateInfo     = typeof(FuncSignature).GetMethod("Invoke");
        MethodInfo targetMethodInfo = target.GetType().GetMethod(   methodName,
                                                                    types.Length,
                                                                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                                                                    null,
                                                                    CallingConventions.Standard,
                                                                    types,
                                                                    null);

        // Ensure the method exists
        if (null == targetMethodInfo)
            throw new ArgumentException($"Method '{methodName}' not found on target object.");

        // Check if the method signature matches the delegate type
        if (IsMethodSignatureMatching(targetMethodInfo, delegateInfo))
            throw new ArgumentException($"The method '{methodName}' does not match the signature of delegate type '{typeof(FuncSignature).Name}'.");

        return Delegate.CreateDelegate(typeof(FuncSignature), target, targetMethodInfo) as FuncSignature;
    }

    public static bool IsMethodSignatureMatching(MethodInfo first, MethodInfo second)
    {
        // Compare methods signatures

        // Compare return types
        if (first.ReturnType != second.ReturnType)
            return false;

        // Compare parameter counts
        ParameterInfo[] firstParameters     = first.GetParameters();
        ParameterInfo[] secondParameters    = second.GetParameters();

        if (firstParameters.Length != secondParameters.Length)
            return false;

        // Compare parameter types
        for (int i = 0; i < firstParameters.Length; ++i)
            if (firstParameters[i].ParameterType != secondParameters[i].ParameterType)
                return false;

        // Signatures match
        return true;
    }

    public static ArrayType[] ConcatArrays<ArrayType>(params ArrayType[][] arrays)
    {
        // Combine multiple arrays into a single one.

        if (null == arrays)
            return null;

        ArrayType[] ret = new ArrayType[0];

        foreach (ArrayType[] arr in arrays)
            ret = ret.Concat(arr).ToArray();

        return ret;
    }
}