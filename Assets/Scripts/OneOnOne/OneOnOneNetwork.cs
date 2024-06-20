using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class OneOnOneNetwork : MonoBehaviourPunCallbacks
{
    int roomNum = 0;
    public GameObject UIManager;
    OneOnOneUI oneUI;
    // Start is called before the first frame update
    void Start()
    {
        oneUI = UIManager.GetComponent<OneOnOneUI>();
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        JoinOrCreateRoom();
    }
    public void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        Debug.Log("Attempting to join or create room: " + "\"OneOnOne" + roomNum + "\"");
        PhotonNetwork.JoinOrCreateRoom("OneOnOne" + roomNum, roomOptions, TypedLobby.Default);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (returnCode == ErrorCode.GameFull) //êlêîÇ¢Ç¡ÇœÇ¢
        {
            roomNum++;
            JoinOrCreateRoom();
        }
        else
        {
            Debug.LogError("Failed to join room: " + message);
            base.OnJoinRoomFailed(returnCode, message);
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to \"OneOnOne" + roomNum + "\"");
        Player[] p = PhotonNetwork.PlayerList;
        base.OnJoinedRoom();
    }
    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }

    // Update is called once per frame
    void Update()
    {

    }
}