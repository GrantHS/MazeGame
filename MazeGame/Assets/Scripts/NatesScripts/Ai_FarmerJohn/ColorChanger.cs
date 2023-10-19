using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Transform player;
    public Transform Farmer;

    private float maxDistance = 15f;

    private Image CircleIndicator;

    private void Start()
    {
        CircleIndicator = GetComponent<Image>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, Farmer.position);
        Color newColor = Color.Lerp(Color.red, Color.blue, Mathf.InverseLerp(0f, maxDistance, distance));

        CircleIndicator.color = newColor;
    }

}
