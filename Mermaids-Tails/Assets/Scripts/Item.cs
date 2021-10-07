using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {COIN, HEART};
    public ItemType currentType;
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
        if (collision.tag == "Player")
        {
            if (currentType == ItemType.COIN)
            {
                GameStats.gameStatsRef.addCoins(1);
            }
            Destroy(gameObject);    
        }
    }
}
