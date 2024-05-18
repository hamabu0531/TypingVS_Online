using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OneOnOneVariables : MonoBehaviourPunCallbacks, IPunObservable
{
    public int[] playerHP = { 100, 100 }; // Master��HP, Client��HP
    public float countDownTimer = 5.0f;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            countDownTimer -= Time.deltaTime;
        }
    }

    // �ϐ��̓���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �f�[�^�̑��M���i�}�X�^�[�N���C�A���g�j
            stream.SendNext(playerHP[0]); // Master��HP
            stream.SendNext(playerHP[1]); // Client��HP
            stream.SendNext(countDownTimer);
        }
        else
        {
            // �f�[�^�̎�M��
            playerHP[0] = (int)stream.ReceiveNext(); // Master��HP
            playerHP[1] = (int)stream.ReceiveNext(); // Client��HP
            countDownTimer = (float)stream.ReceiveNext();
        }
    }

    public void Attack()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerHP[1] -= 10; // Client��HP�����炷
            photonView.RPC("UpdateHP", RpcTarget.All, playerHP[0], playerHP[1]);
        }
        else
        {
            SendDamage();
        }
    }

    public void SendDamage()
    {
        photonView.RPC("ReceiveDamage", RpcTarget.MasterClient);
    }

    [PunRPC]
    void ReceiveDamage()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerHP[0] -= 10; // Master��HP�����炷
            photonView.RPC("UpdateHP", RpcTarget.All, playerHP[0], playerHP[1]);
        }
    }

    [PunRPC]
    void UpdateHP(int masterHP, int clientHP)
    {
        playerHP[0] = masterHP;
        playerHP[1] = clientHP;
    }
    public int playerNum()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return 2;
        }else
        {
            return 1;
        }
    }
}
