using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    public GameObject UIManager;
    LobbyUI lobbyUI;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("name");
        //Debug.Log(PhotonNetwork.NickName);
        PhotonNetwork.ConnectUsingSettings();
        lobbyUI = UIManager.GetComponent<LobbyUI>();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        lobbyUI.ChangeActive();
    }

    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        DisplayRoomList(roomList); // ルームリストが更新されたときの処理
    }

    private void DisplayRoomList(List<RoomInfo> roomList)
    {
        string roomListString = "";
        foreach (RoomInfo room in roomList)
        {
            if (room.IsVisible && room.IsOpen)
            {
                if (roomListString != "")
                {
                    roomListString += ", "; // カンマで区切る
                }
                roomListString += $"Room Name: {room.Name} (Players: {room.PlayerCount}/{room.MaxPlayers})";
            }
        }

        Debug.Log("Room List: " + roomListString); // ルームリストをデバッグログに表示
    }

    public void Update()
    {
        int ping = PhotonNetwork.GetPing();
        lobbyUI.pingText.text = ping + " ms";
    }

}
