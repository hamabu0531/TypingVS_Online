using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class OneMatchingNetwork : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("OneMatching", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
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
        Disconnection();
        SceneManager.LoadScene("OneOnOne");
    }
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            CheckPlayerCount();
        }
    }
}
