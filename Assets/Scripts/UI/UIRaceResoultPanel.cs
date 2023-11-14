using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResoultPanel : MonoBehaviour, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject resoultPanel;
    [SerializeField] private Text recordTime;
    [SerializeField] private Text currentTime;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;


    private void Start()
    {
        resoultPanel.SetActive(false);
        raceResultTime.ResultUpdated += OnUpdateResoults;        
    }    


    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnUpdateResoults;
    }


    private void OnUpdateResoults()
    {
        resoultPanel.SetActive(true);

        recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRecord());
        currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
    }


}
