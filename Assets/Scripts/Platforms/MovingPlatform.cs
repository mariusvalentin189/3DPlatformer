using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] positions;
    private Transform newpos;
    private Vector3 currentPos;
    public float speed,timeForChange;
    private int posNumber=0;
    public CharacterController cc;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        GetPos();
    }
    void FixedUpdate()
    {
        currentPos = Vector3.MoveTowards(transform.position, newpos.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(currentPos);
    }
    void GetPos()
    {
        if (posNumber == 0)
        {
            newpos = positions[0];
            posNumber = 1;
        }
        else if(posNumber==1)
        {
            newpos = positions[1];
            posNumber = 0;
        }
        Invoke("GetPos", timeForChange);
    }
}
