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
    OneOnOneVariables oneVariables;
    // Start is called before the first frame update
    void Start()
    {
        oneVariables = Variables.GetComponent<OneOnOneVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.value = oneVariables.playerHP[0];
        enemySlider.value = oneVariables.playerHP[1];
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
}
