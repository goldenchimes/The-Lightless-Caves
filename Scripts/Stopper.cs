using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    FixedJoint joint;

    void OnEnable()
    {
        Destroy(joint);
    }

    void OnCollisionEnter(Collision collision)
    {
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = collision.rigidbody;
    }
}
