using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;

public class Player : MonoBehaviour
{
    // variables for gameplay
    public GameObject playerCamera;
    public SkeletonAnimation sk;
    public AnimationReferenceAsset idle, walking, fightMelee, fightRanged, death;
    public enum states { IDLE, WALK, FIGHTMELEE, FIGHTRANGED, DEATH };
    public states currentState;

    // variables for attack
    public GameObject projectileType;
    public int minimumDamage;
    public int maximumDamage;
    public float projectileForce;

    public float speed;
    private float dash;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        setWeapon(10, 20, 2.5f);
        dash = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Updated ist called fixed by time
    private void FixedUpdate()
    {
        // make a 3D Vector of Player Position and Z-Camera Position
        Vector3 currentCameraPosition = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
        playerCamera.transform.position = currentCameraPosition;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            GameObject currentProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 currentPosition = transform.position;
            Vector2 projectileDirection = (mousePosition - currentPosition).normalized;
            currentProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileForce;
            currentProjectile.GetComponent<Projectile>().damage = Random.Range(minimumDamage, maximumDamage);
            // Do attack animation
        }
    }

    public void OnDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            transform.Translate(dash * speed * Time.deltaTime * direction);
        }
    }

    // set the player weapon ans weapon stats
    public void setWeapon(int pMin, int pMax, float pForce)
    {
        minimumDamage = pMin;
        maximumDamage = pMax;
        projectileForce = pForce;
        Debug.Log(minimumDamage);
    }

    // move the player in game
    private void MovePlayer()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
            currentState = states.WALK;
            setCharacterState();
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
