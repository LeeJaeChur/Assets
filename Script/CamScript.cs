using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class CamScript : MonoBehaviour
{
    static WebCamTexture cam; // 디바이스 카메라
                              // Camera MainCamera;

    // Use this for initialization

    //void Start()
    void Awake()
    {
        //MainCamera = GetComponent<Camera>();
        if (cam == null)
            cam = new WebCamTexture();
       // MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    
      //cam.GetPixel(MainCamera.pixelWidth, MainCamera.pixelHeight);
        transform.localScale = new Vector3((float)Screen.width*(-1), (float)Screen.height, 1);    //보이는 화면은 이것으로 해결. 저장하면 반전안된 원래이미지로 저장
        //transform.localScale = new Vector3((float)Screen.width, (float)Screen.height, 1);
       
        //GetComponent<Renderer>().material.mainTexture = cam;
      
        this.GetComponent<MeshRenderer>().material.mainTexture = cam;

        if (!cam.isPlaying)
            cam.Play();
    }

    // For photo varibles
    void OnGUI()
    {
        // 버튼 UI를 코드상으로 생성한 후 버튼 이벤트(TakeSnapshot()) 지정
        //if (GUI.Button(new Rect(10, 70, 120, 30), "TakeAndSave"))
        //    TakeSnapshot();

    }

    // 지정한 경로( _savepath)에 PNG 파일형식으로 저장합니다.
    //private string _SavePath = @"C:\TEST\"; //경로 바꾸세요!

    private string _SavePath = @"C:\test\";//경로 바꾸세요!
    int _CaptureCounter = 0; // 파일명을 위한
    string Today = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

    void TakeSnapshot()
    {

        Texture2D snap = new Texture2D(cam.width, cam.height);
        //  Texture2D snap = new Texture2D(MainCamera.pixelWidth, MainCamera.pixelHeight);
        Debug.Log(cam.GetPixels());
        snap.SetPixels(cam.GetPixels());
        // Color[] color = new Color[MainCamera.pixelHeight * MainCamera.pixelWidth];
        //snap.SetPixels(0,0, MainCamera.pixelWidth, MainCamera.pixelHeight, color);

        // snap.SetPixels(0, 0, 100, 100, color);
        snap.Apply();
        //


        System.IO.File.WriteAllBytes(_SavePath + Today + "_" + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }

    private void Update()
    {
        //transform.rotation = baseRotation * Quaternion.AngleAxis(cam.videoRotationAngle, Vector3.up);
        //esc누르면 나가기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
