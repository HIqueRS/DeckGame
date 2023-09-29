using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    [SerializeField]
    public List<ScriptableCard> _deck;
    public List<ScriptableCard> _deckIronclad;
    public List<ScriptableCard> _deckTest;

    [SerializeField]
    private TMPro.TextMeshProUGUI _numberText;

    [SerializeField]
    private GameObject _cardObject;

    

    [SerializeField]
    private Transform[] _handPositions;

    private bool[] _hasCard;

    public bool _isDiscard;

    public Deck OtherStack;

    private int _currentDeck;


    private void OnEnable()
    {
        GameManager.BeginTurn += InitializeHand;
        GameManager.FirstTurn += FirstTurn;
    }
    private void OnDisable()
    {
        GameManager.BeginTurn -= InitializeHand;
        GameManager.FirstTurn -= FirstTurn;
    }

    // Start is called before the first frame update
    void Start()
    {

        _currentDeck = GameManager.Instance._currentDeck;

        _hasCard = new bool[5];



        Shuffle();

       
        

        UpdateNumber();

    }

    public void AddCard(ScriptableCard card)
    {
        _deck.Add(card);
        UpdateNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FirstTurn()
    {
        
        StartCoroutine( WaitFirstTurn());
    }

    private IEnumerator WaitFirstTurn()
    {
        yield return new WaitForSeconds(0.5f);

        _currentDeck = GameManager.Instance._currentDeck;

        switch (_currentDeck)
        {
            case 0: _deck = _deckIronclad;
                break;

            case 1: _deck = _deckTest;
                break;

            default:
                break;
        }

        InitializeHand();
    }

    private void InitializeHand()
    {


        if(!_isDiscard)
        {


            if (_deck.Count >= 5)
            {
                Shuffle();

                DrawFiveCards();
            }
            else
            {

                ResetStack();

                Shuffle();

                DrawFiveCards();

            }


            
        }

        UpdateNumber();
    }

    private void ResetStack()
    {
        int aux;
        aux = OtherStack._deck.Count;

        for (int i = 0; i < aux; i++)
        {
            _deck.Add(OtherStack._deck[0]);

            OtherStack._deck.Remove(OtherStack._deck[0]);
        }

        OtherStack._deck.Clear();

        OtherStack.UpdateNumber();
    }

    private void DrawFiveCards()
    {
        Card auxCard;
        GameObject auxObj;

        for (int i = 0; i < 5; i++)
        {


            auxObj = GameObject.Instantiate(_cardObject, _handPositions[i].position, Quaternion.identity, _handPositions[i]);

            auxCard = auxObj.GetComponent<Card>();

            auxCard.SetCard(_deck[0]);

            auxCard._discard = OtherStack;

            auxObj.GetComponent<BackPosition>().SetPosition(_handPositions[i]);

            _deck.Remove(_deck[0]);


        }
    }

    private void Shuffle()
    {
        ScriptableCard aux;
        int r;

        for(int i =0; i < _deck.Count; i ++)
        {
            r = Random.Range(0, _deck.Count);

            aux = _deck[i];
            _deck[i] = _deck[r];
            _deck[r] = aux;
        }


    }

    private void DrawCard()
    {

    }

    private void DrawNCards(int i)
    {

    }

    public void UpdateNumber()
    {
        _numberText.text = _deck.Count.ToString();
    }
}
