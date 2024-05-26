using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float sanity;

    PlayerStatus(float initSanity)
    {
        sanity = initSanity;
    }

    public void reduceSanity(float decrease)
    {
        sanity -= decrease;
    }

    public void increaseSanity(float increase)
    {
        sanity += increase;
    }

    public void setSanity(float sanityValue)
    {
        sanity = sanityValue;
    }

    public float getSanity()
    {
        return sanity;
    }
}
