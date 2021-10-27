using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // other variables
    public GameObject loot;
    protected bool rangedEnemny;
    protected float walkRange = 2;
    protected int worldBorder = 20;
    protected NavMeshAgent agent;

    // variables for attack
    protected bool doAttack;
    protected bool seePlayer;
    protected readonly int maximumHP;
    protected int currentHP;
    public GameObject projectileType;
    protected GameObject playerRef;
    public GameObject healthbar;
    public Slider healthSlider;
    protected int minimumDamage;
    protected int maximumDamage;
    protected float projectileForce;
    protected float normalAttackCooldown;

    // construct an ranged enemy
    protected Enemy(int pHP, int pAttackSpeed, int pMinDmg, int pMaxDmg, float pProjectileForce)
    {
        maximumHP = pHP;
        currentHP = maximumHP;
        normalAttackCooldown = pAttackSpeed;
        minimumDamage = pMinDmg;
        maximumDamage = pMaxDmg;
        projectileForce = pProjectileForce;
        rangedEnemny = true;
    }

    // construct an meele enemy
    protected Enemy(int pHP, int pAttackSpeed, int pMinDmg, int pMaxDmg)
    {
        maximumHP = pHP;
        currentHP = maximumHP;
        normalAttackCooldown = pAttackSpeed;
        minimumDamage = pMinDmg;
        maximumDamage = pMaxDmg;
        rangedEnemny = false;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        playerRef = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!doAttack)
        {
            if (rangedEnemny)
            {
                StartCoroutine(FightPlayerRanged());
            }
            else
            {
                StartCoroutine(FightPlayerMeele());
            }
        }

        if (!agent.hasPath && !seePlayer)
        {
            agent.SetDestination(GetRandomPoint(transform, walkRange));
        }
        else if (seePlayer)
        {
            agent.SetDestination(playerRef.transform.position);
        }
    }


    // get damage
    public void GetDamage(int damage)
    {
        healthbar.SetActive(true);
        if(currentHP - damage <= 0)
        {
            currentHP = 0;
            Instantiate(loot, transform.position, Quaternion.identity);
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

    // return the health in procent
    private float getProcentualHealth()
    {
        return ((float)currentHP / (float)maximumHP);
    }

    // fight the player with a ranged attack
    IEnumerator FightPlayerRanged()
    {
        doAttack = true;
        yield return new WaitForSeconds(normalAttackCooldown);
        agent.SetDestination(GetRandomPoint(transform, walkRange));
        if (playerRef != null)
        {
            GameObject currentProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Vector2 currentPosition = transform.position;
            Vector2 currentPlayerPosition = playerRef.transform.position;
            Vector2 projectileDirection = (currentPlayerPosition - currentPosition).normalized;
            currentProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileForce;
            currentProjectile.GetComponent<EnemyProjectile>().damage = Random.Range(minimumDamage, maximumDamage);
            doAttack = false;
        }
    }

    IEnumerator FightPlayerMeele()
    {
        doAttack = true;
        yield return new WaitForSeconds(normalAttackCooldown);
        if (playerRef != null)
        {
            float minDist = 5;
            float fightDist = 2;
            float dist = Vector3.Distance(playerRef.transform.position, transform.position);
            if (dist < minDist)
            {
                seePlayer = true;
                if (dist < fightDist)
                {
                    Debug.Log("Hi");
                    GameStats.gameStatsRef.GetDamage(Random.Range(minimumDamage, maximumDamage));
                }
            }
            else
            {
                seePlayer = false;
            }
            doAttack = false;
        }
    }

    private bool RandomPoint(Vector3 pCenter, float pRange, out Vector3 pResult)
    {
        for (int i = 0; i < worldBorder; i++)
        {
            Vector3 randomPoint = pCenter + Random.insideUnitSphere * pRange;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                pResult = hit.position;
                pResult.z = 0;
                return true;
            }
        }
        pResult = Vector3.zero;
        return false;
    }

    public Vector3 GetRandomPoint(Transform pPoint, float pRadius)
    {
        Vector3 point;
        if (RandomPoint(pPoint == null ? transform.position : pPoint.position, pRadius == 0 ? walkRange : pRadius, out point))
        {
            return point;
        }
        return pPoint == null ? Vector3.zero : pPoint.position;
    }
}