using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    private PlayerMovement controls;
    
    private float moveSpeed = 6f;
    public Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float JumpHeight = 2.4f;
    private CharacterController controller;
    public Transform ground;
    public float distancetoGround = 0.2f;
    public LayerMask groundMask;
    public bool isGrounded;


    private void Awake()
    {
        controls = new PlayerMovement();
        controller = this.GetComponent<CharacterController>();
    }
  private void Update()
    {
        Interactables();
        PlayerMoving();  
        RightClick();
        LeftClick();
        Attacking();
        Gravity();
        //Paused();
        Jump();

        

    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distancetoGround, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void PlayerMoving()
    {
        move = controls.PlayerActions.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {

        if (controls.PlayerActions.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * -9.81f);
        }
       
    }

    private void LeftClick()
    {
        if (controls.PlayerActions.PickUp.triggered)
        {
            Debug.Log("Player will PICK UP an object to be stored");

        }
    }

    private void RightClick()
    {
        if (controls.PlayerActions.UseItems.triggered)
        {
            Debug.Log("Player will USE up an object that is stored");

        }
    }

    private void Attacking()
    {
        if (controls.PlayerActions.Attack.triggered)
        {
            Debug.Log("Attacking Enemies ahhhhhhhhhhhhh");
        }
    }

    private void Interactables()
    {
        if (controls.PlayerActions.Interactables.triggered)
        {
            Debug.Log("Player will INTERACT with an object");

        }
    }

    private void Paused()
    {
        //GameManager.FindAnyObjectByType<GameManager>().PauseDaGame();
    }


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
  
}
