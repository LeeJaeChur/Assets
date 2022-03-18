using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test_capture : MonoBehaviour
{
    public GameObject form;
   // public GameObject Size;
    //RectTransform rectT;

    //RectTransform rectS;

    //float pivot;
    //public float wtf = 30;//160;

    private string _SavePath = @"C:\test\"; //경로​
    int _CaptureCounter = 0; // 파일명을 위한​

    // Start is called before the first frame update
    void Start()
    {  
        //rectT = Pos.GetComponent<RectTransform>();
       // rectS = Size.GetComponent<RectTransform>();
        //pivot = Size.transform.localScale.y / 2;//y축 피봇
        //rectT.anchoredPosition = Camera.main.WorldToScreenPoint(Pos.GetComponent<RectTransform>().anchoredPosition);

    }
    void Update()
    {
        
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
        //Debug.Log("Size.transform.localScale.y : " + Size.transform.localScale.y);
        //Debug.Log("Pos.GetComponent<RectTransform>().anchoredPosition : " + Pos.GetComponent<RectTransform>().anchoredPosition);
        //Debug.Log("Camera.main.WorldToScreenPoint(Pos.GetComponent<RectTransform>().anchoredPosition) : " + Camera.main.WorldToScreenPoint(Pos.GetComponent<RectTransform>().anchoredPosition));
        //Debug.Log("pivot : " + pivot);
        //Debug.Log("rectT.anchoredPosition : " + rectT.anchoredPosition);
        //Debug.Log("rectT.anchoredPositionAll : " + (rectT.anchoredPosition.y + (pivot + wtf)));

        yield return new WaitForEndOfFrame();

        Texture2D snap = new Texture2D((int)form.transform.localScale.x, (int)form.transform.localScale.y, TextureFormat.RGB24, false);
        //snap.ReadPixels(new Rect(rectT.anchoredPosition.x, rectT.anchoredPosition.y + (pivot + wtf), Size.transform.localScale.x, Size.transform.localScale.y), 0, 0, true); //영역 지정
        snap.ReadPixels(new Rect(form.GetComponent<RectTransform>().anchoredPosition.x, form.GetComponent<RectTransform>().anchoredPosition.y, form.transform.localScale.x, form.transform.localScale.y), 0, 0, true); //영역 지정

        //snap.SetPixels(a.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }
}

