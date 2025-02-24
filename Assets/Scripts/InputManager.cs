using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = new PlayerInput.OnFootActions();
        onFoot = playerInput.OnFoot;


        //motor = GetComponent<PlayerMotor>();
        //look = GetComponent<PlayerLook>();

        //onFoot.Jump.performed += ctx => motor.Jump();

        //onFoot.Sprint.performed += ctx => motor.Sprint();
        //onFoot.Sprint.canceled += ctx => motor.StopSprint();
    }

    void FixedUpdate()
    {

        //motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        //look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
