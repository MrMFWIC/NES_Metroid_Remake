using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    SpriteRenderer sr;

    public float projectileSpeed;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectile projectilePrefab;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
        }
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab,
                spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab,
                spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
        }
    }
}