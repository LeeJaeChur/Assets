using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
//저장/불러오기 
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


#if UNITY_ANDROID
using UnityEngine.Android;
#endif

// 날짜 : 2021-09-07 PM 3:03:10
// 작성자 : Rito

namespace Rito.Tests
{
    public class Test_ScreenShot : MonoBehaviour
    {
        /***********************************************************************
        *                               Public Fields
        ***********************************************************************/
        #region .
        public Button screenShotButton;          // 전체 화면 캡쳐
        public Button screenShotWithoutUIButton; // UI 제외 화면 캡쳐
        public Button readAndShowButton; // 저장된 경로에서 스크린샷 파일 읽어와서 이미지에 띄우기
        public Image imageToShow;        // 띄울 이미지 컴포넌트

        //제작
        public Button resetButton;  //테스트용 리셋버튼 ( 플레이가 전부 끝나고 초기화면으로 가는것을 임시로 구현) 
        public int gameNum;  //게임 플레이별 폴더 생성
        //날짜가 바뀌는거 확인 테스트용
        // string Today_now = DateTime.Now.ToString("yyMMddHHmmss");
         //static int Today = int.Parse(DateTime.Now.ToString("yyMMdd"));

        string Today_now = DateTime.Now.ToString("yyMMdd");
        public int Today = 0; 
        //(3/7)최대 촬영 횟수 추가분
        private int cut = 0;
        private int max_cut;
        //(3/8)
        public bool isfadeout = false;
        
        




        public ScreenShotFlash flash;


        public string folderName = "ScreenShots";
        public string fileName = "MyScreenShot";
        public string extName = "png";

        private bool _willTakeScreenShot = false;
        private bool shot = false;
        #endregion
        /***********************************************************************
        *                               Fields & Properties
        ***********************************************************************/
        #region .
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

        /*
         (3/8)파일 저장명 통일을 위한 작업
        */
        private string save_cut=> $"{FolderPath}/{cut.ToString()}.{extName}";
        //private string FolderPath => $"{RootPath}/{folderName}";
        //private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";

        private string lastSavedPath;

        #endregion

        /***********************************************************************
        *                               Unity Events
        ***********************************************************************/
        #region .
        private void Awake()
        {
            Today = int.Parse(DateTime.Now.ToString("yyMMdd"));
            //Today = int.Parse(DateTime.Now.ToString("ddHHmmss")); //테스트용
            //필요없어져서 숨김
            //screenShotButton.onClick.AddListener(TakeScreenShotFull);
            screenShotWithoutUIButton.onClick.AddListener(TakeScreenShotWithoutUI);
            //readAndShowButton.onClick.AddListener(ReadScreenShotAndShow);

            //(3/3)테스트용 리셋버튼
            //resetButton.onClick.AddListener(ResetScene);

           
            //(3/3)시작시 파일없으면 생성. 첫 실행시
            if (File.Exists(@$"{RootPath}/{folderName}/DataSave.dat") == false)
            {
                Debug.Log("파일없음 신규저장");
                BinaryFormatter bfs = new BinaryFormatter();
                FileStream fs = new FileStream($"{RootPath}/{folderName}/DataSave.dat", FileMode.Create);

                SerializableDataField filesave = new SerializableDataField();
                filesave.today = Today;
                filesave.gamenum = gameNum;
                Debug.Log("Save : " + filesave.gamenum);
                bfs.Serialize(fs, filesave);
                fs.Close();
            }
            //(3/3)저장파일 불러와서 비교
            
                BinaryFormatter bfl = new BinaryFormatter();
                FileStream fsl = new FileStream($"{RootPath}/{folderName}/DataSave.dat", FileMode.Open);

                SerializableDataField fileload = new SerializableDataField();
                fileload = bfl.Deserialize(fsl) as SerializableDataField;
                fsl.Close();

                if (fileload.today == Today)    //기계가 꺼졌다가 켜졌는데 같은 날 라면
                {
                    Debug.Log("같은날");
                Debug.Log("저장된 날짜 : " + fileload.today + "오늘 날짜 : " + Today);
                    Debug.Log("이전 파일 : " + fileload.gamenum);
                    gameNum = fileload.gamenum;
                    Debug.Log("Load : " + gameNum);
                }
                else                            //다음날이라면
                {
                    Debug.Log("다른날");
                    Debug.Log("현재 시간" + Today);
                    Debug.Log("저장된 시간" + fileload.today);

                }
            max_cut = fileload.max_cut;
            
        }

