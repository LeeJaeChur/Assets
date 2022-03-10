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
            GameObject.FindWithTag("Button").SetActive(false);              //��������ư ��Ȱ��ȭ            
        }else if(fade_shot)
        {
            GameObject.Find("Select_button_canvas").SetActive(false);           //�����ù�ư ��Ȱ��ȭ   
           
        }
       // GameObject.FindWithTag("Button").SetActive(false);
    }

    public void OnClickMain()
    {
        //GameObject.Find("Panel").GetComponent<Fadeout>().Call(); 
        //fadeout.Call();
       // mainfade = true;
        GameObject.Find("Panel").GetComponent<Fadeout>().fadeMain = true;  //���̵�ƿ��� ���� ȭ���̵���
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //���̵� ����
        GameObject.Find("MainButton").SetActive(false);                     //���ι�ư��Ȱ��ȭ

        // fadeout.StartCoroutine();//  SceneManager.LoadScene("Select_form");

        //SceneManager.LoadScene("Select_form");
    }

    public void OnClickform()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().fadeForm = true;  //���̵�ƿ��� ���� ȭ���̵���
        fade = GameObject.Find("Panel").GetComponent<Fadeout>().fadeForm;  
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //���̵� ����
       // GameObject.FindWithTag("Button").SetActive(false);

        //SceneManager.LoadScene("Select_Payment");
    }

    public void OnClickPay()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().payment = true;  //���̵�ƿ��� ���� ȭ���̵���
        fade = GameObject.Find("Panel").GetComponent<Fadeout>().payment;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //���̵� ����
    }
    //(3/7)�߰�
    public void OnClickshot_10sec()
    {       
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo = true;  //���̵�ƿ��� ���� ȭ���̵���
        //(3/7)
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_select = false;//10�� ��ư

        fade_shot = GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();
    }
    //(3/7)�߰�
    public void OnClickshot_button()
    {
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_fo = true;  //���̵�ƿ��� ���� ȭ���̵���(
        //(3/7)
        GameObject.Find("Panel").GetComponent<Fadeout>().shot_select = true;//10��/����ġ ���� ��ư Ȱ��ȭ

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
