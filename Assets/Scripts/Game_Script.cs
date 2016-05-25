using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Script : MonoBehaviour {

    public static int score = 0;
    public static int lives = 100;
    public static int currentLevel = 1;
    int bricksCount;
    Text livesText;
    Text scoreText;
    bool endGame;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "GameOver")
            endGame = true;
        else
            endGame = false;
        scoreText = GameObject.Find("ScoreLabel").GetComponent<Text>();
        livesText = GameObject.Find("LivesLabel").GetComponent<Text>();
        bricksCount = GameObject.FindGameObjectsWithTag("Brick").Length;
        Debug.Log("Current level: " + currentLevel + "\n Scenes: " + SceneManager.sceneCountInBuildSettings);
    }
	
	// Update is called once per frame
	void Update () {
        
        // Game lost condition        
        if (lives == 0 && !endGame)
        {
            Debug.Log("Game Lost");
            endGame = true;
            SceneManager.LoadScene("GameOver");
        }
        // Level win condition
        // Loads next level            
        if (bricksCount == 0 && !endGame)
        {
            endGame = true;
            Debug.Log("Level Won");            
            currentLevel++;
            //Check if there scene to load
            if(SceneManager.sceneCountInBuildSettings >= currentLevel + 1) // +1 for each non level scene eg. GameOver
                SceneManager.LoadScene("Level_0" + currentLevel);
            else
                SceneManager.LoadScene("GameOver");
        }
        // GUI labels update            
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        
        // Debug scene load (Some cheat button) 
        if (Input.GetKeyDown(KeyCode.P))
            bricksCount = 0;
        if (Input.GetKeyDown(KeyCode.O))
            lives = 0;
        // Quit game if Esc pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    /// <summary>
    /// Brick destruction
    /// updates score
    /// count bricks
    /// </summary>
    public void BrickDestroyed()
    {
        bricksCount--;
        score++;        
        Debug.Log("Bricks left: " + bricksCount);
    }

    /// <summary>
    /// Life minus one
    /// </summary>
    public void LoseOneLife()
    {
        lives--;        
        Debug.Log("Lives left: " + lives);
    }
}
