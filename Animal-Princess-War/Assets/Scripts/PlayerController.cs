using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float playerSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float gravityValue;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float rotationSpeed;

    Animator anim;
    private CharacterController controller;
    private Player playerInput;
    private Vector3 playerVelocity;

    private Transform cameraMain;
    private Transform child;

    void Awake()
    {
        playerInput = new Player();
    }


    void Start()
    {
        rotationSpeed = 4f;

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        cameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;

    }

    void Update()
    {
        ControllPlayer();
    }

    // movement
    void ControllPlayer()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (playerInput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movementInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}