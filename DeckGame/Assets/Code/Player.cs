using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mortal
{

    private void OnEnable()
    {
        GameManager.BeginTurn += ResetState;
    }
    private void OnDisable()
    {
        GameManager.BeginTurn -= ResetState;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetState()
    {
        _shield = 0;

        SetHealthBar();
    }
}
