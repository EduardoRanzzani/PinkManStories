using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  private Rigidbody2D rigidbody2D;
  private Animator animator;

  public float speed;
  public Transform rightColision;
  public Transform leftColision;
  public Transform headPoint;
  public LayerMask layer;

  private bool colliding;
  public BoxCollider2D boxCollider2D;
  public CircleCollider2D circleCollider2D;

  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    colliding = Physics2D.Linecast(rightColision.position, leftColision.position, layer);

    if (colliding)
    {
      speed *= -1f;
      transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
    }
  }

  bool playerDestroyed;
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      float height = collision.contacts[0].point.y - headPoint.position.y;

      if (height > 0 && !playerDestroyed)
      {
        Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
        player.AddForce(Vector2.up * 10);
        speed = 0;

        animator.SetTrigger("Die");

        boxCollider2D.enabled = false;
        circleCollider2D.enabled = false;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        Destroy(gameObject, 0.33f);
      }
      else
      {
        playerDestroyed = true;
        PlayerController.instance.PlayerDie();
      }
    }
  }
}
