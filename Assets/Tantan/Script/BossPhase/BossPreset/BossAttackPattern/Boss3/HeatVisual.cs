using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HeatVisual : MonoBehaviour
{
    Volume volume => GetComponent<Volume>();

    [SerializeField] Slider heatSlider;

    // Update is called once per frame
    void Update()
    {
        if (volume.profile.TryGet<Vignette>(out var vignette))
        {
            vignette.intensity.value = Mathf.Lerp(0, .4f, heatSlider.value/heatSlider.maxValue);
        }
    }
}
