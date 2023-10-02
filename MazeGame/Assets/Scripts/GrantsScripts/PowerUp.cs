using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public ItemCollectables power;
    private Rigidbody rb;
    private float _rollSpeed = 5;

    private void OnEnable()
    {
        Array values = Enum.GetValues(typeof(ItemCollectables));
        System.Random random = new System.Random();
        power = ItemCollectables.Speed; //(ItemCollectables)values.GetValue(random.Next(values.Length));
        //Debug.Log("Item power: " +  power);

        switch (power)
        {
            case ItemCollectables.Speed:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case ItemCollectables.Strength:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case ItemCollectables.Invisibility:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = UnityEngine.Random.onUnitSphere * _rollSpeed;
    }
}
