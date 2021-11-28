using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool isDead;
    public int countEnemies;
    public Text scoreText;
    public Text coinText;
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
        playerRef = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    { 
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

    public GameObject getPlayer()
    {
        return playerRef;
    }

    // get damage
    public void GetDamage(int damage)
    {
        if (!isDead)
        {
            if (currentHP - damage <= 0)
            {
                currentHP = 0;
                isDead = true;
                playerRef.GetComponent<Player>().isDead = true;
                playerRef.GetComponent<Player>().currentState = Player.states.DEATH;
                playerRef.GetComponent<Player>().setCharacterState();
                StartCoroutine(Death());
            }
            else
            {
                currentHP -= damage;
            }
            healthSlider.value = getProcentualHealth();
            healthText.text = currentHP + "/" + maximumHP;
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
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
        coinText.text = "Coins: " + coins;
    }
    
    public int getCoins()
    {
        return coins;
    }

    public void reduceCoins(int amount)
    {
        coins -= amount;
        coinText.text = "Coins: " + coins;
    }

    public void IncreaseScore(bool elite)
    {
        countEnemies--;
        if (elite)
        {
            currentScore += 10;
        }
        else
        {
            currentScore += 1;
        }
        scoreText.text = "Score: " + currentScore;
        if (countEnemies <= 0)
        {
            StartCoroutine(Death());
        }
    }
}
