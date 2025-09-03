using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizingPlatform : MonoBehaviour
{
    public float rateX,rateZ,timer;
    private float maxSizeX, maxSizeZ,timeCount;
    private bool scallingUp=false;
    void Start()
    {
        maxSizeX = transform.localScale.x;
        maxSizeZ = transform.localScale.z;
        timeCount = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (scallingUp == false && timeCount > 0)
        {
            transform.localScale -= new Vector3(rateX, 0f, rateZ) * Time.deltaTime;
            timeCount -= Time.deltaTime;
        }
        else if(scallingUp ==false)
        {
            timeCount = timer;
            scallingUp = true;
        }
        if (scallingUp == true && timeCount > 0)
        {
            transform.localScale += new Vector3(rateX, 0f, rateZ) * Time.deltaTime;
            timeCount -= Time.deltaTime;
        }
        else if(scallingUp==true)
        {
            timeCount = timer;
            scallingUp = false;
        }
        if (transform.localScale.x > maxSizeX && transform.localScale.z > maxSizeZ)
            transform.localScale = new Vector3(maxSizeX,transform.localScale.y,maxSizeZ);
        if (transform.localScale.x < 0f && transform.localScale.z < 0f)
            transform.localScale = new Vector3(0f, transform.localScale.y, 0f);
    }
}