        private void Start()
        {
            //(3/3)카메라 켜지면 폴더생성
            //(3/10)촬영중 프로그램이 꺼지면 폴더 숫자를 건너 해결을 위해 Awake->Strat로 이동  
            // 폴더가 존재하지 않으면 새로 생성
            if (Directory.Exists(FolderPath) == false)
            {
                Debug.Log("폴더생성");
                Directory.CreateDirectory(FolderPath);              
            }
            gameNum++;
            //Debug.Log("start : " + gameNum);
            //Debug.Log("현재 시간" + Today_now);
            //Debug.Log("저장 시간" + Today);
           
            //(3/3)날짜 관련 파일저장. 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream($"{RootPath}/{folderName}/DataSave.dat", FileMode.Create);

            SerializableDataField filesave = new SerializableDataField();
            filesave.today = Today;
                  
            //(3/10)original
            filesave.gamenum = gameNum;

            Debug.Log("Save : " + filesave.gamenum);
            bf.Serialize(fs, filesave);
            fs.Close();
        }
        #endregion
        //(3/7)


        //(3/7)delay
        private void Update()
        {
            shot = GetComponent<Countdown>().shot_Time;
            //Debug.Log("test_screen : " + shot);
            if (shot)
            {
                Debug.Log("촬영");
                shot = false;
                GetComponent<Countdown>().shot_Time = shot;
                Debug.Log(GetComponent<Countdown>().shot_Time);
                TakeScreenShotWithoutUI();
            }

            //(3/7)
            if (cut == max_cut)
            {
                if(!isfadeout)
                {                    
                    GameObject.Find("Panel_shot").GetComponent<Fadeout>().MainFade();
                    isfadeout = true;
                    GameObject.Find("Panel_shot").GetComponent<Fadeout>().shot_end = isfadeout;
                }
               
            }
        }
       
        private void fadeout()
        {
            GameObject.Find("Panel_shot").GetComponent<Fadeout>().MainFade();
        }

        /***********************************************************************
        *                               Button Event Handlers
        ***********************************************************************/
        #region .
        ///<summary> 초기화면으로 이동 </summary>
        private void ResetScene()
        {
            
            Debug.Log("Reset : " + gameNum);
            SceneManager.LoadScene("Main");
           
        }

        /// <summary> UI 포함 전체 화면 캡쳐 </summary>
        private void TakeScreenShotFull()
        {
#if UNITY_ANDROID
            CheckAndroidPermissionAndDo(Permission.ExternalStorageWrite, () => StartCoroutine(TakeScreenShotRoutine()));
#else
            StartCoroutine(TakeScreenShotRoutine());
#endif
        }

        /// <summary> UI 미포함, 현재 카메라가 렌더링하는 화면만 캡쳐 </summary>
        private void TakeScreenShotWithoutUI()
        {
#if UNITY_ANDROID
            CheckAndroidPermissionAndDo(Permission.ExternalStorageWrite, () => _willTakeScreenShot = true);
#else
            _willTakeScreenShot = true;
            cut++;
#endif
        }

        private void ReadScreenShotAndShow()
        {
#if UNITY_ANDROID
            CheckAndroidPermissionAndDo(Permission.ExternalStorageRead, () => ReadScreenShotFileAndShow(imageToShow));
#else
            ReadScreenShotFileAndShow(imageToShow);
#endif
        }
        #endregion
        /***********************************************************************
        *                               Methods
        ***********************************************************************/
        #region .

        // UI 포함하여 현재 화면에 보이는 모든 것 캡쳐
        private IEnumerator TakeScreenShotRoutine()
        {
            yield return new WaitForEndOfFrame();
            CaptureScreenAndSave();
        }

