using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    /// <summary>
    /// 슬라이더 값을 설정하는 함수
    /// </summary>
    /// <param name="value">설정할 값</param>
    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}