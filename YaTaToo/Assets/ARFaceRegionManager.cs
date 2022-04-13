using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine;
using Unity.Collections;

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
    // Start is called before the first frame update
    void Start()
    {
        model = infos[0].Object;
        // arRaycastManager = GetComponent<ARRaycastManager>();
        arFaceManager = GetComponent<ARFaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        facePrefab = arFaceManager.facePrefab;
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
                        ARFace arface = GameObject.FindObjectOfType<ARFace>();
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
}
