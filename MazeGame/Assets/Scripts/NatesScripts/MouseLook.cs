using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private PlayerMovement controls;

    [Range(1f, 100f)] public float mouseSen = 50f;
    private Vector2 mouseLook;
    private float xRotation;
    private Transform PlayerBody;

    private void Awake()
    {
        PlayerBody = transform.parent;

        controls = new PlayerMovement();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        controls.Enable();

    }
    private void OnDisable()
    {
        controls.Disable();

    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        mouseLook = controls.PlayerActions.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSen * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSen * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);


    }



}
