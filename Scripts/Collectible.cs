using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    string message;
    [SerializeField]
    GameObject payload;

    void OnTriggerEnter(Collider other)
    {
        other.SendMessage(message, payload);
        Destroy(this);
    }
}
