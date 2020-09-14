using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float time = 120;
    private bool paused = false, gameover = false;

    public Text info, timer;
    public GameObject filter, button;
    public BallController p1, p2;
    
    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    
    void Update()
    {
        int seconds = (int)(time % 60);
        if (seconds >= 10) { timer.text = (int)(time / 60) + ":" + seconds; }
        else if (seconds < 10) { timer.text = (int)(time / 60) + ":0" + seconds; }

        if (Input.GetKeyDown("escape") && !gameover)
        {
            info.text = "Game Paused";
            info.color = Color.red;
            paused = !paused;
        }

        if (paused) { timer.color = Color.yellow;  button.SetActive(true); filter.SetActive(true); info.gameObject.SetActive(true); Time.timeScale = 0; }
        else { timer.color = Color.black; button.SetActive(false); filter.SetActive(false); info.gameObject.SetActive(false); Time.timeScale = 1; }

        if(time < 20) { timer.color = Color.red; }

        time -= Time.deltaTime;

        if(time <= 0)
        {
            gameover = true;
            paused = true;

            if(p1.getScore() > p2.getScore())
            {
                info.text = "Player 1 Wins!";
                info.color = new Color(255, 120, 50);
            }
            else if (p2.getScore() > p1.getScore())
            {
                info.text = "Player 2 Wins!";
                info.color = new Color(255, 50, 255);
            }
            else
            {
                info.text = "Draw";
                info.color = new Color(255, 255, 50);
            }
        }
    }

    public void ButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
