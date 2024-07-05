using UnityEngine;

// This behaviour contains a state machine that is unavailable to extern modifications.
// Other states linked to this FSM can only use ChangeState() method...
// ...to simply change the current state, but the actual state change is implemented by each derived class.
public abstract class MonoBehaviourController : MonoBehaviour
{
    private State _controlState = null;                 // actual FSM returned from GetControlState()

    #region Control Interface
    protected abstract State GetControlState();         // returns a FSM created in derived class
    protected abstract bool UpdateConditions();         // control update execution
    protected abstract bool FixedUpdateConditions();    // control fixedupdate execution
    public abstract void ChangeState();
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
