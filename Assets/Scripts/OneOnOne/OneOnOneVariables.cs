using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OneOnOneVariables : MonoBehaviourPunCallbacks, IPunObservable
{
    public int[] playerHP = { 100, 100 }; // MasterのHP, ClientのHP
    public float countDownTimer = 5.0f;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            countDownTimer -= Time.deltaTime;
        }
    }

    // 変数の同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // データの送信側（マスタークライアント）
            stream.SendNext(playerHP[0]); // MasterのHP
            stream.SendNext(playerHP[1]); // ClientのHP
            stream.SendNext(countDownTimer);
        }
        else
        {
            // データの受信側
            playerHP[0] = (int)stream.ReceiveNext(); // MasterのHP
            playerHP[1] = (int)stream.ReceiveNext(); // ClientのHP
            countDownTimer = (float)stream.ReceiveNext();
        }
    }

    public void Attack(int damage, int heal)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(damage>0 || playerHP[1] < 100)
            {
                playerHP[1] -= damage; // ClientのHPを減らす
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
                playerHP[0] -= damage; // MasterのHPを減らす
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
