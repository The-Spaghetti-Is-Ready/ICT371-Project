using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <author>
/// Jaiden di Lanzo
/// </author>
/// <summary>
/// This class models a playing card.
/// </summary>
public class PlayingCard : MonoBehaviour
{
    /// <summary>
    /// Enum for the side of the card.
    /// </summary>
    public enum CardSide
    {
        Back,
        Face
    }

    /// <summary>
    /// Flips the card to the specified side.
    /// </summary>
    /// <param name="side">The side to flip the card to.</param>
    public void Flip(CardSide side)
    {
        _selected = !_selected;
        switch (side)
        {
            case CardSide.Back:
                GetComponent<MeshRenderer>().material = _back;
                break;
            case CardSide.Face:
                GetComponent<MeshRenderer>().material = _face;
                break;
        }
    }

    /// <summary>
    /// Hides the card.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the card matches the specified card.
    /// </summary>
    /// <param name="card">The card to compare.</param>
    /// <returns>True if the cards match, false otherwise.</returns>
    public bool Matches(PlayingCard card)
    {
        return _rank == card._rank && _suit == card._suit;
    }

    /// <summary>
    /// Called when the card is activated.
    /// </summary>
    /// <param name="args">The activation event arguments.</param>
    public void OnActivate(ActivateEventArgs args)
    {
        if (!_selected)
            MemoryGame.Instance.Select(this);
    }

    /// <summary>
    /// Sets the suit and rank of the card.
    /// </summary>
    /// <param name="suit">The suit of the card.</param>
    /// <param name="rank">The rank of the card.</param>
    public void SetSuitAndRank(string suit, string rank)
    {
        _suit = suit;
        _rank = rank;
        _face = Resources.Load<Material>(k_MatPath + "Black_PlayingCards_" + _suit + _rank + "_00");
    }

    private void Awake()
    {
        _back = Resources.Load<Material>(k_MatPath + "Black_PlayingCards_Back_00");
        var interactable = gameObject.GetComponent<XRSimpleInteractable>();
        interactable.activated.AddListener(OnActivate);
        interactable.enabled = false;
    }

    /// <summary>
    /// Called when the mouse is over the object.
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            if (!_selected)
                MemoryGame.Instance.Select(this);
    }

    /// <summary>
    /// The path to the materials.
    /// </summary>
    private const string k_MatPath = "Materials/PlayingCards/";

    /// <summary>
    /// The back material of the card.
    /// </summary>
    private Material _back;

    /// <summary>
    /// The face material of the card.
    /// </summary>
    private Material _face;

    /// <summary>
    /// The rank of the card.
    /// </summary>
    private string _rank;

    /// <summary>
    /// Indicates whether the card is selected.
    /// </summary>
    private bool _selected;

    /// <summary>
    /// The suit of the card.
    /// </summary>
    private string _suit;
}