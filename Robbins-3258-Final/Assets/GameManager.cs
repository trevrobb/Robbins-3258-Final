using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    public float timer = 180f;
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText2;

    public Canvas gameOverCanvas;

    public Canvas IntroCanvas;

    public Image crossHair;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IntroCanvas.gameObject.activeInHierarchy){
            crossHair.gameObject.SetActive(true);
            scoreText.text = "Score: " + score;

            timer -= Time.deltaTime;

            timerText.text = "Time Left: " + Mathf.Floor(timer / 60) + ":" + (int)timer % 60;
            if (timer <= 0)
            {

                scoreText2.text = "Score: " + score;
                gameOverCanvas.gameObject.SetActive(true);
            }
        }
        
        if (IntroCanvas.gameObject.activeInHierarchy)
        {
            crossHair.gameObject.SetActive(false);
            scoreText.text = "";
            timerText.text = "";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IntroCanvas.gameObject.SetActive(false);
            }
        }

    }

    public void addScore(int amount)
    {
        score += amount;
    }
}
