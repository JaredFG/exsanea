using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Player speed multiplier")]
    private float playerSpeed = 2.0f;
    [SerializeField, Tooltip("Input smooth damp speed")]
    private float inputSmoothDamp = 2.0f;
    [SerializeField]
    private float smoothInputSpeed = .2f;

    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private InputAction moveAction;
    private InputAction interactAction;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];

    }
    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 move = new Vector2(currentInputVector.x, currentInputVector.y);
        transform.Translate(move * playerSpeed * Time.deltaTime);



    }

}
