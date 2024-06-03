using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

/// <author>
/// Jaiden di Lanzo
/// </author>
/// <summary>
/// This class models the card activity minigame and implements the IActivity interface.
/// </summary>
/// <seealso cref="IActivity"/>
/// <seealso cref="PlayingCard"/>
public class MemoryGame : MonoBehaviour, IActivity
{
    /// <summary>
    /// Instance of the MemoryGame class.
    /// </summary>
    public static MemoryGame Instance;

    /// <summary>
    /// Getter/Setter for the activity name
    /// </summary>
    public string ActivityName
    {
        get => activityName;
        set => activityName = value;
    }

    /// <summary>
    /// Ends the activity.
    /// </summary>
    public void EndActivity()
    {
        if (!_isRunning)
            return;
        
        _isRunning = false;
        CancelInvoke("DeductTimeLimitSeconds");

        onEnd.Invoke();
    }

    /// <summary>
    /// Getter for the amount of moves made.
    /// </summary>
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

    /// <summary>
    /// Selects a card.
    /// </summary>
    /// <param name="card">The card to select.</param>
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

    /// <summary>
    /// Starts the activity.
    /// </summary>
    public void StartActivity()
    {
        _isRunning = true;

        var interactables = transform.GetComponentsInChildren<XRSimpleInteractable>();
        foreach (var interactable in interactables)
            interactable.enabled = true;

        InvokeRepeating("DeductTimeLimitSeconds", 1.0f, 1.0f);

        onStart.Invoke();
    }

    /// <summary>
    /// Gets a random element from the deck
    /// </summary>
    /// <typeparam name="T">The deck</typeparam>
    private static T GetRandomElement<T>(T[] array)
    {
        return array[(int)Mathf.Floor(UnityEngine.Random.value * array.Length)];
    }

    /// <summary>
    /// Checks if the two selected cards match.
    /// </summary>
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

    /// <summary>
    /// Checks if the game has been won or lost.
    /// </summary>
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

    /// <summary>
    /// Deals the cards.
    /// </summary>
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

    /// <summary>
    /// Shuffles the deck.
    /// </summary>
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
    }

    /// <summary>
    /// Deducts the time limit seconds.
    /// </summary>
    private void DeductTimeLimitSeconds()
    {
        _timeLimitSeconds -= 1;
    }

    /// <summary>
    /// The suits of the deck.
    /// </summary>
    private static readonly string[] Suits = { "Club", "Diamond", "Heart", "Spade" };

    /// <summary>
    /// The ranks of the deck.
    /// </summary>
    private static readonly string[] Ranks =
        { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13" };

    /// <summary>
    /// The name of the activity.
    /// </summary>
    [SerializeField] private string activityName = "Memory Game";

    /// <summary>
    /// The won state of the game.
    /// </summary>
    [SerializeField] private bool isWon;

    /// <summary>
    /// The events invoked when the activity ends.
    /// </summary>
    [SerializeField] private UnityEvent onEnd;

    /// <summary>
    /// The events invoked when the activity starts.
    /// </summary>
    [SerializeField] private UnityEvent onStart;
    
    /// <summary>
    /// The time limit in seconds.
    /// </summary>
    [SerializeField] [Range(1, 30)]
    private int _timeLimitSeconds = 10;

    /// <summary>
    /// The deck of cards.
    /// </summary>
    private PlayingCard[] _deck;
    
    /// <summary>
    /// The amount of matches made.
    /// </summary>
    private int _matches;

    /// <summary>
    /// The amount of moves made.
    /// </summary>
    private int _moves;

    /// <summary>
    /// The first selected card.
    /// </summary>
    private PlayingCard _selectionOne;

    /// <summary>
    /// The second selected card.
    /// </summary>
    private PlayingCard _selectionTwo;

    /// <summary>
    /// The timer.
    /// </summary>
    private double _timer;

    /// <summary>
    /// The running state of the game.
    /// </summary>
    private bool _isRunning = false;
}