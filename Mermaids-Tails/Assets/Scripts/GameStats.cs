using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    // variables
    public static GameStats gameStatsRef;
    private GameObject playerRef;
    private int maximumHP;
    private int currentHP;
    private int maximumMP;
    private int currentMP;
    private int currentScore;
    private int coins;
    public Text healthText;
    public Text skillText;
    public Slider healthSlider;
    public Slider skillSlider;

    private void Awake()
    {
        if (gameStatsRef != null)
        {
            Destroy(gameStatsRef);
        }
        else
        {
            gameStatsRef = this;
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.Find("Player");
        maximumHP = 100;
        maximumMP = 10;
        currentHP = maximumHP;
        currentMP = maximumMP;
        healthSlider.value = getProcentualHealth();
        skillSlider.value = getProcentualPower();
        healthText.text = currentHP + "/" + maximumHP;
        skillText.text = currentMP + "/" + maximumMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // get damage
    public void GetDamage(int damage)
    {
        if (currentHP - damage <= 0)
        {
            currentHP = 0;
            Destroy(playerRef);
        }
        else
        {
            currentHP -= damage;
        }
        healthSlider.value = getProcentualHealth();
        healthText.text = currentHP + "/" + maximumHP;
    }
    
    // heal character
    public void HealCharacter(int heal)
    {
        if(currentHP + heal > maximumHP)
        {
            currentHP = maximumHP;
        }
        else
        {
            currentHP += heal;
        }
        healthSlider.value = getProcentualHealth();
    }

    // return the procentual health with 1 -> 100% and 0 -> 0% and 0.5 -> 50%
    private float getProcentualHealth()
    {
        return ((float)currentHP / (float)maximumHP);
    }

    // return the procentual power with 1 -> 100% and 0 -> 0% and 0.5 -> 50%
    private float getProcentualPower()
    {
        return ((float)currentMP / (float)maximumMP);
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }
}
