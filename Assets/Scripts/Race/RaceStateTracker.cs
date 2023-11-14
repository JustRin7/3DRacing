using System;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}


public class RaceStateTracker : MonoBehaviour, IDependency<TrackpointCircuit>
{
    public event UnityAction PeparationToInput;
    public event UnityAction PeparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPasstd;
    public event UnityAction<int> LapCompleted;

    private TrackpointCircuit trackPointCircuit;
    public void Construct(TrackpointCircuit trackpointCircuit) => this.trackPointCircuit = trackpointCircuit;

    [SerializeField] private Timer countdownTime;
    [SerializeField] private int lapsToComplete;

    public Timer CoutDownTimer => countdownTime;

    private RaceState state;
    public RaceState State => state;


    private void StartState(RaceState state)
    {
        this.state = state;
    }


    private void Start()
    {       
        StartState(RaceState.Preparation);

        countdownTime.enabled = false;

        countdownTime.Finished += OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTrigerred += OnTrackPointTrigerred;
        trackPointCircuit.LapCompleted += OnLapCompleted;

        PeparationToInput?.Invoke();
    }


    private void OnDestroy()
    {
        countdownTime.Finished -= OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTrigerred -= OnTrackPointTrigerred;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }


    private void OnTrackPointTrigerred(TrackPoint trackPoint)
    {
        TrackPointPasstd?.Invoke(trackPoint);
    }


    private void OnCountdownTimerFinished()
    {
        StartRace();
    }


    private void OnLapCompleted(int lapAmount)
    {
        if(trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }


    public void LaunchPeparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CountDown);

        countdownTime.enabled = true;

        PeparationStarted?.Invoke();
    }


    private void StartRace()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }


    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }


    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    
}
