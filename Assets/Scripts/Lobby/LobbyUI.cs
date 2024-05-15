using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public Canvas canvas, hiddenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OneOnOne()
    {
        SceneManager.LoadScene("OneOnOne");
    }
    public void TwoOnTwo()
    {
        SceneManager.LoadScene("TwoOnTwo");
    }
}
