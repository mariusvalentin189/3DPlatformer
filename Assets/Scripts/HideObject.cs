using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private float hideDistance;
    private Transform player;
    private bool visible = true;
    private MeshRenderer rend;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = GetComponent<MeshRenderer>();
        if(rend == null)
            rend = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > hideDistance && visible)
        {
            rend.enabled = false;
            visible = false;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= hideDistance && !visible)
        {
            rend.enabled = true;
            visible = true;
        }
    }
}
