using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneMatchingUI : MonoBehaviour
{
    public AudioClip buttonSE2;
    private SEManager sEManager;
    // Start is called before the first frame update
    void Start()
    {
        sEManager = GameObject.FindObjectOfType<SEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToLobby()
    {
        sEManager.PlaySE(buttonSE2);
        SceneManager.LoadScene("Lobby");
    }
}
