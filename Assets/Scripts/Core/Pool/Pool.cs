using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : PoolObject
{
    // 1. 오브젝트 생성
    // 2. 생성한 오브젝트 비활성화
    // 3. 오브젝트 사용 함수 작성
    // 3.1 queue 자료구조를 이용해 순차적으로 활성화 ( 활성화 준비 오브젝트들 )
    // 3.2 만약 준비된 오브젝트가 없으면 풀 크기 늘리기 ( 최대한 일어나면 안됨 )
    // 4. 해당 오브젝트가 비활성화 되면 준비큐에 다시 삽입

    /// <summary>
    /// 풀 오브젝트 프리팹
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// 생성할 오브젝트 개수
    /// </summary>
    public int totalObjectCount;

    /// <summary>
    /// 활성화 준비된 오브젝트 큐
    /// </summary>
    Queue<GameObject> readyQueue;

    /// <summary>
    /// 오브젝트 풀을 초기화 하는 함수
    /// </summary>
    public void Initialize()
    {
        // 큐 초기화
        readyQueue = new Queue<GameObject>(totalObjectCount);
        readyQueue.Clear();

        // 오브젝트 생성
        int count = 0;
        while (count < totalObjectCount)
        {
            GameObject obj = Instantiate(prefab, gameObject.transform);
            obj.name = $"{prefab.name}_{count}";
            readyQueue.Enqueue(obj);    // 큐 삽입

            count++;
        }
    }

    /// <summary>
    /// 풀에서 오브젝트를 하나 꺼내는 함수
    /// </summary>
    /// <param name="position">위치값</param>
    /// <param name="rotation">회전값</param>
    public GameObject GetObject(Vector3? position = null, Quaternion? rotation = null)
    {
        GameObject result = null;

        if(readyQueue.TryPeek(out GameObject obj))
        {
            obj = readyQueue.Peek();    // 큐에서 오브젝트 가져오기
            readyQueue.Dequeue();       // pop
            obj.SetActive(true);

            result = obj;
        }
        else
        {
            Debug.Log("모든 오브젝트 사용 중");
        }

        return result;
    }

    void ExtendPool()
    {

    }
}