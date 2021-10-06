using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

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
        if (collision.name != "Player" && collision.name != "Projectile(Clone)")
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
