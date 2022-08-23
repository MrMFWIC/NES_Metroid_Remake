using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(GameManager.instance.playerInstance.transform.position.x, GameManager.instance.playerInstance.transform.position.y - 1, transform.position.z);
    }
}