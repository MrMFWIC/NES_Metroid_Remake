using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSourceManager))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    protected AudioSourceManager sfxManager;

    protected int _health;
    public int maxHealth;

    public AudioClip deathSFX;

    public int health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > maxHealth)
            {
                health = maxHealth;
            }

            if (_health <= 0)
            {
                Death();
            }
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sfxManager = GetComponent<AudioSourceManager>();

        if (maxHealth <= 0)
        {
            maxHealth = 5;
        }

        health = maxHealth;
    }

    public virtual void Death()
    {
        sfxManager.Play(deathSFX, false);

        anim.SetTrigger("Death");
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void DestroyMyself()
    {
        Destroy(gameObject);
    }
}
