using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public float sceneChangeTime = 3;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > sceneChangeTime)
        {
            SceneManager.LoadScene("ShCameraScene", LoadSceneMode.Single);
        }
    }
}
