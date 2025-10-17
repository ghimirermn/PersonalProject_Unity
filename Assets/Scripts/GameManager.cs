using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI TitleText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;

    public int statueHitCount = 0;
    public int maxStatueHits = 12;
    
    private bool gameStarted = false;

    void Start()
    {
        score = 0;
        UpdateScore(0);
        gameOverText.gameObject.SetActive(false);
        
        // Setup Start button
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Cubes Destroyed: " + score;
    }


    public void GameOver()
    {
        gameStarted = false;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over!";
        restartButton.gameObject.SetActive(true);
        controlsText.gameObject.SetActive(false);
    }
    
    public void RegisterStatueHit()
    {
        statueHitCount++;
        Debug.Log("statue has been hit");
        if (statueHitCount >= maxStatueHits)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        score= 0;
        statueHitCount = 0;
        UpdateScore(0);

        controlsText.gameObject.SetActive(true);
        TitleText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

}
