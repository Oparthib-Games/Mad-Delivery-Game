using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 wasd;
    public static float H; // Horizontal
    public static float V; //Vertical
    public static bool slidePerformed;

    PlayerInputActions inputActions;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        HandleWASDInput();
        HandleSlideTrigger();

        Debug.Log(slidePerformed);
    }
    private void HandleWASDInput()
    {
        wasd = inputActions.Player.Move.ReadValue<Vector2>();
        H = inputActions.Player.Move.ReadValue<Vector2>().x;
        V = inputActions.Player.Move.ReadValue<Vector2>().y;
        //print(wasd);
    }

    private void HandleAttackInput()
    {
        //isAttack = inputActions.Player.Attack.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    }
    private void HandleSlideTrigger()
    {
        slidePerformed = inputActions.Player.Slide.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    }
}
