using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMover : MonoBehaviour
{
    [SerializeField]
    Transform bounds;
    [SerializeField]
    float speed = 1.0f;

    Vector3 origin = Vector3.zero;
    Vector3 start = Vector3.zero;
    Vector3 target = Vector3.zero;
    float delta = 0.0f;
    bool go = false;

    void Awake()
    {
        origin = transform.position;
        start = bounds.position;
        target = origin;
    }

    void FixedUpdate()
    {
        if (go && transform.position != target)
        {
            delta += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, delta);
        }
        go = true;
    }

    void OnStop()
    {
        if (enabled)
        {
            go = false;
            if (transform.position == target)
            {
                Vector3 oldStart = start;
                start = target;
                target = oldStart;
                delta = (transform.position - start).magnitude / (target - start).magnitude;
            }
        }
    }

    void Triggerable()
    {
        enabled = true;
    }

    void Untriggerable()
    {
        enabled = false;
    }
}
