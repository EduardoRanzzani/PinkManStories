using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D targetJoint2D;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        targetJoint2D = GetComponent<TargetJoint2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Falling", fallingTime);
        }      
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        targetJoint2D.enabled = false;
        boxCollider2D.isTrigger = true;
    }
}
