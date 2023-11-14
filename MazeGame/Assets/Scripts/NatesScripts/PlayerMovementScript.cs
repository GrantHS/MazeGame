using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

//IDK why this is here
//using static System.Net.Mime.MediaTypeNames;

public class PlayerMovementScript : MonoBehaviour
{
    private PlayerMovement controls;
    private ItemCollection _itemCollection;
    private float moveSpeed = 6f;
    public Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float JumpHeight = 4.2f;
    private CharacterController controller;
    public Transform ground;
    public float distancetoGround = 0.2f;
    public LayerMask groundMask;
    public bool isGrounded;
    Array values = Enum.GetValues(typeof(ItemCollectables));
    private IAbility[] abilities;
    public Text abilityStatusText;
    private int statusNum = 5;
    private float speedBoost;
    private int _duration = 5;
    private bool _canJump = false;
    private int maxJumps = 3;
    private int jumpCount = 0;
    private Vector3 jumpPos;
    public bool _isInvisible = false;
    private Material originMat;
    public GameObject miniMap;
    private GameObject compass;
    private Animation compassAnims;
    public GameObject frostBallPrefab;
    private Camera playerCam;
    public Material freezeMat;
    public GameObject invisibleEffect, speedEffect, strengthEffect, featherEffect;
    public GameObject strengthParticleTut;
    private int speedCount = 5;
    private int invisibleCount = 5;
    private int compassCount = 5;


    private void Awake()
    {
        abilityStatusText.enabled = false;
        miniMap.SetActive(false);
        invisibleEffect.gameObject.SetActive(false);
        speedEffect.gameObject.SetActive(false);
        strengthEffect.gameObject.SetActive(false);
        //featherEffect.gameObject.SetActive(false);

        speedBoost = moveSpeed / 1.5f;
        abilities = new IAbility[values.Length - 1];
        abilities[1] = gameObject.AddComponent<Invisibility>();
        controls = new PlayerMovement();
        controller = this.GetComponent<CharacterController>();
        _itemCollection = GetComponent<ItemCollection>();

        compass = GetComponentInChildren<Compass>().gameObject;
        compass.SetActive(false);
        compassAnims = compass.GetComponent<Animation>();
        playerCam = GetComponentInChildren<Camera>();
        //animator = GetComponent<Animator>();
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

        //abilityStatusText.enabled = _canJump;

        if (!isGrounded)// && !featherEffect.GetComponent<ParticleSystem>().isPlaying)
        {
            //featherEffect.SetActive(true);
            if (!featherEffect.GetComponent<ParticleSystem>().isEmitting)
            {
                Debug.Log("Feathers!");
                featherEffect.transform.position = this.transform.position;
                featherEffect.GetComponentInParent<ParticleSystem>().Play();
            }
            //else Debug.Log("Not emitting");
            
        }
        else// if (featherEffect.activeSelf)
        {
            featherEffect.transform.position = jumpPos;
            featherEffect.GetComponentInParent<ParticleSystem>().Stop();
            //featherEffect.SetActive(false);
        }
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
        if (isGrounded)
        {
            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * moveSpeed * Time.deltaTime);
        }
        
    }

    private void Jump()
    {
        if (_canJump)
        {
            abilityStatusText.enabled = true;
            abilityStatusText.text = "Springs left: " + (maxJumps - jumpCount);
            if (controls.PlayerActions.Jump.triggered && isGrounded && jumpCount < maxJumps)
            {
                jumpPos = this.transform.position;
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * -9.81f);
                jumpCount++;
            }
            else if (jumpCount >= maxJumps)
            {
                _canJump = false;
                jumpCount = 0;
                abilityStatusText.enabled = false;
            }
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
                _itemCollection.rightClickText.SetActive(false);

                switch (_itemCollection._activeItem)
                {
                    case ItemCollectables.Speed:
                        StartCoroutine(SpeedBoost());
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Strength:
                        if (!GetComponent<WallBreak>())
                        {
                            gameObject.AddComponent<WallBreak>();
                        }
                        GetComponent<WallBreak>().enabled = true;
                        WallBreak wallBreak = GetComponent<WallBreak>();
                        wallBreak.canBreak = true;
                        wallBreak.strengthEffect = strengthEffect;
                        wallBreak.strengthEffect.SetActive(true);
                        wallBreak.tutoralParticle = strengthParticleTut;
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Invisibility:
                        StartCoroutine(BecomeInvisible());
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Clairvoyance:
                        StartCoroutine(EquipCompass());
                        miniMap.SetActive(true);
                        Debug.Log("You used " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Jump:
                        _canJump = true;
                        Debug.Log("You used: " + _itemCollection._activeItem);
                        break;
                    case ItemCollectables.Freeze:
                        ShootFrostBall();
                        Debug.Log("You used: " + _itemCollection._activeItem);
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

    private void ShootFrostBall()
    {
        GameObject frostBall = Instantiate(frostBallPrefab, playerCam.transform.position + playerCam.transform.forward, playerCam.transform.rotation);
        FrostBall ball = frostBall.AddComponent<FrostBall>();
        //FrostBall ball = frostBall.GetComponent<FrostBall>();
        ball.master = playerCam.transform;
        ball.freezeMat = freezeMat;

    }

    private IEnumerator BecomeInvisible()
    {
        Debug.Log("Is Invisible");
        abilityStatusText.enabled = true;
        StartCoroutine(CountDown(_duration, "Magic Left: ", "g"));
        originMat = this.gameObject.GetComponentInChildren<MeshRenderer>().material;
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = _itemCollection.invisibleMat;
        invisibleEffect.SetActive(true);
        //Need GameManager to send invisible signal to AI
        _isInvisible = true;
        yield return new WaitForSeconds(_duration);
        abilityStatusText.enabled = false;
        _isInvisible = false;
        Debug.Log("Is not Invisible");
        invisibleEffect.SetActive(false);
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = originMat;
    }

    private IEnumerator SpeedBoost()
    {
        abilityStatusText.enabled = true;
        StartCoroutine(CountDown(_duration, "Caffeine Intake: ", "oz"));
        moveSpeed += speedBoost;
        Debug.Log("Zoom");
        speedEffect.SetActive(true);
        yield return new WaitForSeconds(_duration);
        abilityStatusText.enabled = false;
        speedEffect.SetActive(false);
        Debug.Log("No Zoom");
        moveSpeed -= speedBoost;
    }

    private IEnumerator CountDown(int duration, string text)
    {
        int count = 0;
        int temp = statusNum;
        while(count < duration)
        {
            abilityStatusText.text = text + statusNum;
            yield return new WaitForSeconds(duration/duration);
            statusNum--;
            count++;
            
        }

        statusNum = temp;
    }

    private IEnumerator CountDown(int duration, string firstText, string secondText)
    {
        int count = 0;
        int temp = statusNum;
        while (count < duration)
        {
            abilityStatusText.text = firstText + statusNum + secondText;
            yield return new WaitForSeconds(duration / duration);
            statusNum--;
            count++;

        }

        statusNum = temp;
    }



    private IEnumerator EquipCompass()
    {

        if(!compass.activeInHierarchy)
        {
            compass.SetActive(true);
        }

        compassAnims.Play();
        abilityStatusText.enabled = true;
        StartCoroutine(CountDown(_duration, "Compass Breaking in: "));
        //animator.Play("CompassEquip");

        yield return new WaitForSeconds(_duration);
        abilityStatusText.enabled = false;
        compassAnims.Play("CompassDrop");
        //animator.Play("CompassDrop");

    }
    /*
    private IEnumerator SupaJump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2f * -9.81f);
        jumpCount++;
    }
    */

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
