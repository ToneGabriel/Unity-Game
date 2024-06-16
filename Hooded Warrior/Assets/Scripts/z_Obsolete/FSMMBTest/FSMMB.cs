using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;



public class FSMMB : MonoBehaviour
{
    #region Editor Parameters
    [Serializable]
    public struct EditorTransition
    {
        public string NextState;
        public string Condition;
    }

    [Serializable]
    public struct EditorTransitionHolder
    {
        [ReadOnlyField]
        public string FromState;
        public EditorTransition[] Transitions;
    }

    [SerializeField][ReadOnlyField]
    private StateBehaviour      _target;

    [SerializeField][ReadOnlyField]
    private StateMB[]           _states;

    [SerializeField][ReadOnlyField]
    private ConditionHolder     _conditionHolder;

    [SerializeField]
    private EditorTransitionHolder[]  _transitionHolders;
    #endregion Editor Parameters

    #region Logic Parameters
    private Dictionary<string, StateMB>                     _stateMap;
    private Dictionary<string, Func<bool>>                  _conditionMap;
    private Dictionary<string, Dictionary<string, string>>  _transitionMap;

    private string                                          _currentStateName;
    private Dictionary<string, string>                      _currentStateTransitions = null;

    public string[] StateNames { get { return Helpers.GetTypeNames(_states); } }
    public string[] ConditionMethodNames { get { return Helpers.GetMethodNames<Func<bool>>(_conditionHolder, typeof(ConditionAttribute)); } }
    #endregion Logic Parameters


    private void Awake()
    {
        MapStates();
        MapConditions();
        MapTransitions();
    }

    private void OnEnable()
    {
        EnableDefaultState();
    }

    private void Update()
    {
        foreach (var transition in _currentStateTransitions)
            if (CheckCondition(transition.Value))
            {
                ChangeState(transition.Key);
                return;
            }
    }

    public void InitializeEditor()
    {
        // find and set target in components
        _target             = GetComponentInChildren<StateBehaviour>();
        _states             = GetComponentsInChildren<StateMB>();
        _conditionHolder    = GetComponentInChildren<ConditionHolder>();

        foreach (var state in  _states)
            state.Target = _target;
        
        _conditionHolder.Target = _target;

        // generate state holders
        var stateNames = StateNames;
        int len = stateNames.Length;

        if (len > 0)
        {
            _transitionHolders = new EditorTransitionHolder[len];

            for (int i = 0; i < len; i++)
                _transitionHolders[i].FromState = stateNames[i];
        }
        else
            Debug.Log("No states present!");
    }

    private void MapStates()
    {
        _stateMap = new Dictionary<string, StateMB>();

        foreach (var state in _states)
        {
            var key = state.GetType().Name;

            if (!_stateMap.ContainsKey(key))
                _stateMap.Add(key, state);
            else
                throw new ArgumentException($"Duplicate key: {key}");
        }
    }

    private void MapConditions()
    {
        _conditionMap           = new Dictionary<string, Func<bool>>();
        string[] methodNames    = ConditionMethodNames;

        foreach (var name in methodNames)
        {
            if (!_conditionMap.ContainsKey(name))
                _conditionMap.Add(name, GetConditionMethod(name));
            else
                throw new ArgumentException($"Duplicate method: {name}");
        }
    }

    private void MapTransitions()
    {
        _transitionMap = new Dictionary<string, Dictionary<string, string>>();

        foreach (var holder in _transitionHolders)
        {
            var key1 = holder.FromState;

            foreach(var transition in holder.Transitions)
            {
                var key2 = transition.NextState;

                if (key1 == key2)
                    throw new ArgumentException($"Cannot make transition to self! Index: {Array.IndexOf(holder.Transitions, key2)}");

                if (!_transitionMap.ContainsKey(key1)) // no transitions exists from this state
                    _transitionMap.Add(key1, new Dictionary<string, string>());

                if (!_transitionMap[key1].ContainsKey(key2)) // no duplicate transition from -> to
                    _transitionMap[key1].Add(key2, transition.Condition);
                else
                    throw new ArgumentException($"Duplicate transition! Index: {Array.IndexOf(holder.Transitions, key2)}");
            }
        }
    }

    private void EnableDefaultState()
    {
        _currentStateName           = StateNames[0];
        _currentStateTransitions    = _transitionMap[_currentStateName];
        SetCurrentStateActive(true);
    }

    private void ChangeState(string stateName)
    {
        SetCurrentStateActive(false);
        _currentStateName           = stateName;
        _currentStateTransitions    = _transitionMap[_currentStateName];
        SetCurrentStateActive(true);
    }

    private void SetCurrentStateActive(bool value)    // set state and corresponding transitions active
    {
        _stateMap[_currentStateName].gameObject.SetActive(value);
    }

    private bool CheckCondition(string conditionName)
    {
        return _conditionMap[conditionName]();
    }

    private Func<bool> GetConditionMethod(string methodName)
    {
        return Helpers.CreateDelegateFromMethod<Func<bool>>(_conditionHolder, methodName);
    }
}




#region FSMMBEditor

[CustomEditor(typeof(FSMMB))]
public class FSMMBEditor : Editor
{
    private FSMMB _targetRef = null;

    private void OnEnable()
    {
        _targetRef = target as FSMMB;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Initialize FSM"))
        {
            _targetRef.InitializeEditor();
        }
    }
}

#endregion FSMMBEditor


#region EditorTransitionDrawer

// Custom PropertyDrawer for Transition
[CustomPropertyDrawer(typeof(FSMMB.EditorTransition))]
public class EditorTransitionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Prepare data ==========================================================
        // Get the array of strings from FSMMB
        FSMMB thisStateMachine  = property.serializedObject.targetObject as FSMMB;
        string[] stateNames     = thisStateMachine.StateNames;
        string[] methodNames    = thisStateMachine.ConditionMethodNames;

        // Get the properties of Transition ======================================
        SerializedProperty nextStateNameProp = property.FindPropertyRelative("NextState");
        SerializedProperty conditionNameProp = property.FindPropertyRelative("Condition");

        // Draw =================================================================
        // Draw dropdown for NextState
        Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int selectedIndex = Array.IndexOf(stateNames, nextStateNameProp.stringValue);
        if (selectedIndex == -1)
            selectedIndex = 0;
        nextStateNameProp.stringValue = stateNames[EditorGUI.Popup(fieldRect, "Next State", selectedIndex, stateNames)];

        // Draw dropdown for Condition
        fieldRect.y += EditorGUIUtility.singleLineHeight;
        string selectedMethod = conditionNameProp.stringValue;
        selectedIndex = -1;
        for (int i = 0; i < methodNames.Length; i++)
        {
            if (methodNames[i] == selectedMethod)
            {
                selectedIndex = i;
                break;
            }
        }
        selectedIndex = EditorGUI.Popup(fieldRect, "Condition", selectedIndex, methodNames);
        if (selectedIndex >= 0 && selectedIndex < methodNames.Length)
        {
            conditionNameProp.stringValue = methodNames[selectedIndex];
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 3; // Three dropdowns
    }
}

#endregion EditorTransitionDrawer