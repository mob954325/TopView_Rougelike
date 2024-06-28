using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillBoardText : PoolObject
{
    public AnimationCurve animationCurve;
    TextMeshPro text;

    /// <summary>
    /// 설정된 폰트 사이즈
    /// </summary>
    private float originalFontsize;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.localPosition);
        transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y + 180f, 0);
    }

    /// <summary>
    /// 텍스트를 설정하는 함수
    /// </summary>
    /// <param name="position">텍스트를 소환할 위치</param>
    /// <param name="color">텍스트 색</param>
    /// <param name="str">텍스트 문자</param>
    /// <param name="fontSize">텍스트 스폰 크기</param>
    public void SetText(Vector3 position, Color color, string str, float fontSize = 6f)
    {
        transform.localPosition = position;
        originalFontsize = fontSize;
        text.fontSize = originalFontsize;
        text.color = color;
        text.text = str;

        // fade 아웃 효과 실행
        StartCoroutine(FadeOutCoroutine());
    }

    /// <summary>
    /// 페이드 아웃 코루틴
    /// </summary>
    private IEnumerator FadeOutCoroutine()
    {
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime;
            text.fontSize = animationCurve.Evaluate(timeElapsed) * originalFontsize;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}