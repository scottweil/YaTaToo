using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public Transform[] p;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = 4;
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, p[i].position);
        }
    }
}
