using UnityEngine;

public class Collectible : MonoBehaviour
{
    float rotateSpeed;
    float collectDistance;
    float moveSpeed;
    Transform player;
    protected AudioManager sound;
    protected bool collided = false;

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

    public virtual void OnTriggerEnter(Collider other){ }


}
