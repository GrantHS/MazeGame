using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour, IAbility
{
    private ItemCollection _itemCollection;
    private Material originMat;
    private float _duration = 5f;

    private void Awake()
    {
        _itemCollection = GetComponent<ItemCollection>();
    }

    public void activateAbility()
    {
        StartCoroutine(BecomeInvisible());
    }

    private IEnumerator BecomeInvisible()
    {
        Debug.Log("Is Invisible");
        originMat = this.gameObject.GetComponentInChildren<MeshRenderer>().material;
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = _itemCollection.invisibleMat;
        //Need GameManager to send invisible signal to AI
        yield return new WaitForSeconds(_duration);
        Debug.Log("Is not Invisible");
        this.gameObject.GetComponentInChildren<MeshRenderer>().material = originMat;


    }
}
