using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PlayingCard : MonoBehaviour
{
    public enum CardSide
    {
        Back,
        Face
    }

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

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool Matches(PlayingCard card)
    {
        return _rank == card._rank && _suit == card._suit;
    }

    public void OnActivate(ActivateEventArgs args)
    {
        if (!_selected)
            MemoryGame.Instance.Select(this);
    }

    public void SetSuitAndRank(string suit, string rank)
    {
        _suit = suit;
        _rank = rank;
        _face = Resources.Load<Material>(k_MatPath + "Black_PlayingCards_" + _suit + _rank + "_00");
    }

    private void Awake()
    {
        _back = Resources.Load<Material>(k_MatPath + "Black_PlayingCards_Back_00");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            if (!_selected)
                MemoryGame.Instance.Select(this);
    }

    private void Start()
    {
        _xrGrabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        _xrGrabInteractable.activated.AddListener(OnActivate);
    }

    private const string k_MatPath = "Materials/PlayingCards/";

    private Material _back;
    private Material _face;
    private string _rank;
    private bool _selected;
    private string _suit;
    private XRGrabInteractable _xrGrabInteractable;
}