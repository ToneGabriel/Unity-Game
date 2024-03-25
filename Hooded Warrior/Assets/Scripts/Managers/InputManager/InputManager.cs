using UnityEngine;
using UnityEngine.InputSystem;


#region Action Type Button Def
//Interaction None
//started, performed on button down | canceled on release

//Interaction Press And Release
//started, performed on button down | started, performed again on button up

//Interaction Press Only
//started, performed on button down

//Interaction Release Only
//started on button down | performed on button up

//Interaction Hold
//started on button down | performed once the button is held long enough | canceled when the button is released too early

//Interaction Multi Tap
//started on button down | canceled if button not pressed fast enough soon enough | performed if button pressed enough times fast enough
//note: multitap doesn't work

//Interaction Slow Tap
//started on button down | canceled if you release before min tap duration | performed as soon as you release if the alotted min tap duration has elapsed (happens when you release the button not when the time has elapsed)

//Interaction Tap
//started on button down | performed if you release before the Max Tap Duration | canceled if you hold down past the Max Tap Duration as soon as the duration passes(no event on button up)
#endregion Action Type Button Def


public sealed class InputManager : MonoBehaviour
{
    #region Components
    public static InputManager Instance { get; private set; }

    private PlayerInput _playerInput;
    #endregion Components

    #region Inputs
    public int      NormalizedInputX        { get; private set; }
    public int      NormalizedInputY        { get; private set; }

    public bool     JumpInput               { get; private set; }
    public bool     RollInput               { get; private set; }
    public bool     GrabInput               { get; private set; }
    public bool     DashInput               { get; private set; }
    public Vector2  DashDirectionInput      { get; private set; }

    public bool     PrimaryAttackInput      { get; private set; }
    public bool     SecondaryDefendInput    { get; private set; }
    public bool     SpellCastInput          { get; private set; }
    public bool     ChangeWeaponInput       { get; private set; }
    public bool     ChangeSpellInput        { get; private set; }
    public bool     PauseGameInput          { get; private set; }
    #endregion Inputs

    #region Helper Inputs
    private Vector2 _rawMovementInput;
    private Vector2 _rawDashDirectionInput;
    #endregion Helper Inputs

    #region Unity Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;

            _playerInput = GetComponent<PlayerInput>();
        }
    }
    #endregion Unity Functions

    #region OnInput Functions
    public void OnMoveInput(InputAction.CallbackContext context)        // Vector2 Composite
    {
        _rawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = (int)(_rawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(_rawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)        // Hold
    {
        if (context.started)
            JumpInput = true;
        else if (context.performed || context.canceled)
            JumpInput = false;
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }

    public void OnGrabInput(InputAction.CallbackContext context)        // Normal Button (can hold without restriction)
    {
        if (context.started)
            GrabInput = true;
        else if (context.canceled)
            GrabInput = false;
    }

    public void OnDashInput(InputAction.CallbackContext context)        // Hold
    {
        if (context.started)
            DashInput = true;
        else if (context.performed || context.canceled)
            DashInput = false;
    }

    public void UseDashInput()
    {
        DashInput = false;
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)   // Vector2 Composite
    {
        _rawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard")
            DashDirectionInput = GameManager.Instance.MainCamera.ScreenToWorldPoint(_rawDashDirectionInput) - transform.position;
    }

    public void OnRollInput(InputAction.CallbackContext context)        // Tap
    {
        if (context.started)
            RollInput = true;
        else if (context.performed || context.canceled)
            RollInput = false;
    }

    public void UseRollInput()
    {
        RollInput = false;
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)   // TODO
    {
        if (context.started)
            PrimaryAttackInput = true;
        if (context.canceled)
            PrimaryAttackInput = false;
    }

    public void OnSecondaryDefendInput(InputAction.CallbackContext context) // TODO
    {
        if (context.started)
            SecondaryDefendInput = true;
        if (context.canceled)
            SecondaryDefendInput = false;
    }

    public void OnSpellCastInput(InputAction.CallbackContext context)       // Normal Press Button - use ForceInputStop() to cancel input read early
    {
        if (context.started)
            SpellCastInput = true;
        else if (context.canceled)
            SpellCastInput = false;
    }

    public void OnChangeWeaponInput(InputAction.CallbackContext context)    // TODO
    {
        if (context.started)
            ChangeWeaponInput = true;
    }

    public void UseChangeWeaponInput()
    {
        ChangeWeaponInput = false;
    }

    public void OnChangeSpellInput(InputAction.CallbackContext context)     // TODO
    {
        if (context.started)
            ChangeSpellInput = true;
    }

    public void UseChangeSpellInput()
    {
        ChangeSpellInput = false;
    }

    public void OnPauseGameInput(InputAction.CallbackContext context)       // Tap
    {
        if (context.started)
            PauseGameInput = true;
        else if (context.performed || context.canceled)
            PauseGameInput = false;
    }

    public void UsePauseGameInput()
    {
        PauseGameInput = false;
    }
    #endregion OnInput Functions
}