using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CognitiveStage
{
    Early,
    Middle,
    Late,
    Deceased
};

public class PlayerStatus : MonoBehaviour
{
    private const double k_MinDecay = 0.0d, k_MaxDecay = 1.0d;
    private double _decay = 0.0d;
    private double _decayRate = -0.8d;
    private CognitiveStage _stage = CognitiveStage.Early;

     public double GetCognitiveDecay()
    {
        return _decay;
    }

    public CognitiveStage GetCognitiveStage()
    {
        return _stage;
    }

    public void EvaluateDecay(int currentDayIndex)
    {
        // TODO: Add modifiers into decay rate
        // Evaluate the (daily) cognitive decay s.t. f(x) = c exp(kx)
        _decay = k_MaxDecay * Math.Exp(_decayRate * currentDayIndex);
        Debug.Log("Decay: " + _decay + ", Day: " + currentDayIndex);
    }

    public void EvaluateStage()
    {
        if (_decay > 0.3d)
        {
            _stage = CognitiveStage.Middle;
        }

        if (_decay > 0.6d)
        {
            _stage = CognitiveStage.Late;
        }

        if (_decay > k_MaxDecay)
        {
            _stage = CognitiveStage.Deceased;
        }

        Debug.Log("Stage: " + _stage);
    }
}
