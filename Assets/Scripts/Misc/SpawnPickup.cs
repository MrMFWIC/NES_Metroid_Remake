using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public GameObject[] pickupPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pickupPrefabs[Random.Range(0, 5)], transform.position, transform.rotation);
    }
}