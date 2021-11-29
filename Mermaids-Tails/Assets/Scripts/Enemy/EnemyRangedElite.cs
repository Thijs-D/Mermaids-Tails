using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedElite : Enemy
{
    protected EnemyRangedElite() : base(true, 250, 2, 5, 10, 4.5f)
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
