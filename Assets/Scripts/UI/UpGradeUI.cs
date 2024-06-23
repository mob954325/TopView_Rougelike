using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeUI : MonoBehaviour
{
    UpGradeSlotUI slot1;
    UpGradeSlotUI slot2;
    UpGradeSlotUI slot3;

    private void Awake()
    {
        slot1 = GetComponentInChildren<UpGradeSlotUI>();
        slot2 = GetComponentInChildren<UpGradeSlotUI>();
        slot3 = GetComponentInChildren<UpGradeSlotUI>();
    }

    /// <summary>
    /// 패널을 활성화 시키는 함수
    /// </summary>
    public void ShowPanel()
    {
        // 패널 활성화
    }

    /// <summary>
    /// 패널 비활성화 함수
    /// </summary>
    public void ClosePanel()
    {

    }

    /// <summary>
    /// 랜덤으로 슬롯 내용 설정하는 함수
    /// </summary>
    public void GetRandomSlot()
    {
        // 슬롯 내용 랜덤 출현
        // 체력증가, 공격력증가, 방어력 증가, 스피드 증가, 능력 업그래이드 2가지
    }
}