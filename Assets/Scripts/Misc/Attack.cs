using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSourceManager sfxManager;

    public float projectileSpeed;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Transform spawnPoint;
    public Projectile projectilePrefab;
    public TurretProjectile turretProjectilePrefab;

    public AudioClip attackSFX;
    public AudioClip enemyShootSFX;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sfxManager = GetComponent<AudioSourceManager>();

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

        sfxManager.Play(attackSFX, false);
    }

    public void TurretFire()
    {
        if (!sr.flipX)
        {
            TurretProjectile curProjectile = Instantiate(turretProjectilePrefab,
                spawnPoint.position, spawnPoint.rotation);
            curProjectile.speed = -projectileSpeed;
        }
        else if (sr.flipX)
        {
            TurretProjectile curProjectile = Instantiate(turretProjectilePrefab,
                spawnPoint.position, spawnPoint.rotation);
            curProjectile.speed = projectileSpeed;
        }

        sfxManager.Play(enemyShootSFX, false);
    }
}