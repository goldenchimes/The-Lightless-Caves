using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    [SerializeField]
    GameObject contents;

    void SetPercentage(float percentage)
    {
        Vector3 scale = contents.transform.localScale;
        scale.x = percentage;
        contents.transform.localScale = scale;
    }
}
