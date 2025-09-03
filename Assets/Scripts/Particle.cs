using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float timer;
    void Start()
    {
        Invoke("DestroyObj", timer);
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
