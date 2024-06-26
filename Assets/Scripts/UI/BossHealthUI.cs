using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : HealthUI
{
    TextMeshProUGUI boosName;
    CanvasGroup canvasGroup;

    private float maxValue;
    private float maxTime = 1f;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
        boosName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(Enemy_Boss_Warrior boss)
    {
        boosName.text = $"{boss.gameObject.name}";
        maxValue = boss.MaxHealth;
        ShowUI();
        StartCoroutine(ZeroToMaxValue());
        boss.onChangeHealth += RefreshUI;
    }

    public void ShowUI()
    {
        canvasGroup.alpha = 1f;
    }

    public void HideUI()
    {
        canvasGroup.alpha = 0f;
    }

    /// <summary>
    /// 보스 체력 UI를 리프레쉬하는 함수
    /// </summary>
    /// <param name="curHealth">현재 체력 값</param>
    private void RefreshUI(float curHealth)
    {
        SetSliderValue(curHealth / maxValue);
    }

    private IEnumerator ZeroToMaxValue()
    {
        float timeElpased = 0f;
        while (timeElpased < maxTime)
        {
            timeElpased += Time.deltaTime;
            SetSliderValue(timeElpased / maxTime);
            yield return null;
        }
    }
}