using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainButton : MonoBehaviour
{
   
    public bool fade = false;
    public bool fade_shot = false;
    private void Awake()
    {
      // mainfade = GameObject.Find("Panel").GetComponent<Fadeout>().fade ;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            GameObject.FindWithTag("Button").SetActive(false);              //폼설정버튼 비활성화            
        }else if(fade_shot)
        {
            GameObject.Find("Select_button_canvas").SetActive(false);           //샷선택버튼 비활성화   
           
        }
       // GameObject.FindWithTag("Button").SetActive(false);
    }

    public void OnClickMain()
    {
        //GameObject.Find("Panel").GetComponent<Fadeout>().Call(); 
        //fadeout.Call();
       // mainfade = true;
        GameObject.Find("Panel").GetComponent<Fadeout>().fadeMain = true;  //페이드아웃의 다음 화면이동용
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //페이드 실행
        GameObject.Find("MainButton").SetActive(false);                     //메인버튼비활성화

        // fadeout.StartCoroutine();//  SceneManager.LoadScene("Select_form");

        //SceneManager.LoadScene("Select_form");
    }

    public void OnClickform()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().fadeForm = true;  //페이드아웃의 다음 화면이동용
        fade = GameObject.Find("Panel").GetComponent<Fadeout>().fadeForm;  
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //페이드 실행
       // GameObject.FindWithTag("Button").SetActive(false);

        //SceneManager.LoadScene("Select_Payment");
    }

    public void OnClickPay()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().payment = true;  //페이드아웃의 다음 화면이동용
        fade = GameObject.Find("Panel").GetComponent<Fadeout>().payment;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //페이드 실행
    }
    //(3/7)추가
    public void OnClickshot_10sec()
    {       
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo = true;  //페이드아웃의 다음 화면이동용
        //(3/7)
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_select = false;//10초 버튼

        fade_shot = GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();
    }
    //(3/7)추가
    public void OnClickshot_button()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo = true;  //페이드아웃의 다음 화면이동용(
        //(3/7)
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_select = true;//10초/스위치 선택 버튼 활성화

        fade_shot = GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();
    }
    //IEnumerator FadeCoroutine()
    //{
    //    float fadeCount = 0.0f;
    //    while (fadeCount < 1.0f)
    //    {
    //        fadeCount += 0.01f;
    //        yield return new WaitForSeconds(0.01f);
    //        image.color = new Color(0, 0, 0, fadeCount);
    //    }
    //    SceneManager.LoadScene("Select_form");
    //}
}
