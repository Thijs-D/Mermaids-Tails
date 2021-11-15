using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyMeeleElite : Enemy
{
    public SkeletonAnimation sk;
    public AnimationReferenceAsset idle, walking, fight, block, death;
    public enum states { IDLE, WALK, FIGHT, BLOCK, DEATH };
    public states currentState;

    protected EnemyMeeleElite() : base(true, 200, 4, 5, 10)
    {

    }

    new
        // Start is called before the first frame update
        void Start()
    {
        base.Start();
        currentState = states.WALK;
        setCharacterState();
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
    }

    // set character animation
    private void setAnimation(AnimationReferenceAsset pAnimation, bool pLoop, float pTimescale)
    {
        Spine.TrackEntry ae = sk.state.SetAnimation(0, pAnimation, pLoop);
        ae.TimeScale = pTimescale;
        ae.Complete += Ae_Complete;
    }

    // do something after animation completes
    private void Ae_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState == states.FIGHT)
        {
            currentState = states.WALK;
            setCharacterState();
            doAttack = false;
        }
        else if (currentState == states.DEATH)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setCharacterState()
    {
        switch (currentState)
        {
            case states.WALK:
                {
                    setAnimation(walking, true, 1f);
                    break;
                }
            case states.FIGHT:
                {
                    setAnimation(fight, false, 1f);
                    break;
                }
            case states.BLOCK:
                {
                    setAnimation(block, false, 1f);
                    break;
                }
            case states.DEATH:
                {
                    setAnimation(death, false, 1f);
                    break;
                }
            default:
                {
                    setAnimation(idle, true, 1f);
                    break;
                }
        }
    }
}
