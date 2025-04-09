using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyScript : MonoBehaviour
{
    public UIScript UI;
    private float cashDollar;

    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();

    }

    void Update()
    {
        cashDollar = UI.kills * 10;
    }
}
