using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    public GameObject UIManager, buttonPrefab;
    public Transform buttonParent;
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
        Debug.Log("更新");
    }

    private void DisplayRoomList(List<RoomInfo> roomList)
    {
        // 既存のボタンを削除（リストを更新するため）
        foreach (RectTransform child in buttonParent)
        {
                Destroy(child.gameObject);
                Debug.Log("破壊");
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.IsVisible && room.IsOpen)
            {
                // ボタンをプレハブから生成
                GameObject button = Instantiate(buttonPrefab, buttonParent);

                // ボタンのテキストを設定
                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = room.Name;

                // ボタンのクリックイベントを設定
                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnRoomButtonClick(room.Name));
            }
        }
    }

    private void OnRoomButtonClick(string roomName)
    {
        Debug.Log("Selected Room: " + roomName);
        // ここで選択したルームに参加する処理を追加できます
    }
    public void Update()
    {
        int ping = PhotonNetwork.GetPing();
        lobbyUI.pingText.text = ping + " ms";
    }

}
