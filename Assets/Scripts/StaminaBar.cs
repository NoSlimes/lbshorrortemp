using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider StaminaSlider;
    public void SetMaxStamina(float stamina)
    {
        StaminaSlider.maxValue = stamina;
        StaminaSlider.value = stamina;
    }
    public void SetStamina(float stamina)
    {
        StaminaSlider.value = stamina;
    }
}
