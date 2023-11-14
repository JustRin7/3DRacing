using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GearboxSound : MonoBehaviour
{
    [SerializeField] private Car car;
    private AudioSource engineAudioSource;

    void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
        car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        if(car.EngineRpm > 800 & car.LinearVelocity > 10 || car.LinearVelocity < -10 || car.LinearVelocity == 1f)
        engineAudioSource.Play();
    }
}
