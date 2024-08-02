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
        DisplayRoomList(roomList); // ���[�����X�g���X�V���ꂽ�Ƃ��̏���
        Debug.Log("�X�V");
    }

    private void DisplayRoomList(List<RoomInfo> roomList)
    {
        // �����̃{�^�����폜�i���X�g���X�V���邽�߁j
        foreach (RectTransform child in buttonParent)
        {
                Destroy(child.gameObject);
                Debug.Log("�j��");
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.IsVisible && room.IsOpen)
            {
                // �{�^�����v���n�u���琶��
                GameObject button = Instantiate(buttonPrefab, buttonParent);

                // �{�^���̃e�L�X�g��ݒ�
                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = room.Name;

                // �{�^���̃N���b�N�C�x���g��ݒ�
                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnRoomButtonClick(room.Name));
            }
        }
    }

    private void OnRoomButtonClick(string roomName)
    {
        Debug.Log("Selected Room: " + roomName);
        // �����őI���������[���ɎQ�����鏈����ǉ��ł��܂�
    }
    public void Update()
    {
        int ping = PhotonNetwork.GetPing();
        lobbyUI.pingText.text = ping + " ms";
    }

}
