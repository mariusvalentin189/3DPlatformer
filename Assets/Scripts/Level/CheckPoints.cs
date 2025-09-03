using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class CheckPoints : MonoBehaviour
{
    public Material inactiveMaterial, activeMaterial;
    public RespawnTrigger respawnTrigger; 
    [SerializeField] private TMP_Text checkpointText;
    [SerializeField] private float timeToDissapear;
    private bool active = false;
    private AudioManager sound;
    private void Awake()
    {
        checkpointText = GameObject.FindGameObjectWithTag("CheckpointText").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        sound = AudioManager.instance;
        HideText();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (active == false)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = activeMaterial.color;
                sound.PlayCheckpointSound();
                active = true;
                respawnTrigger.DeactivateOtherCheckpoints(gameObject);
                ShowText();
                Invoke("HideText", timeToDissapear);

            }
        }

    }
    public bool GetActive()
    {
        return active;
    }
    public void ChangeActive()
    {
        active = !active;
    }
    public void ChangeColor()
    {
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.color = inactiveMaterial.color;
    }
    public bool GetColorIfActive()
    {
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        if (mat.color == activeMaterial.color)
            return true;
        else return false;
    }
    private void HideText()
    {
        checkpointText.text = "";
        checkpointText.gameObject.SetActive(false);
    }
    private void ShowText()
    {
        checkpointText.gameObject.SetActive(true);
        checkpointText.text = "CHECKPOINT REACHED";
    }
}
