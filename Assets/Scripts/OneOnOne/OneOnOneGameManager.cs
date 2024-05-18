using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneOnOneGameManager : MonoBehaviour
{
    int i = 0;
    public Text questionText, inputText;
    string sampleText = "sample", enteredText = "";
    public GameObject Variables;
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        questionText.text = sampleText;
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(sampleText[i].ToString()))
        {
           // Debug.Log(sampleText[i]);
            if (i < sampleText.Length-1)
            {
                enteredText = enteredText + sampleText[i];
                i++;
            }
            else
            {
                //Š®—¹ˆ—
                enteredText = "";
                oneVariables.Attack();
                //‰Šú‰»
                i = 0;
                sampleText = "randomstring";
                questionText.text = sampleText;
            }
        }
        inputText.text = enteredText;
    }
}
