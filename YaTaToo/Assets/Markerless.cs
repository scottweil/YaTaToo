using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Markerless : MonoBehaviour
{

    public GameObject indicator;
    public GameObject model;
    GameObject placedModel;
    ARRaycastManager arRaycastManager;
    public GameObject floor;
    bool isIndicator = true;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        floor.SetActive(true);
#else
        floor.SetActive(false);
#endif
        indicator.SetActive(false);
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateIndicator();
        UpdateMakeModel();

#else
        UpdateIndicatorForAndroid();
        UpdateMakeModelForAndroid();
#endif
    }
    private void UpdateMakeModel()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Touch touch = Input.GetTouch(0);Input.touchCount > 0 || 
            // if (touch.phase == TouchPhase.Began)
            // {
            //클릭한 위치를 기준으로 Ray를 만들고
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                {
                    isIndicator = false;
                    placedModel = Instantiate(model, hitInfo.transform.position, hitInfo.transform.rotation);
                }
            }
            // if (placedModel == null)
            // {
            //     placedModel = Instantiate(model, indicator.transform.position, indicator.transform.rotation);
            // }
            // else
            // {
            //     placedModel.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
            // }
            //}
        }
    }

    void UpdateIndicator()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        int layermask = ~(1 << LayerMask.NameToLayer("Indicator"));
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layermask) && isIndicator)
        {
            indicator.SetActive(true);
            indicator.transform.position = hitInfo.point + hitInfo.normal * 0.1f;
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void UpdateMakeModelForAndroid()
    {
        // 화면을 터치했다면
        if (Input.touchCount > 0)
        {
            //터치를 누르는 순간
            Touch touch = Input.GetTouch(0); //가장 먼저 눌린 터치
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Indicator"))
                    {
                        isIndicator = false;
                        placedModel = Instantiate(model, hitInfo.transform.position, hitInfo.transform.rotation);
                    }
                }
            }
            // if (touch.phase == TouchPhase.Began)
            // {
            //     if (placedModel == null)
            //     {
            //         placedModel = Instantiate(model, indicator.transform.position, indicator.transform.rotation);
            //     }
            //     else
            //     {
            //         placedModel.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
            //     }
            // }
        }
    }

    void UpdateIndicatorForAndroid()
    {
        //arRaycastManager를 이용해서 Raycast를 하고
        Vector2 screen = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

        if (arRaycastManager.Raycast(screen, hitResults) && isIndicator)
        {
            indicator.SetActive(true);
            indicator.transform.position = hitResults[0].pose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}






// ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem)arFaceManager.subsystem;
// foreach (ARFace face in arFaceManager.trackables)
// {
//     subsystem.GetRegionPoses(face.trackableId, Unity.Collections.Allocator.Persistent, ref faceRegions);
//     foreach (ARCoreFaceRegionData faceRegion in faceRegions)
//     {
//         ARCoreFaceRegion regionType = faceRegion.region;
// if (regionType == ARCoreFaceRegion.NoseTip)
// {
//     if (!noseObject)
//     {
//         noseObject = Instantiate(nosePrefabs, sessionOrigin.trackablesParent);
//     }
//     noseObject.transform.localPosition = faceRegion.pose.position;
//     noseObject.transform.localRotation = faceRegion.pose.rotation;
// }
// else if (regionType == ARCoreFaceRegion.ForeheadLeft)
// {
//     if (!leftHeadObject)
//     {
//         leftHeadObject = Instantiate(leftHeadPrefabs, sessionOrigin.trackablesParent);
//     }
//     leftHeadObject.transform.localPosition = faceRegion.pose.position;
//     leftHeadObject.transform.localRotation = faceRegion.pose.rotation;
// }
// else if (regionType == ARCoreFaceRegion.ForeheadRight)
// {
//     if (!rightHeadObject)
//     {
//         rightHeadObject = Instantiate(rightHeadPrefabs, sessionOrigin.trackablesParent);
//     }
//     rightHeadObject.transform.localPosition = faceRegion.pose.position;
//     rightHeadObject.transform.localRotation = faceRegion.pose.rotation;
// }
//}