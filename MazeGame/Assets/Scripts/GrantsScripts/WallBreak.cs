using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallBreak : MonoBehaviour
{
    public GameObject strengthEffect;
    private Rigidbody[] wallPieces;
    private float force;
    private float blastRadius = 100f;
    private float despawnTime = 2f;
    private bool inUse = true;
    public GameObject tutoralParticle;
    public bool canBreak = true;

    private void Start()
    {
        force = GetComponent<PushDoor>()._pushPower;
        strengthEffect.SetActive(true);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Breakable Wall"))
        {
            Debug.Log("Hit Breakable Wall");
            if (canBreak)
            {
                strengthEffect.SetActive(false);
                tutoralParticle.SetActive(false);
                wallPieces = hit.gameObject.GetComponentsInChildren<Rigidbody>();
                hit.collider.isTrigger = true;
                int pieces = 0;
                while (pieces < wallPieces.Length)
                {
                    foreach (Rigidbody piece in wallPieces)
                    {
                        piece.gameObject.AddComponent<BoxCollider>();

                        StartCoroutine(TNT(piece, hit));
                        pieces++;
                    }

                    canBreak = false;
                }
            }
            

            
            inUse = false;


        }
    }
    private void Update()
    {
        if (!inUse)
        {
            Debug.Log("Disabling");
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        //strengthEffect.SetActive(false);
    }

    private IEnumerator TNT(Rigidbody victim, ControllerColliderHit hit)
    {
        Debug.Log("Boom");
        victim.isKinematic = false;
        Vector3 shrinkage = new Vector3(0.02f, 0.02f, 0.02f);
        hit.gameObject.transform.localScale -= shrinkage;
        victim.AddExplosionForce(10, this.transform.position, blastRadius,1, ForceMode.Impulse);


        
        /*
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        
        

        victim.velocity = pushDirection * 3f;
        */

        yield return new WaitForSeconds(despawnTime);
        victim.gameObject.SetActive(false);
        //inUse = false;
    }
    

    
}
