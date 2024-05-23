using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OneOnOneUI : MonoBehaviour
{
    public Canvas canvas, hiddenCanvas;
    public GameObject Variables;
    public Slider mySlider, enemySlider;
    public Text winnerText;
    private float timer;
    private Text countText, playerNumText;
    private bool flag = true;
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
        countText = hiddenCanvas.transform.GetChild(0).GetComponent<Text>();
        playerNumText = hiddenCanvas.transform.GetChild(1).GetComponent<Text>();
        playerNumText.text = "You are player" + oneVariables.playerNum() + "!";
    }

    // Update is called once per frame
    void Update()
    {
        timer = oneVariables.countDownTimer;
        mySlider.value = oneVariables.playerHP[0];
        enemySlider.value = oneVariables.playerHP[1];
        if (timer > 1)
        {
            countText.text = ((int)timer).ToString();
        }
        else
        {
            if (flag)
            {
                ChangeActive();
                flag = false;
            }
        }
        if (oneVariables.playerHP[0] <= 0)
        {
            oneVariables.playerHP[0] = 0;
            GameOver(PhotonNetwork.PlayerList[1].NickName);
            //クライアントの勝利！
        }
        else if (oneVariables.playerHP[1] <= 0)
        {
            oneVariables.playerHP[1] = 0;
            GameOver(PhotonNetwork.PlayerList[0].NickName);
            //マスターの勝利！
        }
    }
    public void Disconnection()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ChangeActive()
    {
        canvas.gameObject.SetActive(true);
        hiddenCanvas.gameObject.SetActive(false);
    }

    public void GameOver(string winner)
    {
        winnerText.text = "勝者: " + winner;
    }
}
