using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class TutorialUI : MonoBehaviour
{
    public Canvas canvas, hiddenCanvas;
    public GameObject gManager;
    public Slider p1Slider, p2Slider;
    public Text playerName;
    public TextMeshProUGUI musicText, correctText, missText, probText, resultText;
    public Image bg;
    public AudioClip[] audioClips;
    public Sprite[] backgrounds;
    private bool flag = true, isGameOver = false, flag3 = true;
    int[] playerHP = { 100, 100 };
    TutorialGameManager tGameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("name");
        backgrounds = Resources.LoadAll<Sprite>("Images/battle_backgrounds/");
        audioClips = Resources.LoadAll<AudioClip>("Musics/tutorial/");
        tGameManager = gManager.GetComponent<TutorialGameManager>();
        bg.sprite = backgrounds[Random.Range(0, 4)];
        SelectMusic();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerWin(int correct, int miss)
    {
        canvas.gameObject.SetActive(false);
        int allTyped = correct + miss;
        correctText.text = "総タイプ数: " + allTyped;
        missText.text = "ミスタイプ数: " + miss;
        probText.text = "正確性: " + (int)(((float)tGameManager.correct / (float)allTyped) * 100) + "%";
        hiddenCanvas.gameObject.SetActive(true);
        resultText.text = playerName.text + "の勝利！";
    }

    public void PlayerLose(int correct, int miss)
    {
        canvas.gameObject.SetActive(false);
        int allTyped = correct + miss;
        correctText.text = "総タイプ数: " + allTyped;
        missText.text = "ミスタイプ数: " + miss;
        probText.text = "正確性: " + (int)(((float)tGameManager.correct / (float)allTyped) * 100) + "%";
        hiddenCanvas.gameObject.SetActive(true);
        resultText.text = playerName.text + "の負け...";
    }
    void SelectMusic()
    {
        int ran = Random.Range(0, audioClips.Length);
        GetComponent<AudioSource>().clip = audioClips[ran];
        musicText.text = "♪ " + audioClips[ran].name;
        StartCoroutine(FadeOut());
        GetComponent<AudioSource>().Play();
    }

    public void SetPlayerHP(int[] playerHP)
    {
        p1Slider.value = playerHP[0];
        p2Slider.value = playerHP[1];

        //バーの色の変更
        if (playerHP[0] <= 20)
        {
            p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            p1Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
        if (playerHP[1] <= 20)
        {
            p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            p2Slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
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
