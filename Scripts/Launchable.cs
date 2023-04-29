using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchable : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Launch(Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
