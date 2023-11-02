using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    public Vector3 SpawnPos { get => spawnPos; set => spawnPos = value; }

    public float _playerHealth;
    private float _fullHealth = 100f;
    private float playerYPos;
    private Vector3 spawnPos;
    private Vector3 rotOffset = new Vector3(0, -135, 0);
    private Quaternion spawnRot;
    private ParticleSystem[] particleSystems;

    [Tooltip("The depth from spawn that the player respawns after passing")]
    public float maxDepth = 20f;

    private float minYPos;

    private bool _isSpawning = false;
    private float _blinkInterval = 0.5f;

    //Temp parameters for animation alternative
    private GameObject newCheckpoint;
    private GameObject oldCheckpoint;
    public Material _inactiveMat;
    public Material _activeMat;

    private void Awake()
    {
        //bud.GetComponent<MeshRenderer>().material.color = _inactiveColor;
        
    }
    void Start()
    {
        SpawnPos = this.transform.position;
        spawnRot = this.transform.rotation;
        minYPos = transform.position.y - maxDepth;
        _playerHealth = _fullHealth;
    }

    void Update()
    {
        playerYPos = transform.position.y;

        if (CheckDeath())
        {
            if (!_isSpawning)
            {
                Debug.Log("Respawning");
                _isSpawning = true;
                _playerHealth = _fullHealth;
                this.transform.position = SpawnPos;
                this.transform.rotation = spawnRot;
                transform.Rotate(rotOffset);
                _isSpawning = false;
                //StartCoroutine(Blink(this.gameObject, _blinkInterval));
            } 
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            if(newCheckpoint == null)
            {
                newCheckpoint = other.gameObject;
            }
            else
            {
                oldCheckpoint = newCheckpoint;
                newCheckpoint = other.gameObject;
            }

            if(oldCheckpoint != null)
            {
                oldCheckpoint.GetComponent<MeshRenderer>().material = _inactiveMat;
            }

            SpawnPos = this.transform.position;
            spawnRot = other.gameObject.transform.rotation;
            //Start bloom animation for flower
            //Alternative to animation
            particleSystems = other.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in particleSystems)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
            }
            newCheckpoint.GetComponent<MeshRenderer>().material = _activeMat;
            Debug.Log("Checkpoint Reached!");
        }
    }

    bool CheckDeath()
    {
        if (_playerHealth <= 0 || playerYPos <= minYPos)
        {
            //Debug.Log("Player position: " + playerYPos);
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
