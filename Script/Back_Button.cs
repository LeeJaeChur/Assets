using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Back_Button : MonoBehaviour
{
    public Button BackButton;
 
    private void Awake()
    {
        BackButton.onClick.AddListener(BackScene);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SM);
    }

    void BackScene()
    {
        //Debug.Log("뒤로가기실행");
        
        
        GameObject.Find("Panel").GetComponent<Fadeout>().bb_fadeout = true;
        GameObject.Find("Panel").GetComponent<Fadeout>().MainFade();        //페이드 실행
        GameObject.Find("Buttons").SetActive(false);                     //메인버튼비활성화
        
        //Debug.Log("Back_fadeout : " + isFade);
       
         
    }



}
