using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    public Slider BatterySlider;
    public void SetMaxBattery(float battery)
    {
        BatterySlider.maxValue = battery;
        BatterySlider.value = battery;
    }
    public void SetBattery(float battery)
    {
        BatterySlider.value = battery;
    }
}
