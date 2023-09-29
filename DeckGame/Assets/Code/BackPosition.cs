using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPosition : MonoBehaviour
{
    [SerializeField]
    private Transform _initialPosition;

    [SerializeField]
    //private Transform _discardPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Transform pos)
    {
        _initialPosition = pos;
    }

    public void GoToPosition()
    {
        transform.position = _initialPosition.position;//fazer andar até a posição e depois fazer o ir pro descarte
    }

    public void GoToDiscard()
    {
       // transform.position = _discardPosition.position;
    }
}
