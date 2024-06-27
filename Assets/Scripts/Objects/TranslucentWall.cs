using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 반투명한 벽을 만들기 위한 클래스 (RoomObject의 아랫벽 반투명하게 하기 위한 클래스)
/// </summary>
public class TranslucentWall : MonoBehaviour
{
    // 무조건 3번째 벽은 문이 있는 벽
    public Material[] wallMaterials;

    const float activeAlphaValue = 0.2f;
    const float deactiveAlphaValue = 1.0f;

    public void Initialize()
    {
        wallMaterials = new Material[transform.childCount + 1];
        Transform child;

        for (int i = 0; i < wallMaterials.Length - 1; i++)
        { 
            if (i == 2) // 문이 있는 벽
            {
                child = transform.GetChild(i).GetChild(0).GetChild(1);
                wallMaterials[i] = child.GetComponent<MeshRenderer>().material;
            }
            else
            {
                child = transform.GetChild(i).GetChild(0);
                wallMaterials[i] = child.GetComponent<MeshRenderer>().material;
            }
        }

        // 문 쪽 머터리얼 따로 찾기 
        child = transform.GetChild(2).GetChild(1);
        wallMaterials[transform.childCount] = child.GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// 벽을 반투명하게 만드는 함수
    /// </summary>
    public void ActiveTranslucent()
    {
        foreach(var item in wallMaterials)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, activeAlphaValue); // alpha값 내리기
        }
    }

    /// <summary>
    /// 벽을 불투명하게 만드는 함수
    /// </summary>
    public void DeactiveTranslucent()
    {
        foreach (var item in wallMaterials)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, deactiveAlphaValue); // alpha값 되돌리기
        }
    }
}