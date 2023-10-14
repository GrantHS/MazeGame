using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public ItemCollectables power;
    private Rigidbody rb;
    private float _rollSpeed = 5;
    public GameObject freezeEffect, invisibleEffect, speedEffect, strengthEffect;
    public Material invisibleMat;

    private void OnEnable()
    {
        freezeEffect.gameObject.SetActive(false);
        invisibleEffect.gameObject.SetActive(false);
        speedEffect.gameObject.SetActive(false);
        strengthEffect.gameObject.SetActive(false);
        Array values = Enum.GetValues(typeof(ItemCollectables));
        System.Random random = new System.Random();
        power = ItemCollectables.Speed; //power = (ItemCollectables)values.GetValue(random.Next(values.Length));
        //Debug.Log("Item power: " +  power);

        switch (power)
        {
            case ItemCollectables.Speed:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                speedEffect.SetActive(true);
                break;
            case ItemCollectables.Strength:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                strengthEffect.SetActive(true);
                break;
            case ItemCollectables.Invisibility:
                this.gameObject.GetComponent<MeshRenderer>().material = invisibleMat;
                invisibleEffect.SetActive(true);
                break;
            case ItemCollectables.Clairvoyance:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
                break;
            case ItemCollectables.Jump:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case ItemCollectables.Freeze:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                freezeEffect.SetActive(true);
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
