using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Text inputText;
    public AudioClip buttonSE, buttonSE2;
    private SEManager seManager;
    // Start is called before the first frame update
    void Start()
    {
        seManager = GameObject.FindObjectOfType<SEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinLobby()
    {
        seManager.PlaySE(buttonSE);
        PlayerPrefs.SetString("name", inputText.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        seManager.PlaySE(buttonSE2);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}