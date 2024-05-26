using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OneOnOneNetwork : MonoBehaviourPunCallbacks
{
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("OneOnOne", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (returnCode == ErrorCode.GameFull) //êlêîÇ¢Ç¡ÇœÇ¢
        {
            i++;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom("OneOnOne" + i.ToString(), roomOptions, TypedLobby.Default);
        }
        base.OnJoinRoomFailed(returnCode, message);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
    }

    // Update is called once per frame
    void Update()
    {

    }
}