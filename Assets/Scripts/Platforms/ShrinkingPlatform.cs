using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    public float rate, shrinkingTime, appearTime;
    private Vector3 cpyScale;
    [HideInInspector] public bool triggered;
    private float cpyshrinkingtime;
    public MeshCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        cpyScale = transform.localScale;
        cpyshrinkingtime = shrinkingTime;
        cpyScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            if (cpyshrinkingtime < 0f)
            {
                transform.localScale -= new Vector3(rate, 0f, rate) * Time.deltaTime;
            }
            else cpyshrinkingtime -= Time.deltaTime;
        }
        if (transform.localScale.x <= 0f || transform.localScale.z <= 0f)
        {
            transform.localScale = new Vector3(0f, transform.localScale.y, 0f);
            coll.enabled = false;
            triggered = false;
            Invoke("Appear", appearTime);
        }
    }
    void Appear()
    {
        transform.localScale = cpyScale;
        coll.enabled = true;
        cpyshrinkingtime = shrinkingTime;
    }
}
