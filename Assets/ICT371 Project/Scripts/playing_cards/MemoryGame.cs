using UnityEngine;
using Random = System.Random;

public class MemoryGame : MonoBehaviour
{
    public static MemoryGame Instance;
    private static readonly string[] Suits = { "Club", "Diamond", "Heart", "Spade" };

    private static readonly string[] Ranks =
        { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13" };

    private PlayingCard[] _deck;
    private PlayingCard _selectionOne;
    private PlayingCard _selectionTwo;
    private double _timer;

    private void Start()
    {
        Instance = this;
        _deck = transform.GetComponentsInChildren<PlayingCard>();

        Shuffle();
        Deal();
    }

    private void Update()
    {
        if (_selectionTwo)
            if (Time.time > _timer + 2.0)
                CheckMatch();
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
            }
        }
    }

    private void CheckMatch()
    {
        if (_selectionOne.Matches(_selectionTwo))
        {
            _selectionOne.Hide();
            _selectionTwo.Hide();
        }
        else
        {
            _selectionOne.Flip(PlayingCard.CardSide.Back);
            _selectionTwo.Flip(PlayingCard.CardSide.Back);
        }

        _selectionOne = _selectionTwo = null;
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

    private static T GetRandomElement<T>(T[] array)
    {
        return array[(int)Mathf.Floor(UnityEngine.Random.value * array.Length)];
    }
}