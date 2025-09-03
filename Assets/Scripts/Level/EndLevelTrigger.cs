using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject endLevelPanel;
    public HealthManager healthManager;
    public Level level;
    public Level[] levels;
    [SerializeField] private float rotationSpeed;
    private void Start()
    {
        healthManager = HealthManager.instance;
    }
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            endLevelPanel.SetActive(true);
            healthManager.SaveStats();
            PlayerPrefs.SetInt("LevelCompleted" + level.levelId, 1);
            PlayerPrefs.SetInt("LevelUnlocked" + (level.levelId + 1), 1);
        }
    }
}
