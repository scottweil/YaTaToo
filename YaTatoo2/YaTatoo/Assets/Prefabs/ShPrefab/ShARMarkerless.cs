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
        //����ī�޶� ��ġ���� ī�޶��� �չ������� Ray�� �����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //�ε��� ���� �ִٸ� �װ��� �ε������͸� ��ġ�ϰ� �ʹ�
        RaycastHit hitinfo;
        //Ray�� �ٶ� �� Indicator ���̾ �����ϰ� �ʹ�.
        int layerMask = ~(1 << LayerMask.NameToLayer("Indicator"));
        if (Physics.Raycast(ray, out hitinfo, float.MaxValue, layerMask))
        {
            indicator.transform.position = hitinfo.point + hitinfo.normal * 0.001f;
        }
    }
    void UpdateMakeFlower()
    {
        //ȭ���� ��ġ/Ŭ������ �� 
        if (Input.GetButtonDown("Fire1"))
        {
            //2. Ŭ���� ��ġ�� �������� Ray�� �����
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //3. �ٷ� ���� �ε��� ���� �ε������Ͷ��
            RaycastHit hitinfo;
            //Ray�� �ٶ� �� Indicator ���̾ �����ϰ� �ʹ�.
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                {
                    GameObject flower = Instantiate(flowerFactory);
                    flower.transform.position = hitinfo.point;
                    flower.transform.up = hitinfo.normal;
                }
            }
            //�� ���� ���� ��ġ�ϰ� �ʹ�.
        }
    }

    void UpdateIndicatorForAndroid()
    {
        // aRRaycastManager�� �̿��ؼ� Raycast�� �ϰ�
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

        if (aRRaycastManager.Raycast(center, hitResults))
        {
            //�ε��� ���� �ִٸ� �װ��� �ε������͸� ��ġ�ϰ� �ʹ�
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
        //1. ȭ���� ��ġ�ϴ� ���� ((1)�հ����� 1�� �̻� ��ġ�� �Ͼ�ٸ�/(2)��ġ�� ������ �����̶��)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray;
                ray = Camera.main.ScreenPointToRay(touch.position);

                //3. �ٷ� ���� �ε��� ���� �ε������Ͷ��
                RaycastHit hitinfo;
                //Ray�� �ٶ� �� Indicator ���̾ �����ϰ� �ʹ�.
                if (Physics.Raycast(ray, out hitinfo))
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                    {
                        GameObject flower = Instantiate(flowerFactory);
                        flower.transform.position = hitinfo.point;
                        flower.transform.up = hitinfo.normal;
                    }
                }
                //�� ���� ���� ��ġ�ϰ� �ʹ�.
            }
            //2. Ŭ���� ��ġ�� �������� Ray�� �����

        }
    }
}
