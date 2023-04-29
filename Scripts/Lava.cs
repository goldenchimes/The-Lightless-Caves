using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    GameObject destination;

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = destination.transform.position;
    }
}
