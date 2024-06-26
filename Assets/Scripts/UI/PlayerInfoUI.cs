using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerInfoUI : MonoBehaviour
{
    Player player;

    HealthUI healthUI;

    TextMeshProUGUI HealthText;
    TextMeshProUGUI BombText;
    TextMeshProUGUI KeyText;
    TextMeshProUGUI CoinText;

    CanvasGroup canvasGroup;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        healthUI = child.GetComponent<HealthUI>();
        HealthText = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1).GetChild(1);
        BombText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2).GetChild(1);
        KeyText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3).GetChild(1);
        CoinText = child.GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();

        HideUI();
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameStart += Initialize;
    }

    /// <summary>
    /// UI 초기화 함수
    /// </summary>
    void Initialize()
    {
        player = GameManager.Instance.player;

        player.onChangeBomb += RefreshBomb;
        player.onChangeCoin += RefreshCoin;
        player.onChangeKey += RefreshKey;
        player.onChangeHealth += RefreshHealth;

        BombText.text = $"{player.BombCount}";
        CoinText.text = $"{player.CoinAmount}";
        KeyText.text = $"{player.KeyCount}";

        ShowUI();   
    }

    /// <summary>
    /// 폭탄 개수 UI 업데이트 함수
    /// </summary>
    /// <param name="value">폭탄 개수</param>
    void RefreshBomb(uint value)
    {
        BombText.text = value.ToString();
    }

    /// <summary>
    /// 폭탄 개수 UI 업데이트 함수
    /// </summary>
    /// <param name="value">폭탄 개수</param>
    void RefreshCoin(uint value)
    {
        CoinText.text = value.ToString();
    }

    /// <summary>
    /// 폭탄 개수 UI 업데이트 함수
    /// </summary>
    /// <param name="value">폭탄 개수</param>
    void RefreshKey(uint value)
    {
        KeyText.text = value.ToString();
    }

    void RefreshHealth(float value)
    {
        float maxHealth = player.MaxHealth;
        float curHealth = player.CurrentHealth;

        healthUI.SetSliderValue(curHealth / maxHealth);
        HealthText.text = $"{curHealth:F0} / {maxHealth:F0} ";
    }

    public void ShowUI()
    {
        canvasGroup.alpha = 1f;
    }

    public void HideUI()
    {
        canvasGroup.alpha = 0f;
    }
}