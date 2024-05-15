using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JoinLobby : MonoBehaviourPunCallbacks
{
    string currentState = PhotonNetwork.NetworkClientState.ToString();
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
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
