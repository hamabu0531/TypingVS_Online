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
        //Debug.Log(PlayerPrefs.GetString("name"));
        PhotonNetwork.ConnectUsingSettings();
        lobbyUI = UIManager.GetComponent<LobbyUI>();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("lobby", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        lobbyUI.ChangeActive();
        ListPlayerName();
    }

    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
    }
    public void ListPlayerName()
    {
        if (PhotonNetwork.InRoom)
        {
            Player[] p = PhotonNetwork.PlayerList;
            string pList = "";
            for (int i = 0; i < p.Length; i++)
            {
                pList += p[i].NickName;
                if (i != p.Length - 1)
                {
                    pList += ", ";
                }
            }
            lobbyUI.playerList.text = pList;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        ListPlayerName();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        ListPlayerName();
    }
}
