using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorIndicator : MonoBehaviour
{
    [SerializeField] private float distanceDanger;
    [SerializeField] private float distance;
    [SerializeField] private GameObject mazekeeper;
    [SerializeField] private GameObject player;

    //UI Objects
    [SerializeField] private GameObject dangerIndicator;
    [SerializeField] private Color red;
    [SerializeField] private Color green;
    [SerializeField] private float changeSpeed;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = mazekeeper.transform.position - player.transform.position;
        distance = distanceVector.magnitude;
        print(distance);
        IndicatorChangeColor(distance);
    }

    private void IndicatorChangeColor(float distance)
    {
        float offset = distance * changeSpeed;
        GetComponent<Image>().color = Color.Lerp(green, red, offset); //this changes the color gradually as the mazekeeper and player move
    }
}
