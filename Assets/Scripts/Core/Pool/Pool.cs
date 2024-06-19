using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Object Pooling 클래스
/// </summary>
/// <typeparam name="T">재사용할 오브젝트의 최상위 클래스</typeparam>
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
    public int totalCount;

    /// <summary>
    /// 풀에 있는 모든 오브젝트 배열
    /// </summary>
    T[] pool;

    /// <summary>
    /// 활성화 준비된 오브젝트 큐
    /// </summary>
    Queue<T> readyQueue;

    /// <summary>
    /// 오브젝트 풀을 초기화 하는 함수
    /// </summary>
    public void Initialize()
    {
        // 큐 초기화
        pool = new T[totalCount];
        readyQueue = new Queue<T>(pool.Length);
        readyQueue.Clear();

        // 오브젝트 생성
        int index = 0;
        while (index < totalCount)
        {
            T comp = Instantiate(prefab, gameObject.transform).GetComponent<T>();
            comp.name = $"{prefab.name}_{index}";

            pool[index] = comp;  

            readyQueue.Enqueue(comp);                             // 큐 삽입
            comp.onDisable = () => { readyQueue.Enqueue(comp); };     // 비활성화 될 때 다시 큐에 삽입
            comp.gameObject.SetActive(false);                    // 각 오브젝트 비활성화

            index++;
        }
    }

    /// <summary>
    /// 풀에서 오브젝트를 하나 꺼내는 함수
    /// </summary>
    /// <param name="position">위치값</param>
    /// <param name="rotation">회전값</param>
    public T GetObject(Vector3 position, Quaternion rotation)
    {
        T result = null;

        if(readyQueue.TryPeek(out T comp))
        {
            comp = readyQueue.Peek();    // 큐에서 오브젝트 가져오기
            readyQueue.Dequeue();       // 레디큐에서 제거
            comp.gameObject.SetActive(true);        // 오브젝트 활성화
            comp.transform.position = position;
            comp.transform.rotation = rotation;

            result = comp;
        }
        else
        {
            ExtendPool();   // 확장

            comp = readyQueue.Peek();    // 큐에서 오브젝트 가져오기
            readyQueue.Dequeue();       // 레디큐에서 제거
            comp.gameObject.SetActive(true);        // 오브젝트 활성화

            result = comp;
        }

        return result;
    }

    /// <summary>
    /// 풀을 확장하는 함수 ( 현재 개수만큼 추가 )
    /// </summary>
    void ExtendPool()
    {
        int prevCount = totalCount; // 이전 개수 (로그용)
        T[] prevComps = pool;

        // 크기 확장
        totalCount += totalCount;
        pool = new T[totalCount];

        // 이전 배열 추가
        for(int i = 0; i < prevComps.Length; i++)
        {
            pool[i] = prevComps[i]; 
        }

        // 이후 오브젝트 추가
        int index = prevComps.Length;   

        while(index < totalCount)
        {
            if (pool[index] == null)   // 오브젝트가 존재하지않으면 추가
            {
                // 추가 생성
                T comp = Instantiate(prefab, gameObject.transform).GetComponent<T>();
                comp.name = $"{prefab.name}_{index}";
                pool[index] = comp;
                readyQueue.Enqueue(comp);   // 큐 삽입 ( 이미 나머지 오브젝트들을 활성화되어있기 때문에 추가로 생성한 것만 추가 )
            }
            index++;
        }

        Debug.LogWarning($"{this.gameObject.name} 크기 증가 [{prevCount} -> {totalCount}]");
    }
}