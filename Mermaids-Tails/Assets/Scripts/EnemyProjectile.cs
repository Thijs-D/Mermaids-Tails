using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "EnemyProjectile" && collision.tag != "Item")
        {
            if (collision.GetComponent<Player>())
            {
                GameStats.gameStatsRef.GetDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
