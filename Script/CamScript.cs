using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class CamScript : MonoBehaviour
{
    static WebCamTexture cam; // ����̽� ī�޶�
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
        transform.localScale = new Vector3((float)Screen.width*(-1), (float)Screen.height, 1);    //���̴� ȭ���� �̰����� �ذ�. �����ϸ� �����ȵ� �����̹����� ����
        //transform.localScale = new Vector3((float)Screen.width, (float)Screen.height, 1);
       
        //GetComponent<Renderer>().material.mainTexture = cam;
      
        this.GetComponent<MeshRenderer>().material.mainTexture = cam;

        if (!cam.isPlaying)
            cam.Play();
    }

    // For photo varibles
    void OnGUI()
    {
        // ��ư UI�� �ڵ������ ������ �� ��ư �̺�Ʈ(TakeSnapshot()) ����
        //if (GUI.Button(new Rect(10, 70, 120, 30), "TakeAndSave"))
        //    TakeSnapshot();

    }

    // ������ ���( _savepath)�� PNG ������������ �����մϴ�.
    //private string _SavePath = @"C:\TEST\"; //��� �ٲټ���!

    private string _SavePath = @"C:\test\";//��� �ٲټ���!
    int _CaptureCounter = 0; // ���ϸ��� ����
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
        //esc������ ������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
