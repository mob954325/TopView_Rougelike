using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UpgradeUI : MonoBehaviour
{
    UpgadeSlotUI[] slots;

    CanvasGroup canvasGroup;

    public List<string> name;

    [Multiline]
    public List<string> desc;

    /// <summary>
    /// 최대 리스트 수용량
    /// </summary>
    const int maxListCapacity = 6;

    private void Awake()
    {
        slots = GetComponentsInChildren<UpgadeSlotUI>();
        foreach (UpgadeSlotUI slot in slots) 
        {
            slot.onUpGrade += ClosePanel;
        }

        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 패널을 활성화 시키는 함수
    /// </summary>
    public void OpenPanel()
    {
        // 패널 활성화
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = false;
        GetRandomSlot();
    }

    /// <summary>
    /// 패널 비활성화 함수
    /// </summary>
    public void ClosePanel()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 랜덤으로 슬롯 내용 설정하는 함수
    /// </summary>
    public void GetRandomSlot()
    {
        // 슬롯 내용 랜덤 출현
        // 체력증가, 공격력증가, 방어력 증가, 스피드 증가, 능력 업그래이드 2가지

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlot($"Test : {i}",$" GAMEOBJECTNAME : {slots[i].gameObject.name} ");
        }
    }
}