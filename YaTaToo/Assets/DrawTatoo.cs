using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DrawTatoo : MonoBehaviour
{
    ARTrackedImageManager aRTrackedImageManager;

    //유니티 lifecycle(Awake - enable - start )
    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OntrackedImagesChanged; //델리게이트, event driven
    }
    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OntrackedImagesChanged; //델리게이트
    }
    [System.Serializable]
    public class MarkerInfo
    {
        public string name;
        public GameObject Object;
    }
    public MarkerInfo[] infos;

    private void OntrackedImagesChanged(ARTrackedImagesChangedEventArgs arg)
    {
        var list = arg.updated;
        for (int i = 0; i < list.Count; i++)
        {
            var marker = list[i];
            for (int j = 0; j < infos.Length; j++)
            {
                //추적된 마커가 내가 알고 있는 목록에 있는 녀석이라면
                if (marker.referenceImage.name == "marker")
                {
                    if (marker.trackingState == TrackingState.Tracking)
                    {
                        // 그 마커에 해당하는 오브젝트를 그 위치에 배치하고 싶다.
                        infos[0].Object.SetActive(true);
                        infos[0].Object.transform.position = marker.transform.position;
                        //infos[0].Object.transform.rotation = marker.transform.rotation;
                    }
                    else
                    {
                        infos[j].Object.SetActive(false);
                    }
                }
            }
        }
    }
    void Start()
    {

    }


    void Update()
    {

    }
}
