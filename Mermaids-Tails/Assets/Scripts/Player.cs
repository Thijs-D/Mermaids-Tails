using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        ChangeAnimationDirection(direction);
    }

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

    private void ChangeAnimationDirection(Vector2 pDirection)
    {
        playerAnimator.SetFloat("XDirection", pDirection.x);
        playerAnimator.SetFloat("YDirection", pDirection.y);
    }
}
