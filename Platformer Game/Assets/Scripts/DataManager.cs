using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public static GameState gameState;
    public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        SetGameState(new MainMenuState());
    }

    public bool CheckGameState(string state)
    {
        return DataManager.gameState.GetType().Name == state;
    }

    public void SetGameState(GameState state)
    {
        if (gameState != null)
            gameState.OnStateExit();

        gameState = state;
        gameObject.name = "DataManager - " + state.GetType().Name;

        if (gameState != null)
            gameState.OnStateEnter();
    }
}

