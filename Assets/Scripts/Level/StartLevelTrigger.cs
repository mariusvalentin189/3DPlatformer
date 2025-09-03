using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private string levelName;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject loadingBarPanel;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Sprite levelSprite;
    void Start()
    {
        if (level.unlocked == false)
        {
            coll.enabled = false;
            wall.SetActive(true);
        }
        else
        {
            coll.enabled = true;
            wall.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loadingBarPanel.SetActive(true);
            loadingScreenImage.sprite = levelSprite;
            loadingBar.operation=SceneManager.LoadSceneAsync(level.levelSceneName);
            loadingBar.levelName = levelName;
        }
    }
}
