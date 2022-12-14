using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;

    public AudioClip impactSFX;
    
    void Start()
    {
        if (lifetime <= 0)
        { 
            lifetime = 2.0f;
        } 

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Pickup")
        {
            Destroy(this.gameObject);
        }
    }
}