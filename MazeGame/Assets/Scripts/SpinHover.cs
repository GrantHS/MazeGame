using UnityEngine;
using System.Collections;

//Float+Spin
public class SpinHover : MonoBehaviour
{
    public float SpinDegree = 15.0f;
    public float Vertical = 0.5f;
    public float LoopSpeed = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        //Spin the Object
        transform.Rotate(new Vector3(0f, Time.deltaTime * SpinDegree, 0f), Space.World);

        // Float Object
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * LoopSpeed) * Vertical;

        transform.position = tempPos;
    }
}