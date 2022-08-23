using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Pickups
    {
        Powerup,
        Life,
        Score
    }

    public Pickups currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayer = collision.gameObject.GetComponent<PlayerController>();

            switch (currentPickup)
            {
                case Pickups.Life:
                    GameManager.instance.lives++;
                    break;
                case Pickups.Powerup:
                    curPlayer.StartGravityChange();
                    Debug.Log("Gravity change powerup collected, functionality not working");
                    break;
                case Pickups.Score:
                    curPlayer.score++;
                    break;
            }

            Destroy(gameObject);
        }
    }
}