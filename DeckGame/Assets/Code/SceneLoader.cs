using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    private int _currentScene;
    private GameObject[] _enemies;
    private GameObject _player;

    [SerializeField]
    private bool _isMenu;
    private bool _lastScene;

    [SerializeField]
    private int _numberOfScenes;

    [SerializeField]
    private GameObject _retryCanvas;
    [SerializeField]
    private GameObject _nextCanvas;

    [SerializeField]
    private GameObject _resetCanvas;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        _currentScene = 1;

        _lastScene = false;
    }

    // Update is called once per frame

    private void OnEnable()
    {
        GameManager.BeginTurn += GetEnemy;
    }
    private void OnDisable()
    {
        GameManager.BeginTurn -= GetEnemy;
    }

    private void GetEnemy()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        _player = GameObject.FindGameObjectWithTag("Player");

        _isMenu = false;
    }

    void Update()
    {
        if (!_lastScene)
        {
            if (!_isMenu)
            {
                TestToFinish();
                TestToRetry();
            }
        }
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);

        Destroy(this.gameObject);
    }

    private int TestToRetry()
    {
        if(_player != null)
        {
            return 1;
        }

        _retryCanvas.SetActive(true);

        GameManager.Instance.PauseGameInvoke();

        return 0;
    }

    private int TestToFinish()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                return 1;
            }
        }




        _nextCanvas.SetActive(true);

        GameManager.Instance.PauseGameInvoke();



        return 0;
    }

    public void GoToNextScene()
    {
        _isMenu = true;

        _currentScene++;

        SceneManager.LoadScene(_currentScene);

        if(_currentScene == _numberOfScenes)
        {
            _lastScene = true;
            Destroy(_player);
            _resetCanvas.SetActive(true);
        }

        _nextCanvas.SetActive(false);
    }

    public void GoToScene(int i)
    {
        _isMenu = true;

        SceneManager.LoadScene(i);

        _retryCanvas.SetActive(false);

    }
}
