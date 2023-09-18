using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    float playerHealth = 100;
    float playerYPos;
    public Vector3 spawnPos;

    [Tooltip("Empty game object where the player will die if they fall below")]
    public Transform minYPos;

    private List<GameObject> _checkPoints = new List<GameObject>();

    private bool _isSpawning = false;
    private float _blinkInterval = 0.5f;

    //Temp parameters for animation alternative
    public GameObject bud;
    private Color _inactiveColor = Color.grey;
    private Color _activeColor = Color.yellow;

    private void Awake()
    {
        bud.GetComponent<MeshRenderer>().material.color = _inactiveColor;
    }
    void Start()
    {
        spawnPos = this.transform.position;
    }

    void Update()
    {
        playerYPos = transform.position.y;

        if (CheckDeath())
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                this.transform.position = spawnPos;
                StartCoroutine(Blink(this.gameObject, _blinkInterval));
            } 
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            _checkPoints.Add(other.gameObject);
            spawnPos = this.transform.position;
            //Start bloom animation for flower
            //Alternative to animation
            bud.GetComponent<MeshRenderer>().material.color = _activeColor;
            Debug.Log("Checkpoint Reached!");
        }
    }

    bool CheckDeath()
    {
        if (playerHealth <= 0 || playerYPos <= minYPos.position.y)
        {
            return true;
        }
        else return false;
    }

    private IEnumerator Blink(GameObject gameObject, float interval)
    {
        Debug.Log("Blinking");
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //gameObject.transform.position = spawnPos;
        //_isSpawning = true;
        yield return new WaitForSeconds(interval);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds (interval);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(interval);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(interval);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(interval);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _isSpawning = false;
    }
}
