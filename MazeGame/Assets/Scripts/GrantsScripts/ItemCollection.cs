using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject itemSprite;
    public Sprite speedSprite;
    public Sprite strengthSprite;
    public Sprite invisibleSprite;
    public Sprite clairvoyanceSprite;
    public Sprite jumpSprite;
    public Material invisibleMat;
    private GameObject _barrel;
    private Vector3 _barrelSpawn;
    private float _barrelSpawnDistance = 100f;
    public ItemCollectables _activeItem;
    public IAbility _activeAbility;
    private List<IAbility> _abilities;

    private void Start()
    {
        itemSprite.SetActive(false);
    }

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

            switch (_activeItem)
            {
                case ItemCollectables.Speed:
                    //Give player super speed when they push ability button
                    Debug.Log("You found a " + _activeItem + " orb!");
                    itemSprite.GetComponent<Image>().sprite = speedSprite; //(Swap this with line below after sprites are generated)
                    //itemSprite.GetComponent<Image>().color = Color.yellow;
                    itemSprite.SetActive(true);
                    break;
                case ItemCollectables.Strength:
                    //Give player super strength when they push ability button
                    Debug.Log("You found a " + _activeItem + " orb!");
                    itemSprite.GetComponent<Image>().sprite = strengthSprite; //(Swap this with line below after sprites are generated)
                    //itemSprite.GetComponent<Image>().color = Color.red;
                    itemSprite.SetActive(true);
                    break;
                case ItemCollectables.Invisibility:
                    //Give player invisibility when they push ability button
                    Debug.Log("You found an " + _activeItem + " orb!");
                    itemSprite.GetComponent<Image>().sprite = invisibleSprite; //(Swap this with line below after sprites are generated)
                    //itemSprite.GetComponent<Image>().color = Color.black;
                    itemSprite.SetActive(true);
                    break;
                case ItemCollectables.Clairvoyance:
                    //Give player invisibility when they push ability button
                    Debug.Log("You found a " + _activeItem + " orb!");
                    //itemSprite.GetComponent<Image>().sprite = clairvoyanceSprite; //(Swap this with line below after sprites are generated)
                    itemSprite.GetComponent<Image>().color = Color.white;
                    itemSprite.SetActive(true);
                    break;
                case ItemCollectables.Jump:
                    Debug.Log("You found a " + _activeItem + " orb!");
                    itemSprite.GetComponent<Image>().sprite = clairvoyanceSprite; //(Swap this with line below after sprites are generated)
                    //itemSprite.GetComponent<Image>().color = Color.blue;
                    itemSprite.SetActive(true);
                    break;
                default:
                    Debug.Log("Unknown orb power");
                    break;
            }
        }

        
    }

    

    //Using this makes the orbs spawn like crazy; fix if there's enough time
    private IEnumerator Respawn(float respawnTime)
    {
        _barrelSpawn = _barrel.transform.position;
        _barrelSpawn.y += _barrelSpawnDistance;
        yield return new WaitForSeconds(respawnTime);
        _barrel.transform.position = _barrelSpawn;
        _barrel.GetComponent<MeshRenderer>().enabled = true;
    }


}
