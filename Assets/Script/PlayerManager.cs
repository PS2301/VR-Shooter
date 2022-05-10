using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public static float playerHealth, currentScore;
    public Image healthImg;
    public TMP_Text score, highScore;
    public GameObject gameON, gameOFF, enemies;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
            GameOver();
        healthImg.fillAmount = playerHealth;
        score.text = currentScore.ToString();
    }

    public void GameStart()
    {
        gameON.SetActive(true);
        gameOFF.SetActive(false);
        playerHealth = 1;
        currentScore = 0;
        score.text = "0";
    }

    public void GameOver()
    {
        if (currentScore > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            highScore.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("HighScore").ToString();
        }
        foreach(Transform child in enemies.transform)
        {
            child.gameObject.GetComponent<EnemyController>().Death();
        }
        gameON.SetActive(false);
        gameOFF.SetActive(true);
    }
}
