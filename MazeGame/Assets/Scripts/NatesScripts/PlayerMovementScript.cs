using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //notes to designer do we want a Min look distace and a Max look distance 

    //Player looking Stuff
    [SerializeField]
    private Vector2 mouseLook;
    public float MouseSen = 100f;
    private PlayerMovement controls;


     private Vector3 playerBody;

    public float xRotation = 0f;
    //Player Movement Stuff
    public float playerSpeed = 10.0f;



    private void Awake()
    {
        controls = new PlayerMovement();
        this.transform.position = playerBody;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Look();


    }

    private void Look()
    {
        mouseLook = controls.PlayerActions.Look.ReadValue<Vector2>();
         
        //notr getting any Y input?? 

        float mouseX = Input.GetAxis("Mouse X") * MouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSen * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody = (Vector3.up * mouseX);
    }



}
