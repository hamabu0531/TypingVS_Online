using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class OneMatchingNetwork : MonoBehaviourPunCallbacks
{
    private OneMatchingUI oneMatchingUI;
    public GameObject uIManager;
    // Start is called before the first frame update
    void Start()
    {
        oneMatchingUI = uIManager.GetComponent<OneMatchingUI>();
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("roomName"), roomOptions, TypedLobby.Default);
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
            StartCoroutine(StartGame());
        }
    }
    
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            CheckPlayerCount();
        }
    }

    IEnumerator StartGame()
    {
        oneMatchingUI.matchingText.transform.GetChild(0).gameObject.SetActive(false); //ローディングのくるくる非表示
        oneMatchingUI.matchingText.transform.parent.GetChild(1).gameObject.SetActive(false); //キャンセルボタン非表示
        oneMatchingUI.matchingText.text = "対戦相手が見つかりました！";
        yield return new WaitForSeconds(2);
        Disconnection();
        SceneManager.LoadScene("OneOnOne");
    }
}
