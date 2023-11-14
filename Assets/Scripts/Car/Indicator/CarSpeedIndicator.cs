using UnityEngine;
using UnityEngine.UI;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text texts;

    // Update is called once per frame
    void Update()
    {
            texts.text = car.LinearVelocity.ToString("F0");// F0 нужно для округления до нулевого знака полсе запятой
    }
}
