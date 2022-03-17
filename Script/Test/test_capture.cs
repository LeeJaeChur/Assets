using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test_capture : MonoBehaviour
{
    public GameObject Pos;
    public GameObject Size;
    RectTransform rectT;

    RectTransform rectS;

    float pivot;

    private string _SavePath = @"C:\test\"; //경로​
    int _CaptureCounter = 0; // 파일명을 위한​

    // Start is called before the first frame update
    void Start()
    {  
        rectT = Pos.GetComponent<RectTransform>();
        rectS = Size.GetComponent<RectTransform>();
        pivot = Size.transform.localScale.y / 2;//y축 피봇
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 120, 30), "TakeAndSave"))
        {
            StartCoroutine("TakeSnapshot");
        }

    }

    public IEnumerator TakeSnapshot()
    {
        
        yield return new WaitForEndOfFrame();

        Texture2D snap = new Texture2D((int)rectS.transform.localScale.x, (int)rectS.transform.localScale.y, TextureFormat.RGB24, false);
        snap.ReadPixels(new Rect(rectT.anchoredPosition.x, rectT.anchoredPosition.y + (pivot + 30), Size.transform.localScale.x, Size.transform.localScale.y), 0, 0, true); //영역 지정

        //snap.SetPixels(a.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }
}

