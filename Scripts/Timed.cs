using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timed : MonoBehaviour
{
    [SerializeField]
    float life = 1.0f;

    void OnEnable()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
