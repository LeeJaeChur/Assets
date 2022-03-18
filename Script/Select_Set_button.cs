using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Set_button : MonoBehaviour
{
    //getPhoto
    public Button select_1; // 임시 4장 추후에 폼선택방식에 따라 가변으로 바꿔야함
    public Button select_2;
    public Button select_3;
    public Button select_4;

    //setPhoto
    public Button setButton_1;
    public Button setButton_2;
    public Button setButton_3;
    public Button setButton_4;

    public int cutNum = 0;
    public int gameNum = 0;

    private Image selectCut;

    private void Awake()
    {
        select_1.onClick.AddListener(GetPhoto_1);
        select_2.onClick.AddListener(GetPhoto_2);
        select_3.onClick.AddListener(GetPhoto_3);
        select_4.onClick.AddListener(GetPhoto_4);

        setButton_1.onClick.AddListener(SetPhoto_1);
        setButton_2.onClick.AddListener(SetPhoto_2);
        setButton_3.onClick.AddListener(SetPhoto_3);
        setButton_4.onClick.AddListener(SetPhoto_4);

    }

    private void GetPhoto_1()
    {

        cutNum = GameObject.Find("cut_1").GetComponent<Select_cut_Image>().cut;
        gameNum = GameObject.Find("cut_1").GetComponent<Select_cut_Image>().gameNum;
        Debug.Log(cutNum);
    }
    private void GetPhoto_2()
    {

        cutNum = GameObject.Find("cut_2").GetComponent<Select_cut_Image>().cut;
        gameNum = GameObject.Find("cut_2").GetComponent<Select_cut_Image>().gameNum;
        Debug.Log(cutNum);
    }
    private void GetPhoto_3()
    {

        cutNum = GameObject.Find("cut_3").GetComponent<Select_cut_Image>().cut;
        gameNum = GameObject.Find("cut_3").GetComponent<Select_cut_Image>().gameNum;
        Debug.Log(cutNum);
    }
    private void GetPhoto_4()
    {

        cutNum = GameObject.Find("cut_4").GetComponent<Select_cut_Image>().cut;
        gameNum = GameObject.Find("cut_4").GetComponent<Select_cut_Image>().gameNum;
        Debug.Log(cutNum);
    }

    private void SetPhoto_1()
    {
        GameObject.Find("Button_1").GetComponent<Select_Set_Image>().getPhoto = true;
    }
    private void SetPhoto_2()
    {
        GameObject.Find("Button_2").GetComponent<Select_Set_Image>().getPhoto = true;
    }
    private void SetPhoto_3()
    {
        GameObject.Find("Button_3").GetComponent<Select_Set_Image>().getPhoto = true;
    }
    private void SetPhoto_4()
    {
        GameObject.Find("Button_4").GetComponent<Select_Set_Image>().getPhoto = true;
    }

}
