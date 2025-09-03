using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
    }
}
