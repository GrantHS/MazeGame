using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class LockedDoor : MonoBehaviour
{
    [Tooltip("The color of the key needed to unlock the door")]
    public DoorColor color;

    public bool isLocked;


}
