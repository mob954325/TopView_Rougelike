using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(() => 
        {
            GameManager.Instance.StartGame();
            this.gameObject.SetActive(false);
            Debug.Log("게임 시작");
        });
    }
}