using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public bool disappear;
    public float timeToDisappear;
    public float timeToAppear;
    public Material disappearMaterial, defaultMaterial;
    public MeshRenderer rend;
    public MeshCollider coll;
    private float cpyTimeDiss;
    void Start()
    {
        cpyTimeDiss = timeToDisappear;
    }

    // Update is called once per frame
    void Update()
    {
        if (disappear)
        {
            rend.material = disappearMaterial;
            if (cpyTimeDiss <= 0)
            {
                rend.enabled = false;
                coll.enabled = false;
                disappear = false;
                Invoke("Appear", timeToAppear);
            }
            else cpyTimeDiss -= Time.deltaTime;
        }
    }
    void Appear()
    {
        rend.enabled = true;
        coll.enabled = true;
        cpyTimeDiss = timeToDisappear;
        rend.material = defaultMaterial;
    }
}
