using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

public class MemoryGame : MonoBehaviour, IActivity
{
    public static MemoryGame Instance;

    public string ActivityName
    {
        get => activityName;
        set => activityName = value;
    }

    public void EndActivity()
    {
        if (!_isRunning)
            return;
        
        _isRunning = false;

        onEnd.Invoke();
    }

    public int GetMoves()
    {
        return _moves;
    }

    public bool IsWon
    {
        get => isWon;
        set => isWon = value;
    }

    public UnityEvent OnEnd
    {
        get => onEnd;
        set => onEnd = value;
    }

    public UnityEvent OnStart
    {
        get => onStart;
        set => onStart = value;
    }

    public void Select(PlayingCard card)
    {
        if (!_selectionTwo)
        {
            card.Flip(PlayingCard.CardSide.Face);
            if (!_selectionOne)
            {
                _selectionOne = card;
            }
            else
            {
                _selectionTwo = card;
                _timer = Time.time;
                _moves++;
            }
        }
    }

    public void StartActivity()
    {
        _isRunning = true;

        var interactables = transform.GetComponentsInChildren<XRSimpleInteractable>();
        foreach (var interactable in interactables)
            interactable.enabled = true;

        onStart.Invoke();
    }

    private static T GetRandomElement<T>(T[] array)
    {
        return array[(int)Mathf.Floor(UnityEngine.Random.value * array.Length)];
    }

    private void CheckMatch()
    {
        if (_selectionOne.Matches(_selectionTwo))
        {
            _selectionOne.Hide();
            _selectionTwo.Hide();
            _matches++;
        }
        else
        {
            _selectionOne.Flip(PlayingCard.CardSide.Back);
            _selectionTwo.Flip(PlayingCard.CardSide.Back);
        }

        _selectionOne = _selectionTwo = null;
    }

    private void CheckWon()
    {
        if (_matches == _deck.Length / 2)
        {
            isWon = true;
            EndActivity();
            // Debug.Log("Game Won: " + activityName);
        }

        if (_timeLimitSeconds <= 0)
        {
            isWon = false;
            EndActivity();
            // Debug.Log("Game Lost: " + activityName);
        }
    }

    private void Deal()
    {
        var n = 0;

        for (var i = 0; i < _deck.Length / 2; ++i)
        {
            var suit = GetRandomElement(Suits);
            var rank = GetRandomElement(Ranks);
            _deck[n++].SetSuitAndRank(suit, rank);
            _deck[n++].SetSuitAndRank(suit, rank);
        }
    }

    private void Shuffle()
    {
        var rnd = new Random();
        for (var i = _deck.Length - 1; i > 0; --i)
        {
            var n = rnd.Next(i + 1);
            (_deck[i], _deck[n]) = (_deck[n], _deck[i]);
        }
    }

    private void Start()
    {
        Instance = this;
        _deck = transform.GetComponentsInChildren<PlayingCard>();
        // Debug.Log("Deck Length: " + _deck.Length);

        Shuffle();
        Deal();
    }

    private void Update()
    {
        // Debug.Log("Moves: " + _moves + ", Matches: " + _matches);
        CheckWon();

        if (_selectionTwo)
            if (Time.time > _timer + 2.0)
                CheckMatch();

        Invoke("DeductTimeLimitSeconds", 1.0f);
    }

    private void DeductTimeLimitSeconds()
    {
        _timeLimitSeconds -= 1;
    }

    private static readonly string[] Suits = { "Club", "Diamond", "Heart", "Spade" };

    private static readonly string[] Ranks =
        { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13" };

    [SerializeField] private string activityName = "Memory Game";
    [SerializeField] private bool isWon;
    [SerializeField] private UnityEvent onEnd;
    [SerializeField] private UnityEvent onStart;
    
    [SerializeField] [Range(1, 30)]
    private int _timeLimitSeconds = 10;

    private PlayingCard[] _deck;
    private int _matches;
    private int _moves;
    private PlayingCard _selectionOne;
    private PlayingCard _selectionTwo;
    private double _timer;

    private bool _isRunning = false;
}