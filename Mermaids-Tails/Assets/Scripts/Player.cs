using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables for gameplay
    private int maximumHP;
    private int currentHP;
    private int currentScore;

    // variables for attack
    public GameObject projectileType;
    private float minimumDamage;
    private float maximumDamage;
    private float projectileForce;

    // variables for movement
    public float speed;
    private Vector2 direction;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        setWeapon(10, 20, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (direction.x != 0 || direction.y != 0)
        {
            playerAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            playerAnimator.SetLayerWeight(1, 0);
        }

        MovePlayer();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentProjectile = Instantiate(projectileType, transform.position, Quaternion.identity);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currentPosition = transform.position;
            Vector2 projectileDirection = (mousePosition - currentPosition).normalized;
            currentProjectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileForce;
            currentProjectile.GetComponent<Projectile>().damage = Random.Range(minimumDamage, maximumDamage);
        }
    }

    private void setWeapon(float pMin, float pMax, float pForce)
    {
        minimumDamage = pMin;
        maximumDamage = pMax;
        projectileForce = pForce;
    }

    // move the player in game
    private void MovePlayer()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        ChangeAnimationDirection(direction);
    }

    // get player input
    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }

    // Change the direction of animation in the animation class
    private void ChangeAnimationDirection(Vector2 pDirection)
    {
        playerAnimator.SetFloat("XDirection", pDirection.x);
        playerAnimator.SetFloat("YDirection", pDirection.y);
    }
}
