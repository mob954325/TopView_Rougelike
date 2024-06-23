using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpGradeSlotUI : MonoBehaviour
{
    public Action onUpGrade;

    TextMeshProUGUI slotName;
    TextMeshProUGUI desc;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        desc = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        slotName = child.GetComponent<TextMeshProUGUI>();
    }
    
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
    }
}