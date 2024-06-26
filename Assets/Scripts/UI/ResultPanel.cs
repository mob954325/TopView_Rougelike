using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 결과창 패널 클래스
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class ResultPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;

    /// <summary>
    /// 결과 텍스트
    /// </summary>
    TextMeshProUGUI result;

    /// <summary>
    /// 점수 텍스트
    /// </summary>
    TextMeshProUGUI score;

    /// <summary>
    /// 매뉴로 돌아가는 버튼
    /// </summary>
    Button MenuButton;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);
        result = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        score = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        MenuButton = child.GetComponent<Button>();
        MenuButton.onClick.AddListener(BackToMenu);
    }

    void Start()
    {
        GameManager.Instance.onGameEnd += (value, result) =>
        {
            SetResult(value, result);   // 결과값 설정 후
            ShowPanel();                // 패널 공개
        };
    }

    /// <summary>
    /// 패털 공개용 함수
    /// </summary>
    public void ShowPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 패널 숨기는 함수
    /// </summary>
    public void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 결과값을 설정하고 출력하는 함수
    /// </summary>
    /// <param name="value">점수값</param>
    /// <param name="isClear">클리어 여부</param>
    private void SetResult(int value, bool isClear)
    {
        if(isClear) // 클리어
        {
            result.text = $"Clear !!";
            result.color = Color.green;
        }
        else // 클리어 실패
        {
            result.text = $"Defeat ...";
            result.color = Color.red;
        }

        score.text = $"Score : {value}";
    }

    private void BackToMenu()
    {
        // 메뉴로 돌아가기
    }
}