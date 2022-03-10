using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//(3/4)����
public class Fadein : MonoBehaviour
{
    public Image image;
    // public MainButton button = new MainButton();
    public bool shot = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        //   button = GameObject.Find("MainButton").GetComponent<MainButton>();
    }

    private void Start()
    {
        //Debug.Log("�־ȵ�!!");
       
            StartCoroutine(FadeCoroutine());
        
    }
    // Update is called once per frame
    void Update()
    {
        //fade = GameObject.Find("MainButton").GetComponent<MainButton>().mainfade;

        // Debug.Log(fade);
    }

    public void MainFade()
    {
        //StartCoroutine(FadeCoroutine());
    }

    //public void Call()
    //{
    //    Debug.Log("�־ȵ�!!");
    //}

    public IEnumerator FadeCoroutine()
    {
        Debug.Log("���̵��ڷ�ƾ");
        float fadeCount = 1.0f;
        while (fadeCount >= 0.0f)
        {
            
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        //�����̸��̸� �����߻� ����ó��.
        if(!shot)
        {
            GameObject.Find("Panel_fadeIn").SetActive(false);
        }
        else
        {
            GameObject.Find("shot_fadeIn").SetActive(false);
        }
        
    }
}
