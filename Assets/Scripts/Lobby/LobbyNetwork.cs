using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    public GameObject UIManager;
    string currentState = PhotonNetwork.NetworkClientState.ToString();
    LobbyUI lobbyUI;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        lobbyUI = UIManager.GetComponent<LobbyUI>();
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("lobby", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        lobbyUI.ChangeActive();
    }

    public void Disconnection()
    {
        PhotonNetwork.Disconnect();
    }

    private void Update()
    {
        if (PhotonNetwork.NetworkClientState.ToString() != currentState)
        {
            currentState = PhotonNetwork.NetworkClientState.ToString();
            Debug.Log(PhotonNetwork.NetworkClientState.ToString());
        }
    }
}
