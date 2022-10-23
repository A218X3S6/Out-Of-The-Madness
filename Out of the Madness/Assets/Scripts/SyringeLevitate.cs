using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeLevitate : MonoBehaviour
{
    [SerializeField] float frequency;
    [SerializeField] float amplitude;

    Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * frequency) * amplitude + initPos.y, 0);
    }
}
