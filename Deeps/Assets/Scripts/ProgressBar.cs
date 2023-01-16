using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.value = slider.minValue;
    }

    public void SetProgress(int progression)
    {
        slider.value = progression;
    }
}
