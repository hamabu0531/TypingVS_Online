using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoMatchingNetwork : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("TwoMatching", roomOptions, TypedLobby.Default);
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            Debug.Log("4 players have joined the room. Starting the game...");
            StartCoroutine(StartGame());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            CheckPlayerCount();
        }
    }
    IEnumerator StartGame()
    {
        //oneMatchingUI.matchingText.transform.GetChild(0).gameObject.SetActive(false); //ローディングのくるくる非表示
        //oneMatchingUI.matchingText.transform.parent.GetChild(1).gameObject.SetActive(false); //キャンセルボタン非表示
        //oneMatchingUI.matchingText.text = "対戦相手が見つかりました！";
        yield return new WaitForSeconds(2);
        Disconnection();
        SceneManager.LoadScene("TwoOnTwo");
    }
}
