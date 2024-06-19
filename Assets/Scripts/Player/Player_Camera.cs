using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 카메라 스크립트( Player 버츄얼카메라 전용 클래스 )
/// </summary>
public class Player_Camera : MonoBehaviour
{
    CinemachineVirtualCamera vc;

    void Awake()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
    }

    /// <summary>
    /// 버츄얼 카메라 초기화 함수 
    /// </summary>
    /// <param name="player">플레이어 스크립트가 있는 오브젝트</param>
    /// <returns>플레이어가 없으면 false( 실패 ), 있으면 true ( 성공 )</returns>
    public bool Initialize(Player player)
    {
        bool result = false;
        if(player != null)
        {
            vc.Follow = player.gameObject.transform;
            vc.LookAt = player.gameObject.transform;

            result = true;
        }

        return result;
    }
}