using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mortal
{

    [SerializeField]
    private GameObject _dangerZone;

    [SerializeField]
    private ScriptableActions[] _actions;

    private Player _player;

    private int _currentAction;

    [SerializeField]
    private GameObject[] _intentions;

    private void OnEnable()
    {
        GameManager.EndTurn += EnemyTurn;
        GameManager.EndTurn += ResetIntention;
        GameManager.EndTurn += GetPoisoned;
        GameManager.BeginTurn += ShowIntention;
        GameManager.FirstTurn += ShowIntention;
    }
    private void OnDisable()
    {
        GameManager.EndTurn -= EnemyTurn;
        GameManager.EndTurn -= ResetIntention;
        GameManager.EndTurn -= GetPoisoned;
        GameManager.BeginTurn -= ShowIntention;
        GameManager.FirstTurn -= ShowIntention;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHealthBar();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _currentAction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShuffleActions()
    {
        ScriptableActions aux;
        int r;

        _strength += _strength;

        for (int i = 0; i < _actions.Length; i++)
        {
            r = Random.Range(0, _actions.Length);

            aux = _actions[i];
            _actions[i] = _actions[r];
            _actions[r] = aux;
        }
    }

    private void EnemyTurn()
    {
        ResetState();
        Action();
    }

    private void ResetState()
    {
        _shield = 0;
        SetHealthBar();
    }

    private void ShowIntention()
    {
        if (_currentAction >= _actions.Length)
        {
            _currentAction = 0;
            ShuffleActions();
        }

        if ((_actions[_currentAction].Attack+_strength) > 0)
        {
            _intentions[0].SetActive(true);
            _intentions[0].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (_actions[_currentAction].Attack + _strength).ToString();
        }

        if (_actions[_currentAction].Defense > 0)
        {
            _intentions[1].SetActive(true);
            _intentions[1].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = _actions[_currentAction].Defense.ToString();
        }
    }

    private void ResetIntention()
    {
        _intentions[0].SetActive(false);
        _intentions[1].SetActive(false);
    }

    private void Action()
    {
        if(_currentAction >= _actions.Length)
        {
            _currentAction = 0;
            ShuffleActions();
        }
        _player.GetDamage(_actions[_currentAction].Attack + _strength);

        _shield = _actions[_currentAction].Defense;

        _currentAction++;

        SetHealthBar();
    }

    
}
