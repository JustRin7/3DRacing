using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneDependenciesContainer : Depandency
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;

    protected override void BindAll(MonoBehaviour monoBehaviorusInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monoBehaviorusInScene);
        Bind<CarInputControl>(carInputControl, monoBehaviorusInScene);
        Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviorusInScene);
        Bind<Car>(car, monoBehaviorusInScene);
        Bind<CarCameraController>(carCameraController, monoBehaviorusInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviorusInScene);
        Bind<RaceResultTime>(raceResultTime, monoBehaviorusInScene);


        //IDependency<TrackpointCircuit> t = mono as IDependency<TrackpointCircuit>;
        //if (t != null) t.Construct(trackpointCircuit);

        //или (mono as IDependency<TrackpointCircuit>)?.Construct(trackpointCircuit);

        //или if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStateTracker);
    }


    private void Awake()
    {
        FindAllObjectToBind();
    }


}
