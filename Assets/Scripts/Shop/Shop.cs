using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private bool inRange;
    [SerializeField] private GameObject loadingBarPanel;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private GameObject interactImage;
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Sprite shopSprite;
    void Update()
    {
        if (Input.GetKeyDown(PlayerInput.useKey))
            if (inRange)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                loadingBarPanel.SetActive(true);
                loadingScreenImage.sprite = shopSprite;
                loadingBar.operation=SceneManager.LoadSceneAsync("Shop");
                loadingBar.levelName = "SHOP";
            }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            interactImage.SetActive(true);
            interactText.text = PlayerInput.useKey.ToString();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            interactImage.SetActive(false);
        }
    }
}
