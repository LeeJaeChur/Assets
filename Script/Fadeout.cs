using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fadeout : MonoBehaviour
{
    public Image image;
   // public MainButton button = new MainButton();

    public bool fadeMain = false;
    public bool fadeForm = false;
    public bool payment = false;
    public bool shot_fo = false;
    public bool shot_select = false;
    public bool shot_end = false;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
     //   button = GameObject.Find("MainButton").GetComponent<MainButton>();
    }

    private void Start()
    {
        //Debug.Log("왜안되!!");
    }
    // Update is called once per frame
    void Update()
    {
       //fade = GameObject.Find("MainButton").GetComponent<MainButton>().mainfade;
       
       // Debug.Log(fade);
    }

    public void MainFade()
    {
        StartCoroutine(FadeCoroutine());     
    }

    //public void Call()
    //{
    //    Debug.Log("왜안되!!");
    //}

    public IEnumerator FadeCoroutine()      
    {
        
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            //Debug.Log("왜안되!!");
            //if (payment)
            //{
            //    fadeCount += 0.03f;
            //    yield return new WaitForSeconds(0.01f);
            //    image.color = new Color(0, 0, 0, fadeCount);
            //}
            //else
            //{
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                image.color = new Color(0, 0, 0, fadeCount);
           // }
            
        }

        if (fadeMain)
        {
            Debug.Log("main = " + fadeMain);
            //button.gameObject.SetActive(false);
            SceneManager.LoadScene("Select_form");
        }
        else if (fadeForm)
        {
            Debug.Log("form = " + fadeForm);
            SceneManager.LoadScene("Select_Payment");
        }
        else if (payment)
        {
            Debug.Log("payment = " + payment);

            //SceneManager.LoadScene("Camera");
            SceneManager.LoadScene("Screen Shot Test Scene");
        }
        //(3/4)작업
        else if (shot_fo)
        {
            GameObject.Find("Shot_Select_menu").SetActive(false);           //샷선택버튼 비활성화
            GameObject.Find("Canvas").transform.Find("shot_fadeIn").gameObject.SetActive(true); //Activate가 off된 오브젝트의 부모를 찾아. 그 아래 있는 오브젝트를 Activate한다.
            GameObject.Find("shot_fadeIn").GetComponent<Fadein>().shot = true;
            if (!shot_select)
            {
                GameObject.Find("Canvas").transform.Find("Panel_shot").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Countdown").gameObject.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<Countdown>().StartCount();
                
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("Panel_shot").gameObject.SetActive(true);
               //GameObject.Find("Canvas").transform.Find("Countdown").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Btn_Screen Shot Without UI").gameObject.SetActive(true);
            }
        }
        //(3/8)
        else if(shot_end)
        {
            Invoke("next_scene", 2.0f); //2초뒤 화면전환
        }

    }
    //(3/7)레벨이동 딜레이
    private void next_scene()
    {
        SceneManager.LoadScene("Select_Cut");
    }
}  
