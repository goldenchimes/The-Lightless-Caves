using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounder : MonoBehaviour
{
    [SerializeField]
    GameObject listener;

    void OnTriggerEnter()
    {
        Notify();
    }

    void OnTriggerStay()
    {
        Notify();
    }

    void Notify()
    {
        listener.SendMessage("OnGround");
    }
}
