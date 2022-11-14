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
    private InputAction pauseAction;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    //public CharacterController controller;
    public GameObject Pause;
    public bool isPaused = false;
    public Rigidbody2D rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
        pauseAction = playerInput.actions["Pause"];

    }
    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        bool inputInteract = interactAction.triggered;
        bool inputPause = pauseAction.triggered;

        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 move = new Vector2(currentInputVector.x, currentInputVector.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);
        rb.velocity = (move * playerSpeed);

        if (inputInteract)
        {
            Debug.Log("interactuando");
        }
        if (inputPause)
        {
            Debug.Log("pausando");
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            Pause.SetActive(true);
        }
        else
        {
            Pause.SetActive(false);
        }
    }

}
