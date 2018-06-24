using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image[] healths;
    public GameObject[] pauseMenu;
    public GameObject[] overMenu;
    public GameObject[] mainMenu;
    public GameObject[] optionMenu;
    public GameObject[] finishMenu;
    public Text scoreText;
    // Use this for initialization
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectsWithTag("ShowOnPause");
        overMenu = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        mainMenu = GameObject.FindGameObjectsWithTag("ShowOnMainMenu");
        optionMenu = GameObject.FindGameObjectsWithTag("ShowOnOption");
        finishMenu = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        hideOption();
    }

    void Update()
    {
        if (DataManager.instance.CheckGameState("PauseState"))
        {
            showPaused();
            hideFinished();
        }
        else
        {
            hidePaused();
            if (DataManager.instance.CheckGameState("GameOverState"))
            {
                hideFinished();
                showOver();
            }
            else
            {
                hideOver();
                if (DataManager.instance.CheckGameState("PlayingState"))
                {
                    UpdateScore();
                    hideFinished();
                }
                else
                {
                    if (DataManager.instance.CheckGameState("FinishState"))
                    {
                        showFinished();
                    }
                    else
                    {
                        hideFinished();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    public void UpdateHealth(int health)
    {
        for (int i = 0; i < healths.Length; i++)
        {
            if (i < health/10)
            {
                healths[i].gameObject.SetActive(true);
            }
            else
            {
                healths[i].gameObject.SetActive(false);
            }

        }
    }

    /*
     public void ButtonLevelOne(){
        SceneManager.LoadScene("LevelOne");   
     }
         */
    
    public void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = DataManager.instance.score.ToString();
        }
    }   

    public void LoadLevel(string level)
    {
        new LoadScene().Execute(level);
    }

    public void MainMenu()
    {
        new LoadMainMenu().Execute();
    }

    public void Restart()
    {
        new ReloadLevel().Execute();
    }

    public void Resume()
    {
        new PauseGame().Execute();
    }

    public void Quit()
    {
        new ExitCommand().Execute();
    }

    public void ConfirmOption()
    {
        hideOption();
        ShowMain();
    }

    public void Option()
    {
        hideMain();
        showOption();
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(false);
        }
    }

    public void showFinished()
    {
        foreach (GameObject g in finishMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideFinished()
    {
        foreach (GameObject g in finishMenu)
        {
            g.SetActive(false);
        }
    }

    public void showOver()
    {
        foreach (GameObject g in overMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideOver()
    {
        foreach (GameObject g in overMenu)
        {
            g.SetActive(false);
        }
    }

    public void ShowMain()
    {
        foreach (GameObject g in mainMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideMain()
    {
        foreach (GameObject g in mainMenu)
        {
            g.SetActive(false);
        }
    }

    public void showOption()
    {
        foreach (GameObject g in optionMenu)
        {
            g.SetActive(true);
            g.GetComponentInChildren<Slider>().value = AudioListener.volume;
        }
    }

    public void hideOption()
    {
        foreach (GameObject g in optionMenu)
        {
            g.SetActive(false);
        }
    }

    public void changeVolume()
    {
        AudioListener.volume = GameObject.Find("VolumeSlider").GetComponent<Slider>().value;
    }

}
