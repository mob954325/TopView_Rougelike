using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbilityObject_Targeting : AbilityObjectBase
{
    Rigidbody rigid;

    Vector3 dir = Vector3.zero;
    bool isTracing = false;

    const float delayTime = 0.5f;
    const float speed = 5f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public override void Initialize(float speed, float damage)
    {
        root = transform.root;

        rotateSpeed = speed;
        this.damage = damage;
    }

    public override void Attack(Transform target)
    {
        if (!gameObject.activeSelf)
            return;

        StopAllCoroutines();
        StartCoroutine(AttackToTarget(target));
    }

    /// <summary>
    /// target에게 공격을 시작하는 함수
    /// </summary>
    public IEnumerator AttackToTarget(Transform target)
    {
        float timeElapsed = 0.0f;

        // 위로 0.25초 올라가고
        while (timeElapsed < delayTime)
        {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        isTracing = true;

        // 밑으로 내리 꽂기
        dir = target.transform.localPosition - transform.localPosition; // 날라갈 방향
        transform.LookAt(dir);

        yield return new WaitForSeconds(5f);    // 5초후 아무도 안맞으면 비활성화
        gameObject.SetActive(false);    // 비활성화

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == root)
            return;

        IBattler battler = other.gameObject.GetComponent<IBattler>();

        if (battler != null)
        {
            battler.Hit(damage);            // 공격
            gameObject.SetActive(false);    // 비활성화
        }
    }

    private void OnDisable()
    {
        isTracing = false;
        dir = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if(isTracing)
        {
            transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0); // 회전값 y만 적용
            transform.localPosition += Time.fixedDeltaTime * transform.forward * speed;
        }
    }
}