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
    private bool _facingRight = true;
    public bool isInteracting = false;

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
        //Debug.Log(input.x);
        bool inputInteract = interactAction.triggered;
        bool inputPause = pauseAction.triggered;

        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 move = new Vector2(currentInputVector.x, currentInputVector.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);
        rb.velocity = (move * playerSpeed);

        if (input.x > 0f && _facingRight == true)
        {
            Flip();
        }
        else if (input.x < 0f && _facingRight == false)
        {
            Flip();
        }

        if (inputInteract && isInteracting == false)
        {
            //Debug.Log("interactuando");
            StartCoroutine(StopInteaction());


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
    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    IEnumerator StopInteaction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(2);
        isInteracting = false;
    }

}
