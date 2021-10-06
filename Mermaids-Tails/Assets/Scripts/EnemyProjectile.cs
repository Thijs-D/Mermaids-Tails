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
        if (collision.tag != "Enemy" && collision.name != "EnemyProjectile(Clone)")
        {
            if (collision.GetComponent<Enemy>())
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
            }
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
}
