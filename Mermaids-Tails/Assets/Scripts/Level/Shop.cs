using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool isBanana;
    private int minDmg;
    private int maxDmg;
    private float projectileSpeed;
    private int cost = 5;
    // Start is called before the first frame update

    public void UpgradeBanana()
    {
        GameStats.gameStatsRef.getPlayer().GetComponent<Player>().setWeapon(minDmg, maxDmg, projectileSpeed);
    }

    public void UpgradeFish()
    {
        GameStats.gameStatsRef.getPlayer().GetComponent<Player>().setWeapon(minDmg, maxDmg, projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && GameStats.gameStatsRef.getCoins() >= cost)
        {
            minDmg = GameStats.gameStatsRef.getPlayer().GetComponent<Player>().minimumDamage * 2;
            maxDmg = minDmg + 5;
            projectileSpeed = GameStats.gameStatsRef.getPlayer().GetComponent<Player>().projectileForce * 2;
            if (isBanana)
            {
                UpgradeBanana();
            }
            else
            {
                UpgradeFish();
            }
        }
    }
}
