using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // public variables
    public SpriteRenderer sr;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    // private variables
    private int live;

    // Start is called before the first frame update
    void Start()
    {
        live = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get Damage
    public void GetDamage()
    {
        live--;
        if (live < 1)
        {
            Destroy(gameObject);
        } 
        else if (live < 4)
        {
            sr.sprite = sprite3;
        }
        else if (live < 6)
        {
            sr.sprite = sprite2;            
        }
    }
}
