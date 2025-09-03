using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChallengeCoin : MonoBehaviour
{
    public float timeToComplete;
    public float rotateSpeed;
    public Transform[] coinsSpawnPoints;
    public GameObject coinPrefab;
    public GameObject diamondPrefab;
    public Transform diamondSpawnPoint;
    public TMP_Text timerText;
    private GameObject[] coins;
    private bool coinsSpawned;
    private float timer;
    private AudioManager sound;
    void Start()
    {
        coins = new GameObject[coinsSpawnPoints.Length];
        timer = timeToComplete;
        sound = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        if (coinsSpawned)
        {
            timer -= Time.deltaTime;
            CheckRemainingCoins();
            timerText.text = ((int)timer).ToString();
            if (timer < 0.8f && CheckRemainingCoins())
            {
                DestroyCoins();
                transform.GetChild(0).gameObject.SetActive(true);
                coinsSpawned = false;
                timerText.text = "";
            }
            else if (!CheckRemainingCoins() && timer >= 0.8f)
            {
                HealthManager.instance.AddCoins(coinsSpawnPoints.Length);
                timerText.text = "";
                GameObject diamond=Instantiate(diamondPrefab, diamondSpawnPoint);
                diamond.transform.localPosition = Vector3.zero;
                diamond.transform.SetParent(null);
                Destroy(transform.parent.gameObject);
            }
        }
    }
    void SpawnCoins()
    {
        for (int i = 0; i < coinsSpawnPoints.Length; i++)
        {
            coins[i] = Instantiate(coinPrefab, coinsSpawnPoints[i]);
            coins[i].transform.localPosition = Vector3.zero;
        }
        coinsSpawned = true;
    }
    bool CheckRemainingCoins()
    {
        for (int i = 0; i < coins.Length; i++)
            if (coins[i] != null)
                return true;
        return false;
    }
    void DestroyCoins()
    {
        for (int i = 0; i < coins.Length; i++)
            if (coins[i] != null)
                Destroy(coins[i]);
        timer = timeToComplete;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (coinsSpawned == false)
        {
            if (other.CompareTag("Player"))
            {
                coinsSpawned = true;
                sound.PlayCoinCollectSound();
                transform.GetChild(0).gameObject.SetActive(false);
                SpawnCoins();
            }
        }
    }
}
