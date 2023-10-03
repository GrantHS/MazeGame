using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    private PlayerMovement controls;
    private ItemCollection _itemCollection;
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
    Array values = Enum.GetValues(typeof(ItemCollectables));
    private IAbility[] abilities;
    private float speedBoost;
    private float _duration = 5f;


    private void Awake()
    {
        speedBoost = moveSpeed / 1.5f;
        abilities = new IAbility[values.Length - 1];
        abilities[1] = gameObject.AddComponent<Invisibility>();
        controls = new PlayerMovement();
        controller = this.GetComponent<CharacterController>();
        _itemCollection = GetComponent<ItemCollection>();
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
            if (_itemCollection != null && _itemCollection.itemSprite.activeSelf)
            {
                _itemCollection.itemSprite.SetActive(false);

                switch (_itemCollection._activeItem)
                {
                    case ItemCollectables.Speed:
                        StartCoroutine(SpeedBoost());
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Strength:
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Invisibility:
                        UseAbility(abilities[1]);
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Clairvoyance:
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    default:
                        Debug.Log("Unknown power used");
                        break;
                }

                
            }
            else Debug.Log("You have no item to use");

        }
    }
    public void UseAbility(IAbility ability)
    {
        ability.activateAbility();
    }

    private IEnumerator SpeedBoost()
    {
        moveSpeed += speedBoost;
        Debug.Log("Zoom");
        yield return new WaitForSeconds(_duration);
        Debug.Log("No Zoom");
        moveSpeed -= speedBoost;
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
