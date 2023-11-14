using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Text text;
    private Timer countDownTimer;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    private void Start()
    {
        raceStateTracker.PeparationStarted += OnPeparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }


    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnPeparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }


    private void OnPeparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }


    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }


    private void Update()
    {
        text.text = raceStateTracker.CoutDownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO!";
    }
    

}
