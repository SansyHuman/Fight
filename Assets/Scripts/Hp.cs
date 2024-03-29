﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    private Image content;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Text hpText;

    private float currentFill;
    private float MyMaxValue { get; set; }
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue) currentValue = MyMaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;
            currentFill = currentValue / MyMaxValue;
            hpText.text = currentValue + " / " + MyMaxValue;
        }
    }
    private float currentValue;

    void Start()
    {
        content = GetComponent<Image>();
    }
    
    void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
    public void Init(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
