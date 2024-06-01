using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OneOnOneUI : MonoBehaviour
{
    public Canvas canvas, hiddenCanvas, hiddenCanvas2;
    public GameObject Variables;
    public Slider p1Slider, p2Slider;
    private float timer;
    public Text countText, musicText, player1, player2;
    public AudioClip[] audioClips;
    private bool flag = true, flag2 = true;
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll<AudioClip>("Musics/battle/");
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
        countText = hiddenCanvas.transform.GetChild(0).GetComponent<Text>();
        player1.text = PhotonNetwork.PlayerList[0].NickName;
        player2.text = PhotonNetwork.PlayerList[1].NickName;
        SelectMusic();
    }

    // Update is called once per frame
    void Update()
    {
        timer = oneVariables.countDownTimer;
        p1Slider.value = oneVariables.playerHP[0];
        p2Slider.value = oneVariables.playerHP[1];
        //バーの色の変更
        if (oneVariables.playerHP[0] <= 20)
        {
            p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
        if (oneVariables.playerHP[1] <= 20)
        {
            p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
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
        if (oneVariables.playerHP[0] <= 0 && flag2)
        {
            oneVariables.playerHP[0] = 0;
            GameOver(PhotonNetwork.PlayerList[1].NickName);
            canvas.gameObject.SetActive(false);
            flag2 = false;
            //クライアントの勝利！
        }
        else if (oneVariables.playerHP[1] <= 0 && flag2)
        {
            oneVariables.playerHP[1] = 0;
            GameOver(PhotonNetwork.PlayerList[0].NickName);
            canvas.gameObject.SetActive(false);
            flag2 = false;
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
        hiddenCanvas2.gameObject.SetActive(true);
        hiddenCanvas2.transform.GetChild(0).gameObject.GetComponent<Text>().text = winner + "の勝利！";
    }
    void SelectMusic()
    {
        int ran = Random.Range(0, audioClips.Length);
        GetComponent<AudioSource>().clip = audioClips[ran];
        musicText.text = "♪ " + audioClips[ran].name;
        StartCoroutine(FadeOut());
        GetComponent<AudioSource>().Play();
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                musicText.color -= new Color32(0, 0, 0, 1);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
