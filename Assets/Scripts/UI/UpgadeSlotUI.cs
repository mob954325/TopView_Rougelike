using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 업그레이드 가능한 슬롯타입
/// </summary>
public enum UpgradeSlotType
{
    Health = 0,
    Attack,
    Defence,
    Speed,
    Rotate,
    Targeting
}

public class UpgadeSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SlotDatas data;

    TextMeshProUGUI slotName;
    TextMeshProUGUI desc;
    Image image;

    public Action onUpGrade;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        desc = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        slotName = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        image = child.GetComponent<Image>();
    }
    
    // 기능 함수 ==============================================================

    /// <summary>
    /// 슬롯 내용을 설정하는 함수
    /// </summary>
    public void SetSlot(SlotDatas slotData)
    {
        // 내용 설정
        data = slotData;

        slotName.text = data.slotName;
        this.desc.text = data.slotDesc;
    }

    /// <summary>
    /// 플레이어 업그래이드 함수
    /// </summary>
    public void Upgrade(Player player)
    {
        data.Upgrade(player);
        onUpGrade?.Invoke();
    }

    // 이벤트 ==============================================================

    public void OnPointerClick(PointerEventData eventData)
    {
        Upgrade(GameManager.Instance.player);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.red;    // 임시 하이라이트 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.clear;
    }
}