using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyTurret : Enemy
{
    CircleCollider2D cc;

    public float projectileFireRate;
    public float _radius;

    public GameObject player;
    Attack attackScript;

    float timeSinceLastFire;

    public float Radius
    {
        get { return _radius; }
        set
        {
            _radius = value;

            if (_radius < 1.0f)
            {
                _radius = 1.0f;
            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        cc = GetComponent<CircleCollider2D>();
        cc.radius = Radius;

        if (Radius <= 0)
        {
            Radius = 1.5f;
        }

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Fire");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("FireExit");
        }
    }
}