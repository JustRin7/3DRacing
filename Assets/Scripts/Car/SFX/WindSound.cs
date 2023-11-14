using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindSound : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float volumeModifier;

    [SerializeField] private float baseVolume = 0.4f;

    [SerializeField] private AnimationCurve engineTorqueCurve;

    private AudioSource engineAudioSource;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
        engineAudioSource.volume = baseVolume;
    }

    private void Update()
    {
        //engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);

        engineAudioSource.volume = engineTorqueCurve.Evaluate(car.EngineRpm / car.EngineMaxRpm) * volumeModifier;
    }
}
