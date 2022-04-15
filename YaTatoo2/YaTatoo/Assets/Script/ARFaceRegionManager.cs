using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine;
using Unity.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARFaceRegionManager : MonoBehaviour
{
    [System.Serializable]
    public class TatooInfo
    {
        public string name;
        public GameObject Object;
    }
    public TatooInfo[] infos;
    GameObject model;
    ARFaceManager arFaceManager;
    GameObject placedModel;
    // ARRaycastManager arRaycastManager;
    GameObject facePrefab;
    bool isAnimation = false;
    ARFace arface;
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        arface = GameObject.FindObjectOfType<ARFace>();
        print(arface);

#else
        arFaceManager = GetComponent<ARFaceManager>();
#endif
        model = infos[0].Object;
        arFaceManager = GetComponent<ARFaceManager>();
        toggle.onValueChanged.AddListener((value) =>
       {
           MyListener(value);
       });
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            print("111");
            //터치한 것이 타투이미지라면
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("FaceMask"))
                {
                    //ARFace컴포넌트를 가진 오브젝트를 찾고 싶다.
                    arface = GameObject.FindObjectOfType<ARFace>();
                    //오브젝트가 존재한다면
                    if (arface)
                    {
                        //타투 이미지를 생성하고 싶다.
                        placedModel = Instantiate(model);
                        //생성된 오브젝트를 faceMask 프리팹의 자식으로 한다.
                        placedModel.transform.parent = arface.transform;
                        //생성된 오브젝트의 위치를 레이가 부딪힌 지점으로 하고 싶다.
                        placedModel.transform.position = hitInfo.point;
                        placedModel.transform.rotation = hitInfo.transform.rotation;
                    }
                }
            }
        }

#else
        // 화면을 터치했다면
        if (Input.touchCount > 0)
        {
            //터치를 누르는 순간
            Touch touch = Input.GetTouch(0); //가장 먼저 눌린 터치
            if (touch.phase == TouchPhase.Began)
            {
                //터치한 화면상의 점에서 레이를 쏜다.
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo;
                //레이에 부딪힌 것이 있다면 hitInfo에 저장한다.
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //부딪힌 것이 FaceMask라면
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("FaceMask"))
                    {
                        //ARFace컴포넌트를 가진 오브젝트를 찾고 싶다.
                        arface = GameObject.FindObjectOfType<ARFace>();
                        //오브젝트가 존재한다면
                        if (arface)
                        {
                            //타투 이미지를 생성하고 싶다.
                            placedModel = Instantiate(model);
                            //생성된 오브젝트를 faceMask 프리팹의 자식으로 한다.
                            placedModel.transform.parent = arface.transform;
                            //생성된 오브젝트의 위치를 레이가 부딪힌 지점으로 하고 싶다.
                            placedModel.transform.position = hitInfo.point;
                            placedModel.transform.rotation = hitInfo.transform.rotation;
                        }
                    }
                }
            }
        }
#endif
        if (EventSystem.current.currentSelectedGameObject) return;

    }

    private void MyListener(bool value)
    {
        if (arface == null)
        {
            arface = GameObject.FindObjectOfType<ARFace>();
            if (arface == null)
            {
                return;
            }
        }

        GameObject mask = arface.transform.gameObject;
        Transform[] parent = mask.GetComponentsInChildren<Transform>();

        if (value)
        {
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i].Find("Ani"))
                {

                    parent[i].Find("Ani").gameObject.SetActive(true);
                    isAnimation = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i].Find("Ani"))
                {
                    parent[i].Find("Ani").gameObject.SetActive(false);
                    isAnimation = false;
                }
            }
        }
    }
    public void OnChangeBtn(string name)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if (name == infos[i].name)
            {
                model = infos[i].Object;
            }
        }
    }
    // public void OnAnimationBtn()
    // {

    //     if (arface == null)
    //     {
    //         arface = GameObject.FindObjectOfType<ARFace>();
    //         if (arface == null)
    //         {
    //             return;
    //         }
    //     }

    //     GameObject mask = arface.transform.gameObject;
    //     Transform[] parent = mask.GetComponentsInChildren<Transform>();

    //     if (isAnimation)
    //     {

    //         for (int i = 0; i < parent.Length; i++)
    //         {
    //             if (parent[i].Find("Ani"))
    //             {
    //                 parent[i].Find("Ani").gameObject.SetActive(false);
    //                 isAnimation = false;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         for (int i = 0; i < parent.Length; i++)
    //         {
    //             print(parent[i]);
    //             if (parent[i].Find("Ani"))
    //             {

    //                 parent[i].Find("Ani").gameObject.SetActive(true);
    //                 isAnimation = true;
    //             }
    //         }
    //     }
    // }
}
