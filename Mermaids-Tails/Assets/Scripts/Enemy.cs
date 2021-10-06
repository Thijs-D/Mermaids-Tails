using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
