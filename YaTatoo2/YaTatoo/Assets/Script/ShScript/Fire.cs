using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] Transform[] dotpos;
    [SerializeField] float speed = 3f;
    int dotnum = 0;

    public GameObject LightFactory;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = dotpos[dotnum].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    public void MovePath()
    {

        transform.position = Vector3.MoveTowards(transform.position, dotpos[dotnum].transform.position, speed * Time.deltaTime);

        if (transform.position == dotpos[dotnum].transform.position)
        {
            GameObject light = Instantiate(LightFactory);
            light.transform.position = dotpos[dotnum].transform.position;
           // Destroy(light, (float)1.5);
            dotnum++;
        }
        if (dotnum == 6)
        {
            dotnum = 0;
            
        }
        //fire의 위치가 dotpos의 위치와 같아졌을 때
        //효과 프리팹을 활성화하고 싶다.

    }
}
