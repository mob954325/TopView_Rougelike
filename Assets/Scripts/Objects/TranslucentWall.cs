using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 반투명한 벽을 만들기 위한 클래스 (RoomObject의 아랫벽 반투명하게 하기 위한 클래스)
/// </summary>
public class TranslucentWall : MonoBehaviour
{
    // 무조건 3번째 벽은 문이 있는 벽
    Material[] wallMaterials;

    const float activeAlphaValue = 0.2f;
    const float deactiveAlphaValue = 1.0f;

    void Awake()
    {
        wallMaterials = new Material[transform.childCount + 1];
        Transform child = transform.GetChild(0);    

        for(int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).GetChild(0);
            child.GetComponent<MeshRenderer>().material = wallMaterials[i];
            if(i == 2) // 문이 있는 벽
            {
                continue;
                child = transform.GetChild(i).GetChild(1);
                child.GetComponent<MeshRenderer>().material = wallMaterials[i];
            }
        }
    }

    public void ActiveTranslucent()
    {
        foreach(var item in wallMaterials)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, activeAlphaValue); // alpha값 내리기
        }
    }

    public void DeactiveTranslucent()
    {
        foreach (var item in wallMaterials)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, deactiveAlphaValue); // alpha값 되돌리기
        }
    }
}