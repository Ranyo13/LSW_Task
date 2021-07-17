using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody;
    Animator anim;
    bool faceRight = true;
    [SerializeField]
    private int movementSpeed;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.MovePosition((Vector2)transform.position + new Vector2(0, 1) * Time.deltaTime * movementSpeed);
            anim.SetBool("IsMoving", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.MovePosition((Vector2)transform.position + new Vector2(0, -1) * Time.deltaTime * movementSpeed);
            anim.SetBool("IsMoving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.MovePosition((Vector2)transform.position + new Vector2(1, 0) * Time.deltaTime * movementSpeed);
            anim.SetBool("IsMoving", true);
            if(!faceRight)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                faceRight = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.MovePosition((Vector2)transform.position + new Vector2(-1, 0) * Time.deltaTime * movementSpeed);
            anim.SetBool("IsMoving", true);
            if (faceRight)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                faceRight = false;
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }
}
