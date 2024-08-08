using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoMatchingNetwork : MonoBehaviourPunCallbacks
{
    public GameObject uIManager;
    TwoMatchingUI twoMatchingUI;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        twoMatchingUI = uIManager.GetComponent<TwoMatchingUI>();
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
        CheckPlayerCount();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        CheckPlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        CheckPlayerCount();
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
        else
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator StartGame()
    {
        twoMatchingUI.matchingText.transform.GetChild(0).gameObject.SetActive(false); //ローディングのくるくる非表示
        twoMatchingUI.matchingText.transform.parent.GetChild(1).gameObject.SetActive(false); //キャンセルボタン非表示
        twoMatchingUI.matchingText.text = "対戦相手が見つかりました！";
        yield return new WaitForSeconds(2);
        Disconnection();
        yield return new WaitUntil(()=> !PhotonNetwork.IsConnected);
        SceneManager.LoadScene("TwoOnTwo");
    }
}
