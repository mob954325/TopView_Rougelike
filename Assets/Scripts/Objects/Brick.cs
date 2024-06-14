using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    Material material;

    /// <summary>
    /// Dissolve 크기 (1에서 0으로 갈 수록 사라짐)
    /// </summary>
    float amount = 1f;

    /// <summary>
    /// 반응하고 남아있는 시간 (이 시간이 지나면 사라짐)
    /// </summary>
    const float duration = 2f;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// 해당 오브젝트 제거 실행 함수
    /// </summary>
    public void ActiveDisable()
    {
        StartCoroutine(DisableBrick());
    }

    /// <summary>
    /// 벽돌 비활성화 코루틴 (2초)
    /// </summary>
    /// <param name="obj">비활성화 할 오브젝트</param>
    IEnumerator DisableBrick()
    {
        float timeElapsed = 0.0f;
        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            amount -= Time.deltaTime / duration;
            material.SetFloat("_Amount", amount);
            yield return null;
        }

        Destroy(gameObject);
    }
}