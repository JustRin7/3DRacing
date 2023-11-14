using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    //Debug
    [SerializeField] private float engineTorque;
    //Debug
    [SerializeField] private float engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;


    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;

    //Debug
    [SerializeField] private int selectedGearIndex;

    //Debug
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    [SerializeField] private int maxSpeed;

    public float LinearVelocity => chassis.LinerVelocity;
    public float NormalizeLinearVelocity => chassis.LinerVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;


    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;


    private CarChassis chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody : chassis.Rigidbody;


    //Debug
    [SerializeField] private float linearVelocity;
    public float ThrottleControl;//газ
    public float SteerControl;//поворот
    public float BrakeControl;//
    //public float HandBrakeControl;


    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }


    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();

        AutoGearShift();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;        
    }


    //Gearbox
    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";
        if (selectedGear == 0) return "N";
        return (selectedGearIndex + 1).ToString();
    }

    public void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
            UpGear();
            
        if (engineRpm < downShiftEngineRpm)
            DownGear();
            
    }

    public void UpGear()
    {
        ShifhGear(selectedGearIndex + 1);
        
    }

    public void DownGear()
    {
        ShifhGear(selectedGearIndex - 1);
        
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShifhGear(0);
    }

    public void ShiftToNetral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }


    private void ShifhGear(int gerIndex)
    {
        gerIndex = Mathf.Clamp(gerIndex, 0, gears.Length - 1);
        selectedGear = gears[gerIndex];
        selectedGearIndex = gerIndex;
        GearChanged?.Invoke(GetSelectedGearName());
    }


    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }


    public void Reset()
    {
        chassis.Reset();

        chassis.MotorTorque = 0;
        chassis.BrakeTorque = 0;
        chassis.SteerAngle = 0;

        ThrottleControl = 0;
        BrakeControl = 0;
        SteerControl = 0;
    }


    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;
        transform.rotation = rotation;
    }

}
