using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnemy : MonoBehaviour
{
    [SerializeField] private float hideDistance;
    [SerializeField] private Enemy enemyAi;
    private Transform player;
    private bool visible = true;
    [SerializeField] private SkinnedMeshRenderer rend;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > hideDistance && visible)
        {
            rend.enabled = false;
            enemyAi.enabled = false;
            visible = false;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= hideDistance && !visible)
        {
            rend.enabled = true;
            enemyAi.enabled = true;
            visible = true;
        }
    }
}
