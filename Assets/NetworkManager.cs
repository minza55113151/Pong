using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static NetworkManager instance;

    [SerializeField] private TMP_InputField createInputField;
    [SerializeField] private TMP_InputField joinInputField;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        MenuManager.instance.OpenMenu("loading");
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }
            else
            {
                PhotonNetwork.Disconnect();
                Application.Quit();
            }
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenu("createandjoinroom");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Menu");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenu("createandjoinroom");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenu("createandjoinroom");
    }


    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LoadLevel("Game");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createInputField.text))
            return;
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInputField.text, roomOptions, TypedLobby.Default);
    }
    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(joinInputField.text))
            return;
        PhotonNetwork.JoinRoom(joinInputField.text);
    }
}
