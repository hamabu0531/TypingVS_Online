using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneOnOneGameManager : MonoBehaviour
{
    int i = 0;
    public Text questionText, inputText;
    string selectedText, enteredText, bufText;
    public GameObject Variables;
    public TextAsset gameText;
    public AudioClip misTypeSE;
    private string[] gameData;
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        gameData = gameText.text.Split(",");
        selectedText = gameData[Random.Range(0, gameData.Length)];
        questionText.text = selectedText;
        enteredText = selectedText;
        bufText = selectedText;
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(selectedText[i].ToString()))
        {
            if (i < selectedText.Length-1)
            {
                enteredText = "<color=#000000>" + bufText.Substring(0,i+1) + "</color>" + bufText.Substring(i+1);
                i++;
            }
            else
            {
                //äÆóπèàóù
                oneVariables.Attack(10, 2);
                //èâä˙âª
                i = 0;
                selectedText = gameData[Random.Range(0, gameData.Length)];
                questionText.text = selectedText;
                bufText=selectedText;
                enteredText=selectedText;
            }
        }
        else if (Input.anyKeyDown)
        {
            oneVariables.Attack(-1, 0);
            GetComponent<AudioSource>().PlayOneShot(misTypeSE);
        }
        inputText.text = enteredText;
    }

    
}
