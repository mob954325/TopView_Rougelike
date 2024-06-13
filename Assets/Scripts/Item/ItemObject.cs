using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemData itemData;

    /// <summary>
    /// 아이템 데이터의 아이템 오브젝트
    /// </summary>
    GameObject prop;

    /// <summary>
    /// 플레이어 오브젝트 (주변에 플레이어 없으면 null)
    /// </summary>
    GameObject playerObj;

    Rigidbody rigid;

    /// <summary>
    /// 아이템 회전속도
    /// </summary>
    public float spinSpeed = 360f;
    
    /// <summary>
    /// 아이템 움직이는 속도 (플레이어에게 움직이는 속도)
    /// </summary>
    public float moveSpeed = 3.5f;

    /// <summary>
    /// 플레이어가 가까이 있는지 확인하는 변수 (가까이 있으면 true 아니면 false)
    /// </summary>
    bool isNear = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();  
        prop = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {

        if (isNear && playerObj != null) // 주변에 플레이어가 있다.
        {

            Vector3 dir = (playerObj.transform.position - transform.position).normalized;
            dir.y = 0f;
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * dir * moveSpeed);
        }

        prop.transform.Rotate(new (0f, spinSpeed * Time.fixedDeltaTime, 0f), Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            playerObj = other.gameObject;
            isNear = true;
        }
    }

    void OnDisable()
    {
        playerObj = null;
        isNear = false;
    }

    /// <summary>
    /// 아이템 데이터 초기화 함수
    /// </summary>
    public void Initialize(ItemData data)
    {
        itemData = data;
    }

    public void GetItem(GameObject owner)
    {
        IGetable getableItem = itemData as IGetable;

        if (getableItem != null)
        {
            getableItem.OnGet(owner);
        }
    }
}