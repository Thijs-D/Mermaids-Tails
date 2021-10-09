using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables for gameplay
    public GameObject playerCamera;

    // variables for attack
    public GameObject projectileType;
    private int minimumDamage;
    private int maximumDamage;
    private float projectileForce;

    // variables for movement
    private enum Facing { Up, Down, Left, Right };
    private Facing currentDirection = Facing.Down;
    public float speed;
    private float dash;
    private Vector2 direction;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        setWeapon(10, 20, 0.5f);
        dash = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        // if player is moving, the moving animation will displayed
        if (direction.x != 0 || direction.y != 0)
        {
            playerAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            playerAnimator.SetLayerWeight(1, 0);
        }

        MovePlayer();

        // 0 left click, 1 right click, 2 middle click
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

    // Updated ist called fixed by time
    private void FixedUpdate()
    {
        // make a 3D Vector of Player Position and Z-Camera Position
        Vector3 currentCameraPosition = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
        playerCamera.transform.position = currentCameraPosition;
    }

    // set the player weapon ans weapon stats
    private void setWeapon(int pMin, int pMax, float pForce)
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
            currentDirection = Facing.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            currentDirection = Facing.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            currentDirection = Facing.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            currentDirection = Facing.Right;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 targetPosition = Vector2.zero;
            if (currentDirection == Facing.Up)
            {
                targetPosition.y = 1;
            } 
            else if (currentDirection == Facing.Down)
            {
                targetPosition.y = -1;
            }
            else if (currentDirection == Facing.Left)
            {
                targetPosition.x = -1;
            }
            else if (currentDirection == Facing.Right)
            {
                targetPosition.x = 1;
            }
            transform.Translate(targetPosition * dash);
        }
    }

    // Change the direction of animation in the animation class
    private void ChangeAnimationDirection(Vector2 pDirection)
    {
        playerAnimator.SetFloat("XDirection", pDirection.x);
        playerAnimator.SetFloat("YDirection", pDirection.y);
    }
}
