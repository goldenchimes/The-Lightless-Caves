using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.isKinematic = false;
    }

    void OnCollisionEnter()
    {
        rb.isKinematic = true;
    }
}
