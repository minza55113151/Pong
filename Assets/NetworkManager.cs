using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInputField;
    [SerializeField] private TMP_InputField joinInputField;

    private void Start()
    {
        MenuManager.instance.OpenMenu("loading");
        PhotonNetwork.ConnectUsingSettings();
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
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenu("createandjoinroom");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenu("createandjoinroom");
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
