using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerController curPlayer;

    [Header("HUDObject")]
    public GameObject HUD;

    [Header("Text")]
    public Text livesText;
    public Text scoreText;

    [Header("Bars")]
    public GameObject healthBar;
    int oldLives;  
    int oldScore;

    void Start()
    {
        oldLives = GameManager.instance.lives;
        oldScore = curPlayer.score;
        //GameManager.instance.OnLifeValueChanged.AddListener((value) => LifeUpdate(value));
    }

    //create a function that takes an int parameter
    void LifeUpdate(int value)
    {
        livesText.text = "LIVES: " + value.ToString();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level" && Time.timeScale == 1)
        {
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
        }
        
        livesText.text = "LIVES: " + oldLives;
        if (oldLives > GameManager.instance.lives)
        {
            oldLives--;
            livesText.text = "LIVES: " + GameManager.instance.lives.ToString();
        }
        
        scoreText.text = "SCORE: " + oldScore;
        if (oldScore < curPlayer.score)
        {
            oldScore++;
            scoreText.text = "SCORE: " + curPlayer.score.ToString();
        }
    }
}
