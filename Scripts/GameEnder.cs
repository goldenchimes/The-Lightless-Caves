using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    GameObject message;
    [SerializeField]
    GameObject listener;

    void OnTriggerEnter(Collider other)
    {
        message.SetActive(true);
        listener.SendMessage("Won");
    }
}
