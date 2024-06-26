using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerRigidbody2D;
    public float PlayerSpeed = 8f;
    public bool HitCheck = false;
    public bool isLive = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            float xSpeed = PlayerSpeed * xInput;
            float ySpeed = PlayerSpeed * yInput;

            Vector2 newVelocity = new Vector2(xSpeed, ySpeed);

            PlayerRigidbody2D.velocity = newVelocity;
        }
        else
        {
            PlayerRigidbody2D.velocity = Vector2.zero;
        }
    }

    void HitColor()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Wall")||(collision.gameObject.tag == "EnemyReady"))
        {
            HitCheck = true;
        }
    }
}
