using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGameManager : MonoBehaviour
{
    private int i = 0;
    public int correct = 0, miss = 0;
    public Text questionText, inputText;
    string selectedText, enteredText, bufText;
    public TextAsset gameText;
    public AudioClip misTypeSE;
    public GameObject tManager;
    public bool isGameover = false;
    private string[] gameData;
    public int[] playerHP = { 100, 100 };
    TutorialUI tUI;

    // Start is called before the first frame update
    void Start()
    {
        tUI = tManager.GetComponent<TutorialUI>();
        gameData = gameText.text.Split(",");
        selectedText = gameData[Random.Range(0, gameData.Length)];
        questionText.text = selectedText;
        enteredText = selectedText;
        bufText = selectedText;
    }

    // Update is called once per frame
    void Update()
    {
        //バーの色の変更
        if (playerHP[0] <= 20)
        {
            tUI.p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            tUI.p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
        if (playerHP[1] <= 20)
        {
            tUI.p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            tUI.p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        if (!isGameover)
        {
            if (Input.GetKeyDown(selectedText[i].ToString()))
            {
                if (i < selectedText.Length - 1)
                {
                    correct++;
                    enteredText = "<color=#000000>" + bufText.Substring(0, i + 1) + "</color>" + bufText.Substring(i + 1);
                    i++;
                }
                else
                {
                    int damage = (i + 1) * 2;
                    //完了処理
                    playerHP[1] -= damage; // 敵へダメージ
                    //初期化
                    i = 0;
                    selectedText = gameData[Random.Range(0, gameData.Length)];
                    questionText.text = selectedText;
                    bufText = selectedText;
                    enteredText = selectedText;
                }
            }
            else if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                miss++;
                playerHP[0] -= 2; // 自身へ2ダメージ
                GetComponent<AudioSource>().PlayOneShot(misTypeSE);
            }
            inputText.text = enteredText;
        }

        if (playerHP[1] <= 0 && !isGameover)
        {
            Debug.Log("ゲーム終了!");
            playerHP[1] = 0;
            isGameover = true;
            tUI.PlayerWin(correct, miss);
        }
        else if (playerHP[0] <= 0 && !isGameover)
        {
            playerHP[0] = 0;
            isGameover = true;
            tUI.PlayerLose(correct, miss);
        }
    }
}
