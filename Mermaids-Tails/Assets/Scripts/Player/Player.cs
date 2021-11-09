using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // variables for gameplay
    public GameObject playerCamera;

    // variables for attack
    public GameObject projectileType;
    public int minimumDamage;
    public int maximumDamage;
    public float projectileForce;

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
        setWeapon(10, 20, 2.5f);
        dash = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.Translate(direction * speed * dash * Time.deltaTime);
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
        ChangeAnimationDirection(direction);
    }

    // Change the direction of animation in the animation class
    private void ChangeAnimationDirection(Vector2 pDirection)
    {
        playerAnimator.SetFloat("XDirection", pDirection.x);
        playerAnimator.SetFloat("YDirection", pDirection.y);
    }
}
