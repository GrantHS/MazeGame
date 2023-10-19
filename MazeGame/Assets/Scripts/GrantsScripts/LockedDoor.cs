using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Tooltip("The color of the key needed to unlock the door")]
    public DoorColor color;
    public Material _mat1, _mat2, _mat3;

    public bool isLocked;

    private void Awake()
    {
        switch (color)
        {
            case DoorColor.Yellow:
                this.gameObject.GetComponent<MeshRenderer>().material = _mat1;
                break;
            case DoorColor.Orange:
                this.gameObject.GetComponent<MeshRenderer>().material = _mat2;
                break;
            case DoorColor.Red:
                this.gameObject.GetComponent<MeshRenderer>().material = _mat3;
                break;
            default:
                Debug.Log("Color of door unknown");
                break;
        }
    }


}
