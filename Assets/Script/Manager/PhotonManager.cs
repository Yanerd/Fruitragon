using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    [Header("[ConnectViewPage]")]
    [SerializeField] GameObject connectViewPage = null;
    [SerializeField] TextMeshProUGUI connectInfo = null;

    [Header("RoomListPage")]
    [SerializeField] GameObject searchRoomPage = null;

    [Header("TestNameField")]
    [SerializeField] TMP_InputField testNameInput = null;

    //button
    [SerializeField] Button searchRoomButton;


    Vector3 originCamearPos;

    bool onMenu = false;

    string testName;

    Coroutine makingCoroutine;

    //process check value
    int process;

    private void Awake()
    {
        //process value init
        process = 0;

        //Bring Camera Component
        originCamearPos = Camera.main.transform.position;

        //screen setting
        Screen.SetResolution(1920, 1080, false);
    }

    private void Start()
    {
        GameManager.INSTANCE.INVASIONALLOW = (GameManager.INSTANCE.INVASIONALLOW == false);
    }

    private void Update()//view real time connect
    {
        connectInfo.text = PhotonNetwork.NetworkClientState.ToString();
        
        //searchRoomPage에서 방 리스트 확인해서 네트워크 연결후 씬 전환(action Scene에서는 photon view 연결된 obj들 상호작용/ RPC를 통한 value 전달과 생성?)
    }

    public void OnInputChanged()//input feild name test
    {
        Debug.Log(testNameInput.text.Length);
        if (testNameInput.text.Length < 10 && testNameInput.text.Length > 2)
        {
            Debug.Log("Api 확인 (Id 확인됨)");
            Debug.Log("===============================================================");
            testName = testNameInput.text;
        }
        else
        {
            Debug.Log("(외부 API 연결이 확인되지 않습니다. (Id 길이가 틀립니다))");
            Debug.Log("InGame : 북풍이 불어오지 않습니다.");
            Debug.Log("===============================================================");
        }
    }

    #region Permit Invasion
    //1.invasion allow make room and ready(takes benefit)
    public void OnInvasionPermitButton()
    {
        //invasion allow toggle
        GameManager.INSTANCE.INVASIONALLOW = (GameManager.INSTANCE.INVASIONALLOW == false);

        if (GameManager.INSTANCE.INVASIONALLOW == true)
        {
            //make room and ready
            Debug.Log("invasion allow : " + GameManager.INSTANCE.INVASIONALLOW);
            makingCoroutine = StartCoroutine(RoomMakingProcess());
        }
        else if(GameManager.INSTANCE.INVASIONALLOW == false)
        {
            //network disconnect
            Debug.Log("invasion allow : " + GameManager.INSTANCE.INVASIONALLOW);
            PhotonNetwork.Disconnect();
        }
    }

    IEnumerator RoomMakingProcess()
    {
        while (true)
        {
            switch (process)
            {
                case 0:
                    PhotonNetwork.ConnectUsingSettings();
                    break;
                case 1:
                    PhotonNetwork.JoinLobby();
                    process = 2;
                    break;
                case 2:
                    PhotonNetwork.CreateRoom(testName, new RoomOptions { MaxPlayers = 2 }, null);
                    break;
                case 3:
                    StopCoroutine(makingCoroutine);
                    break;
            }
            yield return null;
        }
        //2. master server connect and name check
    }

    #region network call back override
    public override void OnConnectedToMaster()//process 0 
    {
        base.OnConnectedToMaster();

        PhotonNetwork.LocalPlayer.NickName = testName;

        Debug.Log("UserName = " + PhotonNetwork.NickName);
        Debug.Log("InGame : 북풍이 불어옵니다...!");
        Debug.Log("===============================================================");
        
        process = 1;
    }

    public override void OnDisconnected(DisconnectCause cause)//fail
    {
        base.OnDisconnected(cause);

        Debug.Log("Server Disconnected");
        Debug.Log("InGame : 북풍이 잦아듭니다...");
        Debug.Log("===============================================================");
        
        process = 0;
        StopCoroutine(makingCoroutine);
    }

    public override void OnJoinedLobby() //process 1
    {
        base.OnJoinedLobby();

        Debug.Log("Join Lobby");
        Debug.Log("InGame : 북풍이 거세게 불어옵니다..!");
        Debug.Log("===============================================================");
        
        process = 2;
    }

    public override void OnCreatedRoom()//process 2
    {
        base.OnCreatedRoom();

        Debug.Log("Room Created Success");
        Debug.Log("InGame : 손님을 맞이할 준비가 끝났습니다..!");
        Debug.Log("===============================================================");

        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);

        process = 3;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)//fail
    {
        base.OnCreateRoomFailed(returnCode, message);

        Debug.Log("Room Created Failed");
        Debug.Log("InGame : 대문이 대차게 닫힙니다...");
        Debug.Log("===============================================================");

        process = 0;
        StopCoroutine(makingCoroutine);
    }
    public override void OnJoinedRoom()// process 3
    {
        base.OnJoinedRoom();

        Debug.Log("Joined Room Success");
        Debug.Log("InGame : 손님을 기다립니다..!");
        Debug.Log("===============================================================");
        
        process = 4;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)//faild
    {
        base.OnJoinRoomFailed(returnCode, message);

        Debug.Log("Joining Room Failed");
        Debug.Log("InGame : 밥상을 걷어 찹니다...");
        Debug.Log("===============================================================");

        process = 0;
        StopCoroutine(makingCoroutine);
    }

    #endregion
    #endregion

    #region Do Invasion

    //if room is true 

    public void OnGoOffenseButton()
    {
        GameManager.INSTANCE.WANTINVASION = (GameManager.INSTANCE.WANTINVASION == false);

        if (GameManager.INSTANCE.WANTINVASION)
        {
            searchRoomButton.interactable = false;
            StartCoroutine(ClosUpCamera());
            StartCoroutine(Uptrans(searchRoomPage));
            StartCoroutine(JoinRoomProcess());
        }
        else
        {
            searchRoomButton.interactable = false;
            StartCoroutine(FadeOutCamera());
            StartCoroutine(Downtrans(searchRoomPage));
        }
    }

    IEnumerator JoinRoomProcess()
    {
        Debug.Log("입장중");
        Debug.Log(PhotonNetwork.CountOfRooms);
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);

        PhotonNetwork.JoinRandomRoom();
        yield return null;
    }



    #endregion

    #region Page Up&Down Coroutine
    IEnumerator Uptrans(GameObject page)
    {
        while (true)
        {
            page.transform.position =
                Vector3.Lerp(page.transform.position, page.transform.position + new Vector3(0, 100f, 0), Time.deltaTime * 20f);
            yield return null;

            if (page.transform.position.y >= 540f)
            {
                yield break;
            }
        }
    }
    IEnumerator Downtrans(GameObject page)
    {
        while (true)
        {
            page.transform.position =
                Vector3.Lerp(page.transform.position, page.transform.position + new Vector3(0, -100f, 0), Time.deltaTime * 30f);
            yield return null;

            if (page.transform.position.y <= -540f)
            {
                yield break;
            }
        }
    }
    #endregion

    #region Change CameraView Coroutine
    IEnumerator ClosUpCamera()
    {
        while (true)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(-2.5f, 2.9f, -4.1f), Time.deltaTime * 2f);

            if (Camera.main.orthographicSize >= 0.3f)
            {
                Camera.main.orthographicSize -= 0.04f;
            }

            if (Camera.main.transform.position.z <= -4.08f)
            {
                searchRoomButton.interactable = true;
                yield break;
            }

            yield return null;
        }
    }
    IEnumerator FadeOutCamera()
    {
        while (true)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(-2.72f, 4.25f, -2.72f), Time.deltaTime * 2f);

            if (Camera.main.orthographicSize <= 2.5f)
            {
                Camera.main.orthographicSize += 0.04f;
            }

            if (Camera.main.transform.position.z >= -2.74f)
            {
                searchRoomButton.interactable = true;
                yield break;
            }
            yield return null;
        }
    }

    #endregion
}