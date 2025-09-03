using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float rotateSpeed;
    public float collectDistance;
    public float moveSpeed;
    private Transform player;
    protected AudioManager sound;
    protected bool collided = false;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sound = AudioManager.instance;
    }
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        if (Vector3.Distance(player.position, transform.position) <= collectDistance)
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<HealthManager>().AddCoins(1);
                sound.PlayCoinCollectSound();
                Destroy(gameObject);
                collided = true;
            }
        }
    }
}
