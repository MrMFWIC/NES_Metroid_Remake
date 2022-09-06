using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    AudioSourceManager sfxManager;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    readonly CanvasManager cv;

    public float speed = 5.0f;
    public int jumpForce = 300;
    public bool isGrounded;
    public LayerMask isGroundlayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;
    public bool isPoweredUp = false;

    public AudioClip jumpSFX;
    public AudioClip ballSFX;
    public AudioClip respawnSFX;
    public AudioClip ballImpactSFX;

    Coroutine jumpForceChange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sfxManager = GetComponent<AudioSourceManager>();

        sfxManager.Play(respawnSFX, false);

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
        if (Time.timeScale == 0)
        {
            return;
        }

        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundlayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            sfxManager.Play(jumpSFX, false);
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
            {
                anim.SetTrigger("Attack");
            }
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
                sfxManager.Play(ballSFX, false);
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
            if (!isPoweredUp)
                jumpForce = 420;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);

        if (collision.gameObject.tag != "Player" && curPlayingClip[0].clip.name == "Ball" && collision.gameObject.tag != "Pickup") 
        {
            anim.SetTrigger("Impact");
            sfxManager.Play(ballImpactSFX, false);
        }

        if (collision.gameObject.tag == "Lava")
        {
            GameManager.instance.lives--;
        }
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            isPoweredUp = false;
            jumpForce = 420;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        isPoweredUp = true;
        jumpForce = 600;

        yield return new WaitForSeconds(8.0f);

        isPoweredUp = false;
        jumpForce = 420;
        jumpForceChange = null;
    }

    public void CallRespawnSound()
    {
        sfxManager.Play(respawnSFX, false);
    }
}