using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    GameObject listener;
    [SerializeField]
    Color triggeredColor;

    Renderer r;
    Color untriggeredColor;

    bool lit = false;
    bool litTriggered = false;
    bool unlitTriggered = true;

    void Awake()
    {
        r = GetComponent<Renderer>();
        untriggeredColor = r.material.color;
    }

    void FixedUpdate()
    {
        if (lit)
        {
            r.material.color = triggeredColor;
        }
        else
        {
            r.material.color = untriggeredColor;
        }
        if (lit)
        {
            unlitTriggered = false;
            if (!litTriggered)
            {
                listener.SendMessage("OnLit");
                litTriggered = true;
            }
        }
        else
        {
            litTriggered = false;
            if (!unlitTriggered)
            {
                listener.SendMessage("OnUnlit");
                unlitTriggered = true;
            }
        }
        lit = false;
    }

    void OnTriggerEnter()
    {
        lit = true;
    }

    void OnTriggerStay()
    {
        lit = true;
    }
}
