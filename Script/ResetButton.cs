using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public Button resetButton;  //테스트용 리셋버튼 ( 플레이가 전부 끝나고 초기화면으로 가는것을 임시로 구현) 
    private void Awake()
    {
        resetButton.onClick.AddListener(ResetScene);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ResetScene()
    {

        //Debug.Log("Reset : " + gameNum);
        SceneManager.LoadScene("Main");

    }
}
