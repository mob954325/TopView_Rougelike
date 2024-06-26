using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UpgradeUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    /// <summary>
    /// 슬롯 배열 ( 3개 )
    /// </summary>
    UpgadeSlotUI[] slots;

    /// <summary>
    /// 슬롯 데이터 배열
    /// </summary>
    SlotDatas[] datas;

    /// <summary>
    /// 최대 리스트 수용량
    /// </summary>
    const int maxListCapacity = 6;

    private void Awake()
    {
        // 슬롯 초기화
        slots = GetComponentsInChildren<UpgadeSlotUI>();
        foreach (UpgadeSlotUI slot in slots) 
        {
            slot.onUpGrade += ClosePanel;
        }

        // 데이터 초기화
        int dataCount = Enum.GetValues(typeof(UpgradeSlotType)).Length;
        datas = new SlotDatas[dataCount];

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // 데이터 가져오기
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i] = DataManager.Instance.slotDatas[i];
        }
    }

    /// <summary>
    /// 패널을 활성화 시키는 함수
    /// </summary>
    public void OpenPanel()
    {
        // 패널 활성화
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GetRandomSlot();
    }

    /// <summary>
    /// 패널 비활성화 함수
    /// </summary>
    public void ClosePanel()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 랜덤으로 슬롯 내용 설정하는 함수
    /// </summary>
    public void GetRandomSlot()
    {
        // 슬롯 내용 랜덤 출현
        // 체력증가, 공격력증가, 방어력 증가, 스피드 증가, 능력 업그래이드 2가지
        Util<SlotDatas> util = new Util<SlotDatas>();

        SlotDatas[] tempDatas = new SlotDatas[datas.Length];
        tempDatas = util.Shuffle(datas);
        
        // 업그레이드가 풀업이면?

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlot(tempDatas[i]);
        }
    }
}