using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void GetFlashlight(GameObject flashlight)
    {
        player.SendMessage("GetFlashlight", flashlight);
    }

    void GetFlare(GameObject flare)
    {
        player.SendMessage("GetFlare", flare);
    }

    void GetFloodlight(GameObject floodlight)
    {
        player.SendMessage("GetFloodlight", floodlight);
    }
}
