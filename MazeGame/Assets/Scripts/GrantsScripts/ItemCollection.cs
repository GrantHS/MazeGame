using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject _barrel;
    private Vector3 _barrelSpawn;
    private float _barrelSpawnDistance = 100f;
    private ItemCollectables _activeItem;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Barrel"))
        {
            //Debug.Log("You got a special item!");
            _barrel = hit.gameObject;
            Vector3 barrelPos = _barrel.transform.position;
            _barrel.GetComponent<MeshRenderer>().enabled = false;

            Instantiate(itemPrefab, barrelPos, _barrel.transform.rotation);

            _barrelSpawn = _barrel.transform.position;
            _barrelSpawn.y += _barrelSpawnDistance;
            _barrel.transform.position = _barrelSpawn;
            _barrel.GetComponent<MeshRenderer>().enabled = true;
        }

        if (hit.gameObject.CompareTag("PowerUp"))
        {
            _activeItem = hit.gameObject.GetComponent<PowerUp>().power;
            hit.gameObject.SetActive(false);
            Debug.Log("You found a " +_activeItem + " orb!");

            switch (_activeItem)
            {
                case ItemCollectables.Speed:
                    //Give player super speed when they push ability button
                    break;
                case ItemCollectables.Strength:
                    //Give player super strength when they push ability button
                    break;
                case ItemCollectables.Invisibility:
                    //Give player invisibility when they push ability button
                    break;
                default:
                    Debug.Log("Unknown orb power");
                    break;
            }
        }

        
    }
    private IEnumerator Respawn(float respawnTime)
    {
        _barrelSpawn = _barrel.transform.position;
        _barrelSpawn.y += _barrelSpawnDistance;
        yield return new WaitForSeconds(respawnTime);
        _barrel.transform.position = _barrelSpawn;
        _barrel.GetComponent<MeshRenderer>().enabled = true;
    }


}
