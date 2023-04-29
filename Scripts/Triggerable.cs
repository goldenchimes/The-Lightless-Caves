using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
    [SerializeField]
    int triggersRequired = 1;

    int lit = 0;

    void OnLit()
    {
        lit++;
        Trigger();
    }

    void OnUnlit()
    {
        lit--;
        Trigger();
    }

    void Trigger()
    {
        if (lit < triggersRequired)
        {
            SendMessage("Untriggerable");
        }
        else
        {
            SendMessage("Triggerable");
        }
    }
}
