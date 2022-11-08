using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
  private Animator animator;
  public float jumpForce;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
      player.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
      animator.SetTrigger("Jump");
    }
  }


}
