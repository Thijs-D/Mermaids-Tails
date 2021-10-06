using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // variables for attack
    private int maximumHP;
    private int currentHP;
    public GameObject projectileType;
    private GameObject playerRef;
    public GameObject healthbar;
    public Slider healthSlider;
    private int minimumDamage;
    private int maximumDamage;
    private float projectileForce;
    private float normalAttackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        maximumHP = 100;
        currentHP = maximumHP;
        normalAttackCooldown = 2;
        setWeapon(10, 15, 0.2f);
        StartCoroutine(FightPlayer());
        playerRef = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // set the player weapon ans weapon stats
    private void setWeapon(int pMin, int pMax, float pForce)
    {
        minimumDamage = pMin;
        maximumDamage = pMax;
        projectileForce = pForce;
    }

    // get damage
    public void GetDamage(int damage)
    {
        healthbar.SetActive(true);
        if(currentHP - damage <= 0)
        {
            currentHP = 0;
            Destroy(gameObject);
        }
        else
        {
            currentHP -= damage;
        }
        healthSlider.value = getProcentualHealth();
    }

    // heal character
    public void HealCharacter(int heal)
    {
        healthbar.SetActive(true);
        if (currentHP + heal > maximumHP)
        {
            currentHP = maximumHP;
        }
        else
        {
            currentHP += heal;
        }
        healthSlider.value = getProcentualHealth();
    }

    private float getProcentualHealth()
    {
        return ((float)currentHP / (float)maximumHP);
    }

    // fight the player with a delay
    IEnumerator FightPlayer()
    {
        yield return new WaitForSeconds(normalAttackCooldown);
        if (playerRef != null)
        {
            GameObject currentProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Vector2 currentPosition = transform.position;
            Vector2 currentPlayerPosition = playerRef.transform.position;
            Vector2 projectileDirection = (currentPlayerPosition - currentPosition).normalized;
            currentProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileForce;
            currentProjectile.GetComponent<EnemyProjectile>().damage = Random.Range(minimumDamage, maximumDamage);
            StartCoroutine(FightPlayer());
        }
    }
}
