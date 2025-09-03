using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public SkinnedMeshRenderer playerBody;
    public TMP_Text heartsText;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;
    public int coinsCount;
    public int livesCount;
    public int diamondsCount;
    public Animator anim;
    [SerializeField] private float invincibilityTime;
    private float cpyinv;
    private GameManager gameManager;
    public bool Invulnerable { get; set; }
    private void Awake()
    {
        instance = this;
        heartsText = GameObject.FindGameObjectWithTag("HeartsText").GetComponent<TMP_Text>();
        coinsText = GameObject.FindGameObjectWithTag("CoinsText").GetComponent<TMP_Text>();
        diamondsText = GameObject.FindGameObjectWithTag("DiamondsText").GetComponent<TMP_Text>();
        LoadStats();
    }
    void Start()
    {
        gameManager = GameManager.instance;
        heartsText.text = livesCount.ToString() + "x";
        coinsText.text = coinsCount.ToString() + "x";
        diamondsText.text = diamondsCount.ToString() + "x";
    }
    private void Update()
    {
        if (cpyinv > 0f)
        {
            cpyinv -= Time.deltaTime;
            playerBody.enabled = !playerBody.enabled;
        }
        else playerBody.enabled = true;

    }
    public void AddLife()
    {
        livesCount += 1;
        heartsText.text = livesCount.ToString() + "x";
    }
    public void RemoveLife()
    {
        livesCount -= 1;
        heartsText.text = livesCount.ToString() + "x";
        if (livesCount < 1)
        {
            PlayerPrefs.SetInt("LivesCount", 5);
            gameManager.LoadLobbyLevel();
        }
    }
    
    public void TakeDamage(Vector3 direction, float knockbackForce)
    {
        if (Invulnerable)
            return;
        if (cpyinv <= 0f)
        {
            //anim.SetTrigger("HitByEnemy");
            direction.y = 0f;
            RemoveLife();
            cpyinv = invincibilityTime;
            GetComponent<Player>().enabled = false;
            GetComponent<CharacterController>().Move(direction * knockbackForce * Time.deltaTime);
            GetComponent<Player>().enabled = true;
        }
    }
    public void AddCoins(int value)
    {
        coinsCount += value;
        coinsText.text = coinsCount.ToString() + "x";
    }
    public void RemoveCoins(int value)
    {
        coinsCount -= value;
        coinsText.text = coinsCount.ToString() + "x";
    }
    public void AddDiamonds(int value)
    {
        diamondsCount += value;
        diamondsText.text = diamondsCount.ToString() + "x";
    }
    public void RemoveDiamonds(int value)
    {
        diamondsCount -= value;
        diamondsText.text = diamondsCount.ToString() + "x";
    }
    public void SaveStats()
    {
        PlayerPrefs.SetInt("CoinsCount", coinsCount);
        PlayerPrefs.SetInt("LivesCount", livesCount);
        PlayerPrefs.SetInt("DiamondsCount", diamondsCount);
    }
    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("LivesCount"))
            livesCount = PlayerPrefs.GetInt("LivesCount");
        if (PlayerPrefs.HasKey("DiamondsCount"))
            diamondsCount = PlayerPrefs.GetInt("DiamondsCount");
        if (PlayerPrefs.HasKey("CoinsCount"))
            coinsCount = PlayerPrefs.GetInt("CoinsCount");
    }
}
