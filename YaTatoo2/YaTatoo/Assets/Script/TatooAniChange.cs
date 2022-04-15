using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class TatooAniChange : MonoBehaviour
{
    public VideoClip[] vids;
    public VideoPlayer videoPlayer;
    public GameObject aniChangebar;
    GameObject aniBar;
    bool isUI = false;
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            print("111");
            //터치한 것이 타투이미지라면
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            LayerMask layermask = 1 << LayerMask.NameToLayer("Tatoo");
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layermask))
            {
                {
                    if (isUI)
                    {
                        Destroy(aniBar);
                        isUI = false;
                    }
                    else
                    {
                        //애니메이션을 바꿀 수 있는 UI를 띄우고 싶다.
                        aniBar = Instantiate(aniChangebar);
                        aniBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
                        TopGorilla gorilla = aniBar.GetComponent<TopGorilla>();
                        for (int i = 0; i < gorilla.buttons.Length; i++)
                        {
                            int index = i;
                            gorilla.buttons[i].onClick.AddListener(delegate { OnClickAniChange(index); });
                        }
                        isUI = true;
                    }
                }
            }
        }
#else
       //화면을 터치했을 때 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                    //터치한 것이 타투이미지라면
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hitInfo;
                   LayerMask layermask = 1 << LayerMask.NameToLayer("Tatoo");
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layermask))
            {
                {
                    if (isUI)
                    {
                        Destroy(aniBar);
                        isUI = false;
                    }
                    else
                    {
                        //애니메이션을 바꿀 수 있는 UI를 띄우고 싶다.
                        aniBar = Instantiate(aniChangebar);
                        aniBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
                        TopGorilla gorilla = aniBar.GetComponent<TopGorilla>();
                        for (int i = 0; i < gorilla.buttons.Length; i++)
                        {
                            int index = i;
                            gorilla.buttons[i].onClick.AddListener(delegate { OnClickAniChange(index); });
                        }
                        isUI = true;
                    }
                }
            }
                
            }
        }
#endif
        if (EventSystem.current.currentSelectedGameObject) return;
    }
    public void OnClickAniChange(int index)
    {
        print(index);
        videoPlayer.clip = vids[index];
    }
}
