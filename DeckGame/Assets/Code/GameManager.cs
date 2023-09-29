using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public int Energy;

    public static event Action BeginTurn;
    public static event Action FirstTurn;

    public static event Action EndTurn;
    public static event Action PauseGame;


    [SerializeField]
    private TMPro.TextMeshProUGUI _energyText;


    public bool _isPause;

    public int _currentDeck;

    private void OnEnable()
    {
        GameManager.BeginTurn += ResetEnergy;
        GameManager.PauseGame += GamePaused;
    }
    private void OnDisable()
    {
        GameManager.BeginTurn -= ResetEnergy;
        GameManager.PauseGame -= GamePaused;
    }

    private void Awake()
    {
        Instance = this;

        //_isPause = false;
    }

    public void PauseGameInvoke()
    {
        PauseGame?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        //BeginTurn?.Invoke();
        if(!_isPause)
            FirstTurn?.Invoke();

        
    }

    // Update is called once per frame
    

    public void UpdateEnergy()
    {
        _energyText.text = Energy.ToString();
    }

    public void UseEnergy(int i)
    {
        Energy -= i;

        UpdateEnergy();
    }

    public void ResetEnergy()
    {
        Energy = 3;
        UpdateEnergy();
    }

    public void EndTurnButton()
    {
        if(!_isPause)
            StartCoroutine(EndTurnButtonTest());
    }

    public IEnumerator  EndTurnButtonTest()
    {

        EndTurn?.Invoke();

        yield return new WaitForSeconds(1);

        BeginTurn?.Invoke();
    }


    private void GamePaused()
    {
        _isPause = true;
    }

    public void UnpauseGame()
    {
        _isPause = false;
        FirstTurn?.Invoke();
    }
}
