using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OneOnOneNetwork : MonoBehaviourPunCallbacks
{
    public GameObject UIManager;
    private bool flag = false;
    OneOnOneUI oneUI;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        oneUI = UIManager.GetComponent<OneOnOneUI>();
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("OneOnOne", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        flag = true;
        
    }
    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
    }
    void CheckPlayerCount()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("2 players have joined the room. Starting the game...");
            StartGame();
        }
    }
    void StartGame()
    {
        oneUI.ChangeActive();
    }
    private void Update()
    {
        if (flag)
        {
            CheckPlayerCount();
        }
    }
}
