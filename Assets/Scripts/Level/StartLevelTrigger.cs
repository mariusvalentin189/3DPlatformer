using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] Level level;
    [SerializeField] string levelName;
    [SerializeField] BoxCollider coll;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject loadingBarPanel;
    [SerializeField] LoadingBar loadingBar;
    [SerializeField] Image loadingScreenImage;
    [SerializeField] Sprite levelSprite;
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
            loadingBar.SetLevelName(levelName);
        }
    }
}
