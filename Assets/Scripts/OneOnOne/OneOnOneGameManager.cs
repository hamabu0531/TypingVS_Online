using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneOnOneGameManager : MonoBehaviour
{
    private int i = 0;
    public int correct = 0, miss = 0;
    public Text questionText, inputText;
    string selectedText, enteredText, bufText;
    public GameObject Variables;
    public TextAsset gameText;
    public AudioClip misTypeSE, correctSE;
    public bool isGameover = false;
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
        if (!isGameover)
        {
            if (Input.GetKeyDown(selectedText[i].ToString()))
            {
                // “r’†‚Ì•¶š‚ğ“ü—Í
                if (i < selectedText.Length - 1)
                {
                    correct++;
                    enteredText = "<color=#000000>" + bufText.Substring(0, i + 1) + "</color>" + bufText.Substring(i + 1);
                    i++;
                }
                // ÅŒã‚Ì•¶š‚ğ“ü—Í
                else
                {
                    int damage = (i + 1) * 2;
                    //Š®—¹ˆ—
                    oneVariables.Attack(damage, 2);
                    //‰Šú‰»
                    i = 0;
                    selectedText = gameData[Random.Range(0, gameData.Length)];
                    questionText.text = selectedText;
                    bufText = selectedText;
                    enteredText = selectedText;
                    GetComponent<AudioSource>().PlayOneShot(correctSE);
                }
            }
            else if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                miss++;
                oneVariables.Attack(-2, 0);
                GetComponent<AudioSource>().PlayOneShot(misTypeSE);
            }
            inputText.text = enteredText;
        }
    }    
}
