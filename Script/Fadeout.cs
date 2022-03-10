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
        //Debug.Log("�־ȵ�!!");
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
    //    Debug.Log("�־ȵ�!!");
    //}

    public IEnumerator FadeCoroutine()      
    {
        
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            //Debug.Log("�־ȵ�!!");
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
        //(3/4)�۾�
        else if (shot_fo)
        {
            GameObject.Find("Shot_Select_menu").SetActive(false);           //�����ù�ư ��Ȱ��ȭ
            GameObject.Find("Canvas").transform.Find("shot_fadeIn").gameObject.SetActive(true); //Activate�� off�� ������Ʈ�� �θ� ã��. �� �Ʒ� �ִ� ������Ʈ�� Activate�Ѵ�.
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
            Invoke("next_scene", 2.0f); //2�ʵ� ȭ����ȯ
        }

    }
    //(3/7)�����̵� ������
    private void next_scene()
    {
        SceneManager.LoadScene("Select_Cut");
    }
}  
