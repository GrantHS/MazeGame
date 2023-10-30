using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Transform player;
    public Transform Farmer;

    private float maxDistance = 15f;
    private float mediumDistance = 7.5f;

    private Image CircleIndicator;

    private void Start()
    {
        CircleIndicator = GetComponent<Image>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, Farmer.position);

        Color newColor;

        if (distance >= maxDistance)
        {
            newColor = Color.blue;
        }
        else if (distance >= mediumDistance)
        {
            
            float t = Mathf.InverseLerp(mediumDistance, maxDistance, distance);
            newColor = Color.Lerp(Color.yellow, Color.blue, t);
        }
        else
        {
            
            float t = Mathf.InverseLerp(0f, mediumDistance, distance);
            newColor = Color.Lerp(Color.red, Color.yellow, t);
        }

        CircleIndicator.color = newColor;
    }
}


