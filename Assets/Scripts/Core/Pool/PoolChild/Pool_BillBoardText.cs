using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Pool_BillBoardText : Pool<BillBoardText>
{
    public GameObject SetBillBoardText(Vector3 position, Color color, string str, float fontSize)
    {
        BillBoardText text = GetObject(position, Quaternion.identity).GetComponent<BillBoardText>();
        text.SetText(position, color, str, fontSize);

        return text.gameObject;
    }
}
