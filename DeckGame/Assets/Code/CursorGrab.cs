using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGrab : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private string _objectTag;

    private Vector3 _cameraPostion;

    private GameObject _grabedObject;

    public bool _isPause;

    private void OnEnable()
    {
        GameManager.PauseGame += PauseGame;
    }
    private void OnDisable()
    {
        GameManager.PauseGame -= PauseGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isPause = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!_isPause)
        {
            _cameraPostion = _mainCamera.ScreenToWorldPoint(Input.mousePosition);



            if (Input.GetMouseButtonDown(0))
            {
                Grab();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                UnGrab();
            }


            if (_grabedObject != null)
            {
                FollowMouse();

            }
        }

       

    }

    private void PauseGame()
    {
        _isPause = true;
    }

    private void FollowMouse()
    {
        _grabedObject.transform.position = new Vector3(_cameraPostion.x, _cameraPostion.y, _grabedObject.transform.position.z);
    }

    private void UnGrab()
    {
        if (_grabedObject != null)
        {
            _grabedObject.GetComponent<Card>().Drop();
            _grabedObject = null;
        }
        
    }

    private void Grab()
    {
        RaycastHit2D hit;

        

        hit = Physics2D.Raycast(_cameraPostion, Vector3.up, 1f);

        if (hit)
        {
            if (hit.transform.CompareTag(_objectTag))
            {
                _grabedObject = hit.transform.gameObject;
            }
        }
    }
}
