using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IGetable
{
    [SerializeField] ItemData itemData;

    /// <summary>
    /// 아이템 데이터의 아이템 오브젝트
    /// </summary>
    GameObject prop;

    /// <summary>
    /// 아이템 회전속도
    /// </summary>
    public float spinSpeed = 360f;

    void Awake()
    {
        prop = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        prop.transform.Rotate(new (0f, spinSpeed * Time.fixedDeltaTime, 0f), Space.World);
    }
    
    /// <summary>
    /// 아이템 데이터 초기화 함수
    /// </summary>
    public void Initialize(ItemData data)
    {
        itemData = data;
    }

    public void OnGet(GameObject owner)
    {
        this.gameObject.SetActive(false);
        Debug.Log($"{itemData.itemName} 획득");
        // 타입에 따라 아이템 획득량 증가

        Player player = owner.GetComponent<Player>();
        if( player != null )
        {
            switch(itemData.code)
            {
                case ItemCodes.Key:
                    player.GetKey(itemData.count);
                    break;
                case ItemCodes.Gold:
                    player.GetGold(itemData.count);
                    break;
                case ItemCodes.Bomb:
                    player.GetBomb(itemData.count);
                    break;
            }
        }
    }
}