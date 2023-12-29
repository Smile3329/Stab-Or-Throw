using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    public float value;
    private float maxValue;
    private float percentage;

    public void SetMaxValue(float maxValue) {
        this.maxValue = maxValue;
        percentage = maxValue/100;
    }

    public void ChangeValue(float value) {
        this.value = Mathf.Clamp(value, 0, maxValue);
        bar.localScale = new Vector3(this.value/percentage/100, 1, 1);
    }
}
