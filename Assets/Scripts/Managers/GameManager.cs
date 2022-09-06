using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private int _lives = 3;
    public int maxLives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {
                Respawn();
            }

            _lives = value;
            

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            if (_lives <= 0)
            {
                GameOver();
            }

            OnLifeValueChanged.Invoke(_lives);
        }
    }

    private int _score = 0;

    public int score
    {
        get { return _score; }
        set
        { 
            _score = value;

            OnScoreValueChanged.Invoke(_score);

            Debug.Log("Score: " + _score.ToString());
        }
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform currentSpawnPoint;
    [HideInInspector] public Level currentLevel;
    [HideInInspector] public UnityEvent<int> OnLifeValueChanged;
    [HideInInspector] public UnityEvent<int> OnScoreValueChanged;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentSpawnPoint = spawnLocation;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void Respawn()
    {
        playerInstance.transform.position = currentSpawnPoint.position;

        playerInstance.CallRespawnSound();
    }
}
