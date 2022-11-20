using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed;
  public float jumpForce;

  public bool isJumping = false;
  public bool isDoubleJump;
  private bool isBlowing = false;
  private int ordem = 0;

  private new Rigidbody2D rigidbody2D;
  private Animator animator;

  public static PlayerController instance;

  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    instance = this;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Jump();
  }

  void Move()
  {
    float horizontal = Input.GetAxis("Horizontal");
    rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);

    // Move o personagem em uma posição -- sem física
    // Vector3 movement = new(horizontal, 0f, 0f);
    // transform.position += speed * Time.deltaTime * movement;

    if (horizontal > 0f)
    {
      animator.SetBool("Walk", true);
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
    else if (horizontal < 0f)
    {
      animator.SetBool("Walk", true);
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
    else
    {
      animator.SetBool("Walk", false);
    }
  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump") && !isBlowing)
    {
      if (!isJumping)
      {
        isJumping = true;
        rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animator.SetBool("Jump", true);
        isDoubleJump = true;
      }
      else
      {
        if (isDoubleJump)
        {
          animator.SetTrigger("DoubleJump");
          rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
          isDoubleJump = false;
        }
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Ground"))
    {
      isJumping = false;
      animator.SetBool("Jump", false);
    }

    if (collision.gameObject.CompareTag("Spike"))
    {
      PlayerDie();
    }

    if (collision.gameObject.CompareTag("Saw"))
    {
      PlayerDie();
    }
  }

  // void OnCollisionExit2D(Collision2D collision)
  // {
  //   if (collision.gameObject.CompareTag("Ground"))
  //   {
  //     isJumping = true;
  //   }
  // }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Ground"))
    {
      isJumping = true;
    }

    if (collider.gameObject.CompareTag("Fan"))
    {
      isBlowing = false;
    }
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Fan"))
    {
      isBlowing = true;
    }
  }

  public void PlayerDie()
  {
    animator.SetTrigger("Die");
    GameController.instance.ShowGameOver();
    Destroy(gameObject, 0.33f);
  }
}
