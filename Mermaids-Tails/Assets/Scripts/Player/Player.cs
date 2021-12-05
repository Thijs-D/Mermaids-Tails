using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;

public class Player : MonoBehaviour
{
    // public variables
    public GameObject playerCamera;
    public Rigidbody2D rb;
    public SkeletonAnimation sk;
    public AnimationReferenceAsset idle, walking, fightMelee, fightRanged, death;
    public enum states { IDLE, WALK, FIGHTMELEE, FIGHTRANGED, DEATH };
    public states currentState;
    public GameObject projectileType;
    public GameObject gameMenuUI;
    public AudioClip banana;
    public bool isDead;

    // private variables
    private int minimumDamage;
    private int maximumDamage;
    private int meleeDamage;
    private float projectileForce;
    private bool doAttack = false;
    private float speed;
    private float dash;
    private Vector2 direction;
    private AudioSource bananaAudio;

    // Start is called before the first frame update
    void Start()
    {
        bananaAudio = gameObject.AddComponent<AudioSource>();
        bananaAudio.clip = banana;
        setWeapon(50, 10, 20, 3f);
        dash = 0.3f;
        speed = 4;
        currentState = states.IDLE;
        setCharacterState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (direction != Vector2.zero && !doAttack)
            {
                if (currentState != states.WALK)
                {
                    currentState = states.WALK;
                    setCharacterState();
                }
            }
            else if (currentState != states.IDLE && !doAttack)
            {
                currentState = states.IDLE;
                setCharacterState();
            }
            MovePlayer();
        }
    }

    // Updated ist called fixed by time
    private void FixedUpdate()
    {
        // make a 3D Vector of Player Position and Z-Camera Position
        Vector3 currentCameraPosition = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
        playerCamera.transform.position = currentCameraPosition;
    }

    public void OnOpenGameMenu()
    {
        gameMenuUI.SetActive(true);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (!isDead)
            {
                GameObject currentProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Vector2 currentPosition = transform.position;
                Vector2 projectileDirection = (mousePosition - currentPosition).normalized;
                currentProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileForce;
                currentProjectile.GetComponent<Projectile>().damage = Random.Range(minimumDamage, maximumDamage);
                bananaAudio.Play();
            }
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (!doAttack && !isDead)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2f);
                foreach (Collider2D collision in cols)
                {
                    if (collision.CompareTag("Enemy"))
                    {
                        collision.GetComponent<Enemy>().GetDamage(meleeDamage);
                    }
                }
                doAttack = true;
                currentState = states.FIGHTMELEE;
                setCharacterState();
            }
        }        
    }

    public void OnDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (!isDead)
            {
                transform.Translate(dash * speed * direction);
            }           
        }
    }

    // set the player weapon ans weapon stats
    public void setWeapon(int pMelee, int pMin, int pMax, float pForce)
    {
        meleeDamage = pMelee;
        minimumDamage = pMin;
        maximumDamage = pMax;
        projectileForce = pForce;
    }

    // move the player in game
    private void MovePlayer()
    {
        rb.velocity = new Vector2(direction.x * 5, direction.y * 5);
    }

    public int getMinDmg()
    {
        return minimumDamage;
    }

    public int getMaxDmg()
    {
        return maximumDamage;
    }

    public float getProjectileForce()
    {
        return projectileForce;
    }

    public int getMeleeDmg()
    {
        return meleeDamage;
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
        if (currentState == states.FIGHTMELEE || currentState == states.FIGHTRANGED)
        {
            currentState = states.IDLE;
            setCharacterState();
            doAttack = false;
        }
        else if (currentState == states.DEATH)
        {
            Destroy(gameObject);
        }
    }

    // set the state and animation of the character
    public void setCharacterState()
    {
        switch (currentState)
        {
            case states.WALK:
                {
                    setAnimation(walking, true, 1f);
                    break;
                }
            case states.FIGHTMELEE:
                {
                    setAnimation(fightMelee, false, 1f);
                    break;
                }
            case states.FIGHTRANGED:
                {
                    setAnimation(fightRanged, false, 1f);
                    break;
                }
            case states.DEATH:
                {
                    setAnimation(death, false, 0.5f);
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
