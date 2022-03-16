using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public Button resetButton;  //�׽�Ʈ�� ���¹�ư ( �÷��̰� ���� ������ �ʱ�ȭ������ ���°��� �ӽ÷� ����) 
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
