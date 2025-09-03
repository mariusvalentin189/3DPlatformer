using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;
    private static bool audioManagerSpawned;
    void Awake()
    {
        if(!audioManagerSpawned)
	    {
	        Instantiate(audioManager,null);
	        audioManagerSpawned=true;
	    }
    }
}
