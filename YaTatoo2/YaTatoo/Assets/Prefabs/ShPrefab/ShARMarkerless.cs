using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShARMarkerless : MonoBehaviour
{
    public GameObject indicator;
    public GameObject flowerFactory;
    ARRaycastManager aRRaycastManager;
    public GameObject face;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        face.SetActive(true);
#else
        face.SetActive(false);
#endif
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateIndicator();
        UpdateMakeFlower();
#else
        UpdateIndicatorForAndroid();
        UpdateMakeFlowerForAndroid();
#endif
    }
    void UpdateIndicator()
    {
        //메인카메라 위치에서 카메라의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //부딪힌 것이 있다면 그곳에 인디케이터를 배치하고 싶다
        RaycastHit hitinfo;
        //Ray로 바라볼 때 Indicator 레이어를 제외하고 싶다.
        int layerMask = ~(1 << LayerMask.NameToLayer("Indicator"));
        if (Physics.Raycast(ray, out hitinfo, float.MaxValue, layerMask))
        {
            indicator.transform.position = hitinfo.point + hitinfo.normal * 0.001f;
        }
    }
    void UpdateMakeFlower()
    {
        //화면을 터치/클릭했을 때 
        if (Input.GetButtonDown("Fire1"))
        {
            //2. 클릭한 위치를 기준으로 Ray를 만들고
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //3. 바로 보고 부딪힌 것이 인디케이터라면
            RaycastHit hitinfo;
            //Ray로 바라볼 때 Indicator 레이어를 제외하고 싶다.
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                {
                    GameObject flower = Instantiate(flowerFactory);
                    flower.transform.position = hitinfo.point;
                    flower.transform.up = hitinfo.normal;
                }
            }
            //그 위에 꽃을 배치하고 싶다.
        }
    }

    void UpdateIndicatorForAndroid()
    {
        // aRRaycastManager를 이용해서 Raycast를 하고
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

        if (aRRaycastManager.Raycast(center, hitResults))
        {
            //부딪힌 것이 있다면 그곳에 인디케이터를 배치하고 싶다
            indicator.SetActive(true);
            indicator.transform.position = hitResults[0].pose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
    void UpdateMakeFlowerForAndroid()
    {
        //1. 화면을 터치하는 순간 ((1)손가락이 1개 이상 터치가 일어났다면/(2)터치가 누르는 순간이라면)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray;
                ray = Camera.main.ScreenPointToRay(touch.position);

                //3. 바로 보고 부딪힌 것이 인디케이터라면
                RaycastHit hitinfo;
                //Ray로 바라볼 때 Indicator 레이어를 제외하고 싶다.
                if (Physics.Raycast(ray, out hitinfo))
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                    {
                        GameObject flower = Instantiate(flowerFactory);
                        flower.transform.position = hitinfo.point;
                        flower.transform.up = hitinfo.normal;
                    }
                }
                //그 위에 꽃을 배치하고 싶다.
            }
            //2. 클릭한 위치를 기준으로 Ray를 만들고

        }
    }
}
