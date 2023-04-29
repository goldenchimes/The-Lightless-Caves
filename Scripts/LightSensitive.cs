using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSensitive : MonoBehaviour
{
    [SerializeField]
    GameObject listener;
    [SerializeField]
    string enterMessage = "";
    [SerializeField]
    string stayMessage = "";
    [SerializeField]
    string exitMessage = "";

    void OnTriggerEnter(Collider other)
    {
        if (enterMessage.Length > 0)
        {
            listener.SendMessage(enterMessage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (stayMessage.Length > 0)
        {
            listener.SendMessage(stayMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (exitMessage.Length > 0)
        {
            listener.SendMessage(exitMessage);
        }
    }
}
