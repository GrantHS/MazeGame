using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    private PlayerMovement controls;
    private float moveSpeed = 6f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float JumpHeight = 2.4f;
    private CharacterController controller;
    public Transform ground;
    public float distancetoGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;


    private void Awake()
    {
        controls = new PlayerMovement();
        controller = GetComponent<CharacterController>();
    }
  private void Update()
    {
        Gravity();
        PlayerMoving();
        Jump();

    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distancetoGround, groundMask);
        if (isGrounded && velocity.y <0)
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
        if (controls.PlayerActions.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
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
