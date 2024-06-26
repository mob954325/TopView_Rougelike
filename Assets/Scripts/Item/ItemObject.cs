using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : PoolObject
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
    }

    private void OnEnable()
    {
        Initialize(DataManager.Instance.itemDatas[(int)ItemCodes.Dummy]);  // 활성화 되었을 때 더미로 초기화 ( null 방지 )
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerObj = null;
        isNear = false;
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

    /// <summary>
    /// 아이템 데이터 초기화 함수
    /// </summary>
    public void Initialize(ItemData data)
    {
        itemData = data;

        // 아이템 생성
        if (transform.childCount == 0) // 자식 오브젝트가 없다 (아이템이 없다)
        {
            // 아이템 오브젝트 생성
            GameObject itemObj = new GameObject($"{itemData.name}");
            itemObj.transform.parent = this.gameObject.transform;

            MeshFilter meshFilter = itemObj.AddComponent<MeshFilter>();
            meshFilter.mesh = itemData.dropPrefab.GetComponent<MeshFilter>().sharedMesh;

            MeshRenderer meshRenderer = itemObj.AddComponent<MeshRenderer>();
            meshRenderer.material = itemData.dropPrefab.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else // 아이템 오브젝트가 존재하면 mesh만 바꾸기
        {
            Transform child = transform.GetChild(0);

            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            meshFilter.mesh = itemData.dropPrefab.GetComponent<MeshFilter>().sharedMesh;

            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            meshRenderer.material = itemData.dropPrefab.GetComponent<MeshRenderer>().sharedMaterial;
        }

        prop = transform.GetChild(0).gameObject;
        prop.transform.localPosition = Vector3.zero; // 위치 초기화
    }

    public void GetItem(GameObject owner)
    {
        IGetable getableItem = itemData as IGetable;

        if (getableItem != null)
        {
            getableItem.OnGet(owner);
            this.gameObject.SetActive(false);
        }
    }
}