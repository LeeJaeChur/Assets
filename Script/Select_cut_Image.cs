using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class Select_cut_Image : MonoBehaviour
{



    public Image imageToShow;        // ��� �̹��� ������Ʈ

    //����
    public int gameNum;  //���� �÷��̺� ���� ����
                         //��¥�� �ٲ�°� Ȯ�� �׽�Ʈ��
                         // string Today_now = DateTime.Now.ToString("yyMMddHHmmss");
                         //static int Today = int.Parse(DateTime.Now.ToString("yyMMdd"));

    string Today_now = DateTime.Now.ToString("yyMMdd");
    public int Today = 0;
    //(3/7)�ִ� �Կ� Ƚ�� �߰���
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
    
    private Texture2D _imageTexture; // imageToShow�� �ҽ� �ؽ���

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

    private void Awake()
    {
        Today = int.Parse(DateTime.Now.ToString("yyMMdd"));

        BinaryFormatter bfl = new BinaryFormatter();
        FileStream fsl = new FileStream($"{RootPath}/{folderName}/DataSave.dat", FileMode.Open);

        SerializableDataField fileload = new SerializableDataField();
        fileload = bfl.Deserialize(fsl) as SerializableDataField;
        fsl.Close();

        if (fileload.today == Today)    //��谡 �����ٰ� �����µ� ���� �� ���
        {
            Debug.Log("������");
            Debug.Log("����� ��¥ : " + fileload.today + "���� ��¥ : " + Today);
            Debug.Log("���� ���� : " + fileload.gamenum);
            gameNum = fileload.gamenum;
            Debug.Log("Load : " + gameNum);
        }
        else                            //�������̶��
        {
            Debug.Log("�ٸ���");
            Debug.Log("���� �ð�" + Today);
            Debug.Log("����� �ð�" + fileload.today);

        }
    }

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
            //gameNum = GameObject.Find("cut").GetComponent<Select_Set_button>().gameNum;
            Debug.Log("cut : "+cut);
            Debug.Log("gameNum : " + gameNum);
            ReadScreenShotFileAndShow(imageToShow);
            getPhoto = false;
        }
    }

    private void ReadScreenShotFileAndShow(Image destination)
    {
        Debug.Log("����?");
        string folderPath = FolderPath;
        string totalPath = lastSavedPath;

        if (Directory.Exists(folderPath) == false)
        {
            Debug.LogWarning($"{folderPath} ������ �������� �ʽ��ϴ�.");
            return;
        }
        if (File.Exists(totalPath) == false)
        {
            Debug.LogWarning($"{totalPath} ������ �������� �ʽ��ϴ�.");
            return;
        }

        // ������ �ؽ��� �ҽ� ����
        if (_imageTexture != null)
            Destroy(_imageTexture);
        if (destination.sprite != null)
        {
            Destroy(destination.sprite);
            destination.sprite = null;
        }

        // ����� ��ũ���� ���� ��ηκ��� �о����
        try
        {
            byte[] texBuffer = File.ReadAllBytes(totalPath);

            _imageTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            _imageTexture.LoadImage(texBuffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"��ũ���� ������ �д� �� �����Ͽ����ϴ�.");
            Debug.LogWarning(e);
            return;
        }

        // �̹��� ��������Ʈ�� ����
        Rect rect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
        Sprite sprite = Sprite.Create(_imageTexture, rect, Vector2.one * 0.5f);
        destination.sprite = sprite;

       
    }
}
