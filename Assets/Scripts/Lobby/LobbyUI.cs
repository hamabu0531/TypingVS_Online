using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    public Canvas canvas, hiddenCanvas, howToPlayCanvas;
    public Text playerList, pingText;
    public TextMeshProUGUI messageText;
    public AudioClip buttonSE, buttonSE2;
    public GameObject onlineList;
    private SEManager sEManager;
    // Start is called before the first frame update
    void Start()
    {
        sEManager = GameObject.FindObjectOfType<SEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            onlineList.SetActive(true);
        }else
        {
            onlineList.SetActive(false);
        }
    }

    public void ChangeActive()
    {
        if (canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
        }
        if (hiddenCanvas.gameObject.activeSelf)
        {
            hiddenCanvas.gameObject.SetActive(false);
        }
        else
        {
            hiddenCanvas.gameObject.SetActive(true);
        }

    }

    public void Disconnection()
    {
        SceneManager.LoadScene("Title");
        sEManager.PlaySE(buttonSE2);
    }

    public void OneOnOne()
    {
        SceneManager.LoadScene("OneMatching");
        sEManager.PlaySE(buttonSE);
    }
    public void TwoOnTwo()
    {
        SceneManager.LoadScene("TwoMatching");
        sEManager.PlaySE(buttonSE);
    }

    public void HowToPlay()
    {
        if (howToPlayCanvas.gameObject.activeSelf)
        {
            howToPlayCanvas.gameObject.SetActive(false);
        }else
        {
            howToPlayCanvas.gameObject.SetActive(true);
        }
    }
}
