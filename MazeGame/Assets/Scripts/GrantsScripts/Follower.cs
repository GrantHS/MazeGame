using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject target;

    [SerializeField]
    [Tooltip("How far on the Z-axis this object will follow.")]
    private float _trackingDistance;

    /*
    [SerializeField]
    [Tooltip("How high up the camara is going to be relative to the target")]
    private float _trackingHeight;
    */

    private Vector3 _targetLocation;
    private Vector3 _trackDirection;
    private Vector3 _currentLocation;
    public Transform camPosition;
    // Start is called before the first frame update
    void Start()
    {
        _targetLocation = target.transform.position;
        _currentLocation = camPosition.position;
        this.transform.position = _currentLocation;

    }

    // Update is called once per frame
    void Update()
    {
        _currentLocation = camPosition.position;
        transform.position = _currentLocation;
        //_targetLocation = target.transform.position;
        //_trackDirection = _targetLocation - _currentLocation;
        //_trackDirection.z = 0;
        //_currentLocation += _trackDirection;
        //this.transform.position = _currentLocation;
    }
}