        // UI 제외하고 현재 카메라가 렌더링하는 모습 캡쳐
        private void OnPostRender()
        {
            
                if (_willTakeScreenShot)
                {
                    _willTakeScreenShot = false;
                    CaptureScreenAndSave();
                }
                
        }

#if UNITY_ANDROID
        /// <summary> 안드로이드 - 권한 확인하고, 승인시 동작 수행하기 </summary>
        private void CheckAndroidPermissionAndDo(string permission, Action actionIfPermissionGranted)
        {
            // 안드로이드 : 저장소 권한 확인하고 요청하기
            if (Permission.HasUserAuthorizedPermission(permission) == false)
            {
                PermissionCallbacks pCallbacks = new PermissionCallbacks();
                pCallbacks.PermissionGranted += str => Debug.Log($"{str} 승인");
                pCallbacks.PermissionGranted += str => AndroidToast.I.ShowToastMessage($"{str} 권한을 승인하셨습니다.");
                pCallbacks.PermissionGranted += _ => actionIfPermissionGranted(); // 승인 시 기능 실행

                pCallbacks.PermissionDenied += str => Debug.Log($"{str} 거절");
                pCallbacks.PermissionDenied += str => AndroidToast.I.ShowToastMessage($"{str} 권한을 거절하셨습니다.");

                pCallbacks.PermissionDeniedAndDontAskAgain += str => Debug.Log($"{str} 거절 및 다시는 보기 싫음");
                pCallbacks.PermissionDeniedAndDontAskAgain += str => AndroidToast.I.ShowToastMessage($"{str} 권한을 격하게 거절하셨습니다.");

                Permission.RequestUserPermission(permission, pCallbacks);
            }
            else
            {
                actionIfPermissionGranted(); // 바로 기능 실행
            }
        }
#endif

        /// <summary> 스크린샷을 찍고 경로에 저장하기 </summary>
        private void CaptureScreenAndSave()
        {
            //최로 날짜(시분초)로 저장
            //string totalPath = TotalPath; // 프로퍼티 참조 시 시간에 따라 이름이 결정되므로 캐싱
            
           // Debug.Log(save_cut);

            Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            Rect area = new Rect(0f, 0f, Screen.width, Screen.height);

            // 현재 스크린으로부터 지정 영역의 픽셀들을 텍스쳐에 저장
            screenTex.ReadPixels(area, 0, 0);

            bool succeeded = true;
            try
            {
                // 폴더가 존재하지 않으면 새로 생성
                if (Directory.Exists(FolderPath) == false)
                {
                    Directory.CreateDirectory(FolderPath);
                }

                // 스크린샷 저장
                //File.WriteAllBytes(totalPath, screenTex.EncodeToPNG());
                File.WriteAllBytes(save_cut, screenTex.EncodeToPNG());

            }
            catch (Exception e)
            {
                succeeded = false;
               // Debug.LogWarning($"Screen Shot Save Failed : {totalPath}");
                Debug.LogWarning($"Screen Shot Save Failed : {save_cut}");
                Debug.LogWarning(e);
            }

            // 마무리 작업
            Destroy(screenTex);

            if (succeeded)
            {
                // Debug.Log($"Screen Shot Saved : {totalPath}");
                Debug.Log($"Screen Shot Saved : {save_cut}");
                flash.Show(); // 화면 번쩍
                //lastSavedPath = totalPath; // 최근 경로에 저장
                lastSavedPath = save_cut;
            }
            
            // 갤러리 갱신
            //RefreshAndroidGallery(totalPath);
            RefreshAndroidGallery(save_cut);
        }

        [System.Diagnostics.Conditional("UNITY_ANDROID")]
        private void RefreshAndroidGallery(string imageFilePath)
        {
#if !UNITY_EDITOR
            AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]
            { "android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + imageFilePath) });
            objActivity.Call("sendBroadcast", objIntent);
#endif
        }

        // 가장 최근에 저장된 이미지 보여주기
        /// <summary> 경로로부터 저장된 스크린샷 파일을 읽어서 이미지에 보여주기 </summary>
        private void ReadScreenShotFileAndShow(Image destination)
        {
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
        #endregion
    }
}