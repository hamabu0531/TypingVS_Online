using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OneOnOneVariables : MonoBehaviourPunCallbacks, IPunObservable
{
    public int[] playerHP = { 100, 100 }; // MasterのHP, ClientのHP
    public float gameTimer = 10.0f;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
            {
                gameTimer = 0;
                Debug.Log("Times up!");
            }
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
            stream.SendNext(gameTimer);
        }
        else
        {
            // データの受信側
            playerHP[0] = (int)stream.ReceiveNext(); // MasterのHP
            playerHP[1] = (int)stream.ReceiveNext(); // ClientのHP
            gameTimer = (float)stream.ReceiveNext();
        }
    }

    public void Attack()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerHP[1] -= 10; // ClientのHPを減らす
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
            playerHP[0] -= 10; // MasterのHPを減らす
            photonView.RPC("UpdateHP", RpcTarget.All, playerHP[0], playerHP[1]);
        }
    }

    [PunRPC]
    void UpdateHP(int masterHP, int clientHP)
    {
        playerHP[0] = masterHP;
        playerHP[1] = clientHP;
    }
}
