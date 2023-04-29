using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    GameObject message;

    void OnTriggerEnter(Collider other)
    {
        message.SetActive(true);
    }
}
