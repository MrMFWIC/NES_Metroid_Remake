using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    HUDManager HUD;

    public enum Pickups
    {
        Powerup,
        Life,
        Score
    }

    public Pickups currentPickup;

    public AudioClip pickupSFX;

    void Start()
    {
        HUD = GetComponent<HUDManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayer = collision.gameObject.GetComponent<PlayerController>();
            AudioSourceManager sfxManager = collision.gameObject.GetComponent<AudioSourceManager>();

            switch (currentPickup)
            {
                case Pickups.Life:
                    GameManager.instance.lives++;
                    break;
                case Pickups.Powerup:
                    curPlayer.StartJumpForceChange();
                    break;
                case Pickups.Score:
                    GameManager.instance.score++;
                    break;
            }

            sfxManager.Play(pickupSFX, false);

            Destroy(gameObject);
        }
    }
}