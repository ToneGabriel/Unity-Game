using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class StateBehaviour : MonoBehaviour
{
    //public StateMB[] States { get { return GetComponentsInChildren<StateMB>(); } }
    //public Func<bool>[] Conditions
    //{
    //    get
    //    {
    //        var ch = GetComponentInChildren<ConditionHolder>();
    //        MethodInfo[] methods = ch.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
    //        Func<bool>[] ret = new Func<bool>[methods.Length];

    //        for (int i = 0; i < ret.Length; ++i)
    //            ret[i] = Delegate.CreateDelegate(typeof(Func<bool>), ch, methods[i]) as Func<bool>;

    //        return ret;
    //    }
    //}

    //public string[] StateNames
    //{
    //    get
    //    {
    //        var states      = States;
    //        string[] ret    = new string[states.Length];

    //        for (int i = 0; i < ret.Length; ++i)
    //            ret[i] = states[i].GetType().Name;

    //        return ret;
    //    }
    //}

    //public string[] MethodNames
    //{
    //    get
    //    {
    //        List<string> methodList = new List<string>();
    //        MethodInfo[] methods    = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
    //        foreach (MethodInfo method in methods)
    //        {
    //            // check attribute == TransitionAttribute, return type == bool, no parameters
    //            if (Attribute.GetCustomAttribute(method, typeof(ConditionAttribute)) is ConditionAttribute)
    //                if (method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
    //                    methodList.Add(method.Name);
    //                else
    //                    throw new MethodAccessException("Invalid signature for ConditionAttribute Method!");
    //        }

    //        return methodList.ToArray();
    //    }
    //}

    //public Func<bool> GetConditionMethod(string methodName)
    //{
    //    MethodInfo methodInfo = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
    //    return Delegate.CreateDelegate(typeof(Func<bool>), this, methodInfo) as Func<bool>;
    //}
}
