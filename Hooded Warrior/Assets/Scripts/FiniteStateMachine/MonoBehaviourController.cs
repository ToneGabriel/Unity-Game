using UnityEngine;

// Template pattern
public abstract class MonoBehaviourController : MonoBehaviour
{
    private State _controlState = null;                 // actual FSM returned from GetControlState()

    #region Control Interface
    protected abstract State GetControlState();         // returns a FSM created in derived class
    protected abstract bool UpdateConditions();         // control update execution
    protected abstract bool FixedUpdateConditions();    // control fixedupdate execution
    public abstract void ChangeState();                 // control the general flow of the states (the ones inside FSM _controlState)
    #endregion Control Interface

    #region Unity Functions
    protected virtual void Awake() { /*Empty*/ }

    protected virtual void OnEnable()
    {
        if (_controlState == null)
            _controlState = GetControlState();

        _controlState.Enter();
    }

    protected virtual void Start() { /*Empty*/ }

    protected virtual void Update()
    {
        if (!UpdateConditions())
            return;

        _controlState.Update();
    }

    protected virtual void FixedUpdate()
    {
        if (!FixedUpdateConditions())
            return;

        _controlState.FixedUpdate();
    }

    protected virtual void OnDisable()
    {
        _controlState.Exit();
    }
    #endregion Unity Functions
}
