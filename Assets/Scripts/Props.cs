using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    [SerializeField] private int coinsDropAmmount;
    [SerializeField] GameObject destroyedObject;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void DestroyObject()
    {
        gameManager.StawnCoins(coinsDropAmmount, transform.position);
        GameObject destroyedProp = Instantiate(destroyedObject, transform.position, Quaternion.identity);
        Destroy(destroyedProp, 2f);
        Destroy(gameObject);
    }
}
