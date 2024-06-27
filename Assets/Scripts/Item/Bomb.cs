using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PoolObject
{
    ParticleSystem effect;  // 도화선 이펙트
    public GameObject explosionEffect;  // 폭발 이펙트
    public float duration = 3f; // 폭발하기 걸리는 시간

    void Awake()
    {
        effect = GetComponentInChildren<ParticleSystem>();
    }
    void OnEnable()
    {
        effect.Play();
        StartCoroutine(Explosion());
    }

    /// <summary>
    /// 폭탄 폭발 코루틴
    /// </summary>
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(duration);
        effect.Stop();

        // 이펙트 오브젝트 설정
        GameObject effectObj = Instantiate(explosionEffect);
        effectObj.transform.position = this.transform.localPosition;
        effectObj.transform.parent = null;
        Destroy(effectObj, 3f);

        // boom
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, 5f, Vector3.up, 0f);

        foreach(RaycastHit item in hits)    // 맞는 대상이 파괴가능한 벽이면
        {
            if(item.collider.GetComponent<IBreakable>() != null)
            {
                IBreakable obj = item.collider.GetComponentInParent<IBreakable>();
                obj.OnBreak(this.transform);
            }
            
            if(item.collider.GetComponent<IBattler>() != null)   // 맞는 대상이 전투가능한 오브젝트면
            {
                IBattler obj = item.collider.GetComponent<IBattler>();

                obj.Hit(5f);
            }
        }

        this.gameObject.SetActive(false);
    }
}