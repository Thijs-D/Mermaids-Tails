using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : Enemy
{
    protected EnemyMeele() : base(50,4,1,5)
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
