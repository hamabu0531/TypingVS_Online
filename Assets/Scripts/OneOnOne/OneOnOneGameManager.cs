using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneOnOneGameManager : MonoBehaviour
{
    int i = 0;
    public Text questionText, inputText;
    string selectedText, enteredText = "";
    public GameObject Variables;
    public TextAsset gameText;
    private string[] gameData;
    public AudioClip[] audioClips;
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        gameData = gameText.text.Split(",");
        selectedText = gameData[Random.Range(0, gameData.Length)];
        questionText.text = selectedText;
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
        SelectMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(selectedText[i].ToString()))
        {
            if (i < selectedText.Length-1)
            {
                enteredText = enteredText + selectedText[i];
                i++;
            }
            else
            {
                //Š®—¹ˆ—
                enteredText = "";
                oneVariables.Attack();
                //‰Šú‰»
                i = 0;
                selectedText = gameData[Random.Range(0, gameData.Length)];
                questionText.text = selectedText;
            }
        }
        inputText.text = enteredText;
    }

    void SelectMusic()
    {
        int ran = Random.Range(0, audioClips.Length);
        GetComponent<AudioSource>().clip = audioClips[ran];
        GetComponent<AudioSource>().Play();
    }
}
