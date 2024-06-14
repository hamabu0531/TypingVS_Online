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

    public void Attack(int damage, int heal)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(damage>0 || playerHP[1] < 100)
            {
                playerHP[1] -= damage; // Client��HP�����炷
            }
            if (playerHP[0] < 100)
            {
                playerHP[0] += heal;
            }
            photonView.RPC("UpdateHP", RpcTarget.All, playerHP[0], playerHP[1]);
        }
        else
        {
            SendDamage(damage, heal);
        }
    }

    public void SendDamage(int damage, int heal)
    {
        photonView.RPC("ReceiveDamage", RpcTarget.MasterClient, damage, heal);
    }

    [PunRPC]
    void ReceiveDamage(int damage, int heal)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (damage > 0 || playerHP[0] < 100)
            {
                playerHP[0] -= damage; // Master��HP�����炷
            }
            if (playerHP[1] < 100)
            {
                playerHP[1] += heal;
            }
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
            return 1;
        }else
        {
            return 2;
        }
    }
}
