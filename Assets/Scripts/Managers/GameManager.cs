using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            Debug.Log("Lives are set to: " + lives.ToString());
        }
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform currentSpawnPoint;
    [HideInInspector] public Level currentLevel;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("Title");
            }
            else if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene("Level");
            }
            else if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("Title");
                _lives = maxLives;
            }
            else if (SceneManager.GetActiveScene().name == "Victory")
            {
                SceneManager.LoadScene("Title");
                _lives = maxLives;
            }
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
        
        Debug.Log("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");

        Debug.Log("Victory");
    }

    public void Respawn()
    {
        playerInstance.transform.position = currentSpawnPoint.position;

        Debug.Log("Respawned");
    }
}