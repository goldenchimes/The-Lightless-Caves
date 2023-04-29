using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBody : MonoBehaviour
{
    [SerializeField]
    GameObject robot;

    void GetFlashlight(GameObject flashlight)
    {
        robot.SendMessage("GetFlashlight", flashlight);
    }

    void GetFlare(GameObject flare)
    {
        robot.SendMessage("GetFlare", flare);
    }

    void GetFloodlight(GameObject floodlight)
    {
        robot.SendMessage("GetFloodlight", floodlight);
    }
}
