using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    GameObject listener;

    void OnDestroy()
    {
        listener.SendMessage("OnGameStart");
    }
}
