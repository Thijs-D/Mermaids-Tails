using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    // public variables
    public int countEnemies;
    public Text scoreText;
    public Text coinText;
    public Text healthText;
    public Text skillText;
    public Slider healthSlider;
    public Slider skillSlider;
    public static GameStats gameStatsRef;
    public AudioClip waves;
    public AudioClip ambient;
    public AudioClip coin;
    public AudioClip win;
    public AudioClip lose;

    // private variables
    private GameObject playerRef;
    private int maximumHP;
    private int currentHP;
    private int maximumMP;
    private int currentMP;
    private int currentScore;
    private int coins;
    private bool isDead;
    private AudioSource ambientSound1;
    private AudioSource ambientSound2;
    private AudioSource currentSound;

    private void Awake()
    {
        /*if (gameStatsRef != null)
        {
            Destroy(gameStatsRef);
        }
        else
        {
            gameStatsRef = this;
        }
        DontDestroyOnLoad(this);*/
        gameStatsRef = this;
        playerRef = GameObject.Find("Player");
        currentSound = gameObject.AddComponent<AudioSource>();
        ambientSound1 = gameObject.AddComponent<AudioSource>();
        ambientSound2 = gameObject.AddComponent<AudioSource>();
        ambientSound1.clip = waves;
        ambientSound2.clip = ambient;
        ambientSound1.volume = 0.2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ambientSound1.Play();
        ambientSound2.Play();
        ambientSound1.loop = true;
        ambientSound2.loop = true;
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
                StartCoroutine(Death(true));
            }
            else
            {
                currentHP -= damage;
            }
            healthSlider.value = getProcentualHealth();
            healthText.text = currentHP + "/" + maximumHP;
        }
    }

    private IEnumerator Death(bool death)
    {
        if (death)
        {
            currentSound.clip = lose;            
        }
        else
        {
            currentSound.clip = win;
        }
        currentSound.Play();
        float fLength = currentSound.clip.length;
        yield return new WaitForSeconds(fLength);
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
        currentSound.clip = coin;
        currentSound.Play();
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
            StartCoroutine(Death(false));
        }
    }
}
