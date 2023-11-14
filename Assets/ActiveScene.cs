using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScene : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    [SerializeField] private RaceInfo raceInfoNextScene;



    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }


    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }


    public void OnRaceCompleted()
    {
        if (raceInfoNextScene != null)
        {
            raceInfoNextScene.isActive = true;
        }
        else return;
        
    }
}
