using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedElite : Enemy
{
    protected EnemyRangedElite() : base(100, 4, 3, 7, 5f)
    {

    }

    new
        // Start is called before the first frame update
        void Start()
    {
        base.Start();
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
    }
}
