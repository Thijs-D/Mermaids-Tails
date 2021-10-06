using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    // variables
    public static GameStats gameStatsRef;
    private GameObject playerRef;
    private int maximumHP;
    private int currentHP;
    private int currentScore;

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
        currentHP = maximumHP;
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
    }
}
