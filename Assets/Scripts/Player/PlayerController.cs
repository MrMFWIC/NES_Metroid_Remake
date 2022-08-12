using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed = 5.0f;
    public int jumpForce = 300;
    public bool isGrounded;
    public LayerMask isGroundlayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;

    Coroutine gravityChange;

    private int _lives = 3;
    public int maxLives = 5;

    public int lives
    {
        get { return _lives; }
        set
        {
            /*if (_lives > value)
            {
                Lost a life - Respawn
            }*/

            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            /*if (_lives < 0)
            {
                Game Over
            }*/

            Debug.Log("Lives are set to: " + lives.ToString());
        }
    }

    private int _score = 0;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;

            Debug.Log("Your current score is: " + score.ToString());
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 420;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundlayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (isGrounded)
        {
            rb.gravityScale = 1;
        }

        anim.SetFloat("MoveValue", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (hInput != 0)
        {
            sr.flipX = (hInput < 0);
        }

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Attack" && isGrounded)
                anim.SetTrigger("Attack");
            else if (curPlayingClip[0].clip.name == "Attack")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire2") && curPlayingClip[0].clip.name != "Ball" && curPlayingClip[0].clip.name != "BallExplosion" && curPlayingClip[0].clip.name != "Transform" && isGrounded)
            {
                anim.SetTrigger("Ball");
            }
        }

        if (curPlayingClip[0].clip.name == "Ball")
        {
            speed = 7.5f;
            jumpForce = 1;
        }
        else
        {
            speed = 5.0f;
            jumpForce = 420;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);

        if (collision.gameObject.tag != "Player" && curPlayingClip[0].clip.name == "Ball") 
        {
            anim.SetTrigger("Impact");
        }
    }

    public void StartGravityChange()
    {
        if (gravityChange == null)
        {
            gravityChange = StartCoroutine(GravityChange());
        }
        else
        {
            StopCoroutine(gravityChange);
            gravityChange = null;
            rb.gravityScale *= 2;
            gravityChange = StartCoroutine(GravityChange());
        }
    }

    IEnumerator GravityChange()
    {
        rb.gravityScale /= 2;

        yield return new WaitForSeconds(8.0f);

        rb.gravityScale *= 2;
        gravityChange = null;
    }
}