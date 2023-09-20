using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorIndicator : MonoBehaviour
{
    [SerializeField] private float distanceDanger;
    [SerializeField] private float distanceAlert;
    [SerializeField] private float distance;
    [SerializeField] private GameObject mazekeeper;
    [SerializeField] private GameObject player;

    //UI Objects
    [SerializeField] private GameObject dangerIndicator;
    [SerializeField] private Color red = Color.red;
    [SerializeField] private Color yellow = Color.yellow;
    [SerializeField] private Color green = Color.green;
    [SerializeField] private float changeSpeed;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = mazekeeper.transform.position - player.transform.position;
        distance = distanceVector.magnitude;
        //IndicatorChangeColor(distance);
        IndicatorChangeColorSimple(distance);
    }

    private void IndicatorChangeColor(float distance) //doesn't work rn
    {
        print("this is running");
        float offset = distance * changeSpeed;
        GetComponent<Image>().color = Color.Lerp(green, red, offset); //this changes the color gradually as the mazekeeper and player move
    }

    private void IndicatorChangeColorSimple(float distance) //this changes the color of the indicator when distance between mazekeeper and player hits a certain point
    {
        if(distance <= distanceDanger)
        {
            GetComponent<Image>().color = red;
        }
        if(distance <= distanceAlert && distance >= distanceDanger)
        {
            GetComponent<Image>().color = yellow;
        }
        if(distance >= distanceAlert)
        {
            GetComponent<Image>().color = green;
        }
    }
}
