using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] Rigidbody[] objectParts;
    void Start()
    {
        foreach(Rigidbody objPart in objectParts)
        {
            int randomXDirX = Random.Range(-1, 2);
            int randomXDirY = Random.Range(0, 2);
            int randomXDirZ = Random.Range(-1, 2);
            float randomForce = Random.Range(0, 1f);
            float randomRotX = Random.Range(-90, 91);
            float randomRotY = Random.Range(-90, 91);
            float randomRotZ = Random.Range(-90, 91);
            objPart.transform.rotation = Quaternion.Euler(randomRotX, randomRotY, randomRotZ);
            Vector3 direction = new Vector3(randomXDirX, randomXDirY, randomXDirZ);
            objPart.AddForce(direction * randomForce, ForceMode.Impulse);
        }
    }
}
