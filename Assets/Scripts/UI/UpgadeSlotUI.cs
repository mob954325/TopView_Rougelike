using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgadeSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action onUpGrade;

    TextMeshProUGUI slotName;
    TextMeshProUGUI desc;
    Image image;

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
    public void SetSlot(string name, string desc)
    {
        // 내용 설정
    }

    /// <summary>
    /// 플레이어 업그래이드 함수
    /// </summary>
    public void Upgrade()
    {
        onUpGrade?.Invoke();
        Debug.Log($"클릭한 오브젝트 이름 : {gameObject.name}");
    }

    // 이벤트 ==============================================================

    public void OnPointerClick(PointerEventData eventData)
    {
        Upgrade();
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