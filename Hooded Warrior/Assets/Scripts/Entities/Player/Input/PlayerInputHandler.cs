using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Components
    private PlayerInput _playerInput;
    private Camera _cam;
    #endregion

    #region Inputs
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool RollInput { get; private set; }

    public bool PrimaryAttackInput { get; private set; }
    public bool SecondaryDefendInput { get; private set; }
    public bool SpellCastInput { get; private set; }
    public bool ChangeWeaponInput { get; private set; }
    public bool ChangeSpellInput { get; private set; }
    public bool PauseGameInput { get; private set; }
    #endregion

    #region Other variables
    [SerializeField] private float _inputHoldTime = 0.2f; // time to consider holding
    private float _jumpInputStartTime;
    private float _dashInputStartTime;
    #endregion

    #region Unity Functions
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _cam = GameManager.Instance.MainCamera;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }
    #endregion

    #region OnInput Functions
    public void OnPauseGameInput(InputAction.CallbackContext context)
    {
        if (context.started)
            PauseGameInput = true;
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
            PrimaryAttackInput = true;
        if (context.canceled)
            PrimaryAttackInput = false;
    }

    public void OnSecondaryDefendInput(InputAction.CallbackContext context)
    {
        if (context.started)
            SecondaryDefendInput = true;
        if (context.canceled)
            SecondaryDefendInput = false;
    }

    public void OnSpellCastInput(InputAction.CallbackContext context)
    {
        if (context.started)
            SpellCastInput = true;
        if (context.canceled)
            SpellCastInput = false;
    }

    public void OnChangeWeaponInput(InputAction.CallbackContext context)
    {
        if (context.started)
            ChangeWeaponInput = true;
    }

    public void OnChangeSpellInput(InputAction.CallbackContext context)
    {
        if (context.started)
            ChangeSpellInput = true;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
            RollInput = true;
        if (context.canceled)
            RollInput = false;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }
        if(context.canceled)
            JumpInputStop = true;
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
            GrabInput = true;
        else if (context.canceled)
            GrabInput = false;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        }
        else if (context.canceled)
            DashInputStop = true;
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard")
            RawDashDirectionInput = _cam.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
    }
    #endregion

    #region Stop Input Functions
    public void UsePauseGameInput()
    {
        PauseGameInput = false;
    }

    public void StopHoldingSecondaryDefendInput()
    {
        SecondaryDefendInput = false;
    }

    public void UseChangeWeaponInput()
    {
        ChangeWeaponInput = false;
    }

    public void UseChangeSpellInput()
    {
        ChangeSpellInput = false;
    }

    public void UseRollInput()
    {
        RollInput = false;
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
            JumpInput = false;
    }

    public void UseDashInput()
    {
        DashInput = false;
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
            DashInput = false;
    }
    #endregion
}
