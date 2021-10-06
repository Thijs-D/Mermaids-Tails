using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables for gameplay
    private int maximumHP;
    private int currentHP;
    private int currentScore;

    // variables for movement
    public float speed;
    private Vector2 direction;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
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
