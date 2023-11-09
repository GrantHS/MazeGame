using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public ItemCollectables power;
    private Rigidbody rb;
    private float _rollSpeed = 5;
    public GameObject freezeEffect, invisibleEffect, speedEffect, strengthEffect, jumpEffectPrefab;
    private GameObject jumpEffect;
    public Material invisibleMat;
    private Vector3 effectPos;
    public float Vertical = 0.5f;
    public float LoopSpeed = 1f;

    private void OnEnable()
    {
        freezeEffect.gameObject.SetActive(false);
        invisibleEffect.gameObject.SetActive(false);
        speedEffect.gameObject.SetActive(false);
        strengthEffect.gameObject.SetActive(false); 
        //jumpEffect.gameObject.SetActive(false);

        Array values = Enum.GetValues(typeof(ItemCollectables));
        System.Random random = new System.Random();
        power = (ItemCollectables)values.GetValue(random.Next(values.Length)); //Comment this out when using line below
        //power = ItemCollectables.Invisibility; //Uncomment and change this variable to get specific abilities to spawn
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
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case ItemCollectables.Freeze:
                this.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                freezeEffect.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        Destroy(jumpEffect);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = UnityEngine.Random.onUnitSphere * _rollSpeed;

        if (power == ItemCollectables.Jump)
        {
            jumpEffect = Instantiate(jumpEffectPrefab, this.transform.position, jumpEffectPrefab.transform.rotation);
            //jumpEffect.transform.position = this.transform.position;
            jumpEffect.SetActive(true);
            if (!jumpEffect.GetComponent<ParticleSystem>().isEmitting)
            {

                jumpEffect.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void Update()
    {
        if(power == ItemCollectables.Jump)
        {

            effectPos = this.transform.position - jumpEffect.transform.up * 0.65f;
            effectPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * LoopSpeed) * Vertical;           
            jumpEffect.transform.position = effectPos;
        }
        
    }
}
