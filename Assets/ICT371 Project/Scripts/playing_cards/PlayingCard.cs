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

    private static readonly string MatPath = "Materials/PlayingCards/";
    private Material _back;
    private Material _face;
    private string _rank;
    private bool _selected;
    private string _suit;

    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnActivate);
    }

    private void Awake()
    {
        _back = Resources.Load<Material>(MatPath + "Black_PlayingCards_Back_00");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            if (!_selected)
                MemoryGame.Instance.Select(this);
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
        _face = Resources.Load<Material>(MatPath + "Black_PlayingCards_" + _suit + _rank + "_00");
    }

    public bool Matches(PlayingCard card)
    {
        return _rank == card._rank && _suit == card._suit;
    }

    public void Flip(CardSide side)
    {
        // TODO: See if we can make the mesh and texture face double-sided so that we only need to rotate it.
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
}