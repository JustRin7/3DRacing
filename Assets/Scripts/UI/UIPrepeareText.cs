using System;
using UnityEngine;
using UnityEngine.UI;
public class UIPrepeareText : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    [SerializeField] private Text text;

    private void Awake()
    {
        raceStateTracker.PeparationToInput += OnPeparationInput;
        raceStateTracker.PeparationStarted += OnPeparationStarted;

        text.enabled = false;
    }


    private void OnDestroy()
    {
        raceStateTracker.PeparationToInput -= OnPeparationInput;
        raceStateTracker.PeparationStarted -= OnPeparationStarted;
    }

    private void OnPeparationStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnPeparationInput()
    {
        text.enabled = true;
        enabled = true;
    }
}
