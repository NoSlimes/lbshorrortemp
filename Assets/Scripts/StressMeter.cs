using UnityEngine;
using UnityEngine.UI;

public class StressMeter : MonoBehaviour
{
    public Slider stressMeter;
    public void SetMinStress(float stress)
    {
        stressMeter.maxValue = 100;
        stressMeter.value = stress;
    }
    public void SetStress(float stress)
    {
        stressMeter.value = stress;
    }
}
