using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class Countdown : MonoBehaviour
{
   /* [SerializeField]*/public float setTimer = 13.0f;
   /* [SerializeField]*/public Text coutdownText;
   /* [SerializeField]*/public Text startment;
   /* [SerializeField]*/public string A = "잠시 후 촬영을 시작합니다.";
    public int cut = 0;
    public int max_cut;

    bool go = false;
    public bool shot_Time = false;

    //(3/7)test_ScreenShot에서 이동
    public string folderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    private string RootPath
        {
            get
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                return Application.dataPath;
#elif UNITY_ANDROID
                return $"/storage/emulated/0/DCIM/{Application.productName}/";
                //return Application.persistentDataPath;
#endif
            }
        }
       

    //public Fadein fadein_end;
    // Start is called before the first frame update
    void Start()
    {       
        //coutdownText.text = setTimer.ToString();
        coutdownText.text = A;
        //max_cut = 4;   //임시 컷수
        // Debug.Log("Countdown_시작" + setTimer);
        //GameObject.Find("shot_fadeIn").GetComponent<Fadein>().shot_Timer = true;

        BinaryFormatter bfl = new BinaryFormatter();
        FileStream fsl = new FileStream($"{RootPath}/{folderName}/DataSave.dat", FileMode.Open);

        SerializableDataField fileload = new SerializableDataField();
        fileload = bfl.Deserialize(fsl) as SerializableDataField;
        fsl.Close();

        max_cut = fileload.max_cut;
    }

    // Update is called once per frame
    public void Update()
    {
        //Debug.Log("Countdown_shot_Time" + shot_Time);
        if (cut == max_cut)
        {
            Debug.Log("out_" + cut);
            Invoke("next_scene", 2.0f); //2초뒤 화면전환
        }
        else if (go)
        {
            //Debug.Log("Countdown_go " + go);
            if (setTimer > 0)
            {
                coutdownText.text = A;
              //  Debug.Log("Countdown_setTimer > 0");
                setTimer -= Time.deltaTime;
               // Debug.Log("Countdown_" + Mathf.Round(setTimer));
            }
            else if (setTimer <= 0)
            {
              //  Debug.Log("Countdown_setTimer <= 0");
                //Time.timeScale = 0.0f;        //이게 문제. 시간을 고정시켰음.
                setTimer = 13.0f;
                shot_Time = true;
                //go = false;
                cut++;
                Debug.Log("go_" + cut);
            }
            
        }
        
       
            if (setTimer <= 10.3f)
            {
                coutdownText.text = Mathf.Round(setTimer).ToString();
            }
            
        
    }

    public void StartCount()
    {
            go = true;        
    }

    private void next_scene()
    {
        SceneManager.LoadScene("Select_Cut");
    }

}
