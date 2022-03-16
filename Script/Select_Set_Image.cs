using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Select_Set_Image : MonoBehaviour
{



    public Image imageToShow;        // 띄울 이미지 컴포넌트

    //제작
    public int gameNum;  //게임 플레이별 폴더 생성
                         //날짜가 바뀌는거 확인 테스트용
                         // string Today_now = DateTime.Now.ToString("yyMMddHHmmss");
                         //static int Today = int.Parse(DateTime.Now.ToString("yyMMdd"));

    string Today_now = DateTime.Now.ToString("yyMMdd");
    public int Today = 0;
    //(3/7)최대 촬영 횟수 추가분
    //private int cut = 0;
    public int cut = 0;
    private int max_cut;
    //(3/8)
    public bool isfadeout = false;
    //(3/16)selectcut
    private int selectcut;
    public bool getPhoto = false;





    public ScreenShotFlash flash;


    public string folderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    public string extName = "png";

    private bool _willTakeScreenShot = false;
    private bool shot = false;

    /***********************************************************************
    *                               Fields & Properties
    ***********************************************************************/

    private Texture2D _imageTexture; // imageToShow의 소스 텍스쳐

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
    private string FolderPath => $"{RootPath}/{folderName}/{Today_now}/{gameNum}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";

    private string save_cut => $"{FolderPath}/{cut.ToString()}.{extName}";
    private string lastSavedPath;



    // Start is called before the first frame update
    void Start()
    {
        lastSavedPath = save_cut;
        ReadScreenShotFileAndShow(imageToShow);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (getPhoto)
        {
            cut = GameObject.Find("cut").GetComponent<Select_Set_button>().cutNum;
            gameNum = GameObject.Find("cut").GetComponent<Select_Set_button>().gameNum;
            lastSavedPath = save_cut;
            Debug.Log("cut : " + cut);
            Debug.Log("gameNum : " + gameNum);
            Debug.Log(FolderPath);
            Debug.Log(lastSavedPath);
            getShot();
            getPhoto = false;
        }
    }

    void getShot()
    {
        ReadScreenShotFileAndShow(imageToShow);
    }

    private void ReadScreenShotFileAndShow(Image destination)
    {
        Debug.Log("오냐?");
        string folderPath = FolderPath;
        string totalPath = lastSavedPath;

        if (Directory.Exists(folderPath) == false)
        {
            Debug.LogWarning($"{folderPath} 폴더가 존재하지 않습니다.");
            return;
        }
        if (File.Exists(totalPath) == false)
        {
            Debug.LogWarning($"{totalPath} 파일이 존재하지 않습니다.");
            return;
        }

        // 기존의 텍스쳐 소스 제거
        if (_imageTexture != null)
            Destroy(_imageTexture);
        if (destination.sprite != null)
        {
            Destroy(destination.sprite);
            destination.sprite = null;
        }

        // 저장된 스크린샷 파일 경로로부터 읽어오기
        try
        {
            byte[] texBuffer = File.ReadAllBytes(totalPath);

            _imageTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            _imageTexture.LoadImage(texBuffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"스크린샷 파일을 읽는 데 실패하였습니다.");
            Debug.LogWarning(e);
            return;
        }

        // 이미지 스프라이트에 적용
        Rect rect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
        Sprite sprite = Sprite.Create(_imageTexture, rect, Vector2.one * 0.5f);
        destination.sprite = sprite;


    }
}
