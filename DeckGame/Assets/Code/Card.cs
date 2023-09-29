using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    private ScriptableCard _cardInfo;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TMPro.TextMeshProUGUI _name; 
    
    [SerializeField]
    private TMPro.TextMeshProUGUI _energy;

    [SerializeField]
    private TMPro.TextMeshProUGUI _type;

    [SerializeField]
    private TMPro.TextMeshProUGUI _descryption;


    private bool _isDroped;

    [SerializeField]
    private BackPosition _backPosition;

    private Mortal _enemy;
    private Mortal _player;

    public Deck _discard;

    private void OnEnable()
    {
        GameManager.EndTurn += DiscardCard;
    }
    private void OnDisable()
    {
        GameManager.EndTurn -= DiscardCard;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeDisplay();

        _isDroped = false;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mortal>();
    }

    public void SetCard(ScriptableCard card)
    {
        _cardInfo = card;
        InitializeDisplay();
    }

    public int Drop()
    {
        _isDroped = true;

        if(GameManager.Instance.Energy >= _cardInfo.Energy)
        {
            if (_cardInfo.Type == "ATQ")//atq
            {
                if (HasMortal("Enemy"))
                {
                    
                    GameManager.Instance.UseEnergy(_cardInfo.Energy);

                    _enemy.GetDamage(_cardInfo.Damage + _player._strength);

                    _enemy.SetPoison(_cardInfo.Poison);

                    _player.SetShield(_cardInfo.Shield);

                    _player.SetStrength(_cardInfo.Strength);

                    DiscardCard();
                }
                else
                {
                    _backPosition.GoToPosition();
                }
            }
            else// def 
            {
                if (HasMortal("Player"))
                {
                    GameManager.Instance.UseEnergy(_cardInfo.Energy);

                    _player.SetShield(_cardInfo.Shield);

                    _player.SetStrength(_cardInfo.Strength);

                    DiscardCard();
                }
                else
                {
                    _backPosition.GoToPosition();
                }
            }
        }
        else
        {
            _backPosition.GoToPosition();
        }

        

        

        return 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiscardCard()
    {
        _discard.AddCard(_cardInfo);

        Destroy(this.gameObject);
    }

    private void InitializeDisplay()
    {
        _name.text = _cardInfo.Name;
        _energy.text = _cardInfo.Energy.ToString();
        _type.text = _cardInfo.Type;
        _descryption.text = _cardInfo.Descryption;

        _image.sprite = _cardInfo.Image;
    }

    private bool HasMortal(string tag)
    {
        RaycastHit2D[] hit;



        hit = Physics2D.RaycastAll(transform.position, Vector2.up, 1f);

        foreach (RaycastHit2D item in hit)
        {
            if (item)
            {
                if (item.transform.CompareTag("Enemy"))
                {
                    _enemy = item.transform.gameObject.GetComponent<Mortal>();
                    if(tag == "Enemy")
                        return true;
                }
                if (item.transform.CompareTag("Player"))
                {
                    _player = item.transform.gameObject.GetComponent<Mortal>();
                    if (tag == "Player")
                        return true;
                    
                }
            }
        }

        return false;
    }
}
