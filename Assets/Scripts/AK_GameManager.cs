using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AK_GameManager : MonoBehaviour
{   
    private int level;
    private int lives;
    private int score;
    private int conquerorsDestroyed;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        score = 0;

        conquerorsDestroyed = 0;

        LoadLevel(1);
    }

    private void LoadLevel(int index)
    {
        level = index;

        Camera camera = Camera.main;

        if (camera != null)
        {
            camera.cullingMask = 0;
        }

        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void LevelComplete()
    {
        // score += 1000;
        AK_ScoreManager.instance.AddToScore();



        int nextLevel = level + 1;

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadLevel(1);
        }
    }

    public void CC_LevelComplete()
    {
        conquerorsDestroyed++;

        if (conquerorsDestroyed >= 5) {
            LoadLevel(4);
            // int nextLevel = level + 1;

            // if (nextLevel < SceneManager.sceneCountInBuildSettings)
            // {
            //     LoadLevel(nextLevel);
            // }
            // else
            // {
            //     LoadLevel(1);
            // }
        }
    }

    public void LevelFailed()
    {
        lives--;

        if (lives <= 0)
        {
            NewGame();
        }
        else 
        {
            LoadLevel(level);
        }
    }
}
