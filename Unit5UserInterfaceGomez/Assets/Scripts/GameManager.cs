using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.EditorTools;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Slider slider;
    public bool isPaused;

    public GameObject titleScreen;
    public GameObject pauseScreen;

    public Button restartButton;
    public bool isGameActive;
    private int score;
    private int lives = 3;
    private float spawnRate = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = slider.value;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int Index = Random.Range(0, targets.Count);
            Instantiate(targets[Index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void MinusLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 4;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        MinusLife();

        titleScreen.gameObject.SetActive(false);
    }

    void Pause()
    {
        isPaused = !isPaused;
        if(isPaused==true)
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
    }
}
