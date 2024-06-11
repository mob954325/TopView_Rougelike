using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SingletonState : byte
{
    UnInitialized = 0,          // 초기화 안됨
    Initializing,               // 초기화 진행중
    Initialized                 // 초기화 완료
}

/// <summary>
/// 싱글톤 구현 클래스 ( DontDestoryObject )
/// </summary>
/// <typeparam name="T">싱글톤으로 사용할 클래스</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    #region Fields

    /// <summary>
    /// 싱글톤 이름 (로그용)
    /// </summary>
    protected string singletonName;

    /// <summary>
    /// 해당 싱글톤 상태
    /// </summary>
    SingletonState singletonState = SingletonState.UnInitialized;

    /// <summary>
    /// 해당 싱글톤 객체 ( 인스턴스 )
    /// </summary>
    private static T instance;

    /// <summary>
    /// 싱글톤이 처음 실행됬는지 확인하는 변수 ( true : 실행됨 , false : 실행안됨 )
    /// </summary>
    private bool isInitialized;
    #endregion

    #region Properties
    /// <summary>
    /// 싱글톤 객체 접근 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            // 객체가 없으면 새 싱글톤을 생성
            if (instance == null)
            {
                // 싱글톤 생성
                GameObject obj = new GameObject();      // 새 오즈벡트 생성
                obj.name = "Sington";

                obj.AddComponent<T>();                  // 싱글톤 생성
                DontDestroyOnLoad(obj);                 // 파괴 불가능 오브젝트 설정                
            }
            return instance;
        }
    }

    #endregion

    #region Unity LifeCycle
    private void Awake()
    {
        singletonName = this.gameObject.name;

        Debug.Log($"{singletonName} : 1. 로딩전 로그");
        if(instance == null)
        {
            // 씬에서 배치된 싱글톤이 없다.
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // 씬에 싱글톤이 존재한다.
            if (instance != this)   // 해당 싱글톤이 자신이 아니면
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log($"{singletonName} : 2. 씬 로딩 시작");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;        
        Debug.Log($"{singletonName} : 6. 씬 로딩 종료");
    }

    /// <summary>
    /// 씬이 로딩할 때 실행되는 함수 
    /// </summary>
    /// <param name="scene">씬 정보</param>
    /// <param name="mode">로딩 씬 모드</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"{singletonName} : 3. 씬 로딩 진행 시작");
        if(!isInitialized)
        {
            PreInitialize();
        }

        if(mode != LoadSceneMode.Additive)
        {
            Initializing();
            Initialized();
        }
    }

    #endregion

    #region Protected Method
    /// <summary>
    /// 싱글톤이 처음 실행될 때 호출되는 함수
    /// </summary>
    protected virtual void PreInitialize()
    {
        Debug.Log($"{singletonName} : 4. 최초 초기화");
        isInitialized = true;
    }

    /// <summary>
    /// Initialized가 호출 되기 전에 실행되는 함수
    /// </summary>
    protected virtual void Initializing()
    {
        singletonState = SingletonState.Initializing;   // 싱글톤 상태 변환
        Debug.Log($"{singletonName} : 4-1. 초기화 진행중");


    }

    /// <summary>
    /// 씬이 로드 됐을 때 호출되는 초기화 함수 ( Additive 아님 )
    /// </summary>
    protected virtual void Initialized()
    {
        singletonState = SingletonState.Initialized;   // 싱글톤 상태 변환
        Debug.Log($"{singletonName} : 5. 초기화 완료");
    }
    #endregion
}
