using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    //notes to designer do we want a Min look distace and a Max look distance 

    //Player looking Stuff
    [SerializeField]
    private float minLookDis = 25;
    public float MouseSen = 100f;

    [SerializeField]
    Transform playerBody;

    public float xRotation = 0f;
    //Player Movement Stuff
    public float playerSpeed = 10.0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSen * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minLookDis);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
     

   


}
