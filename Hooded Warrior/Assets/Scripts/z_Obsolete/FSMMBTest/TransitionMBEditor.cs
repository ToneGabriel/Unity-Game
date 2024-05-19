using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;




[CustomEditor(typeof(TransitionMB), true)]
public class TransitionMBEditor : Editor
{
    private TransitionMB _targetRef     = null;
    private string[] _methodNames       = null;
    private int _selectedMethodIndex    = 0;


    private void OnEnable()
    {
        _targetRef = target as TransitionMB;

        GetMethodNames();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MakeTransitionConditionsPopup();
    }

    private void GetMethodNames()
    {
        //var transitionTarget = _targetRef.TargetObject;
        //if (transitionTarget != null)
        //{
        //    List<string> methodList = new List<string>();
        //    MethodInfo[] methods = transitionTarget.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

        //    foreach (MethodInfo method in methods)
        //    {
        //        // check attribute == TransitionAttribute, return type == bool, no parameters
        //        TransitionAttribute attribute = (TransitionAttribute)Attribute.GetCustomAttribute(method, typeof(TransitionAttribute));
        //        if (attribute != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
        //            methodList.Add(method.Name);
        //    }

        //    _methodNames = methodList.ToArray();
        //}
    }

    private void MakeTransitionConditionsPopup()
    {
        if (_methodNames == null)
            EditorGUILayout.LabelField("No target selected.");
        else if (_methodNames.Length == 0)
            EditorGUILayout.LabelField("No private transition methods found.");
        else
        {
            var transitionTarget        = _targetRef.TargetObject;
            int prevSelectedMethodIndex = _selectedMethodIndex; // Store previous selection
            _selectedMethodIndex = EditorGUILayout.Popup("Transition Method", _selectedMethodIndex, _methodNames);

            if (_selectedMethodIndex != prevSelectedMethodIndex) // Selected index has changed, do something if needed
            {
                // Get the MethodInfo of the selected method
                MethodInfo methodInfo = transitionTarget.GetType().GetMethod(_methodNames[_selectedMethodIndex], BindingFlags.NonPublic | BindingFlags.Instance);

                // Create a delegate of type Func<bool> from the method
                // Assign the delegate to the Condition property
                _targetRef.Condition = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), transitionTarget, methodInfo);
            }
        }
    }
}