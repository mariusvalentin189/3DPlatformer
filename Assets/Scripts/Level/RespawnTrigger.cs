using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public static RespawnTrigger instance;
    public GameObject[] checkpoints;
    private Player playerController;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoints");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].GetComponent<CheckPoints>().GetActive() == false)
                checkpoints[i].GetComponent<CheckPoints>().ChangeColor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform player = other.gameObject.transform;
            bool foundCheckpoint = false;
            for (int i = 0; i < checkpoints.Length; i++)
            {
                CheckPoints checkp = checkpoints[i].GetComponent<CheckPoints>();
                if (checkp.GetActive() == true)
                {
                    player.GetComponent<CharacterController>().enabled = false;
                    player.position = checkpoints[i].transform.position;
                    player.GetComponent<CharacterController>().enabled = true;
                    player.GetComponent<HealthManager>().RemoveLife();
                    foundCheckpoint = true;
                    return;
                }
            }
            if(foundCheckpoint==false)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.position = playerController.initialPos;
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<HealthManager>().RemoveLife();
            }
        }
    }
    public void DeactivateOtherCheckpoints(GameObject checkpoint)
    {
        for (int i = 0; i < checkpoints.Length;i++)
        {
            if (checkpoints[i] != checkpoint && checkpoints[i].GetComponent<CheckPoints>().GetActive() == true)
            {
                checkpoints[i].GetComponent<CheckPoints>().ChangeActive();
                break;
            }

        }
    }

    public void RestoreCheckpoint()
    {
        bool foundCheckpoint = false;
        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckPoints checkp = checkpoints[i].GetComponent<CheckPoints>();
            if (checkp.GetActive() == true)
            {
                playerController.GetComponent<CharacterController>().enabled = false;
                playerController.transform.position = checkpoints[i].transform.position;
                playerController.GetComponent<CharacterController>().enabled = true;
                foundCheckpoint = true;
                return;
            }
        }
        if (foundCheckpoint == false)
        {
            playerController.GetComponent<CharacterController>().enabled = false;
            playerController.transform.position = playerController.initialPos;
            playerController.GetComponent<CharacterController>().enabled = true;
        }
    }
}
