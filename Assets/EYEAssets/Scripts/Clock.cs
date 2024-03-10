using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform _hourHand;
    [SerializeField] private Transform _minuteHand;

    float _hourSpeed = .43f;
    float _minuteSpeed = 5.0f;
    private float _hourEyeStep;
    private float _minuteEyeStep;
    float _speedMultiplier = 3;

    void Update()
    {
        _hourEyeStep = _hourSpeed * _speedMultiplier * Time.deltaTime;
        _minuteEyeStep = _minuteSpeed * _speedMultiplier * Time.deltaTime;

        _hourHand.Rotate(0, 0, _hourEyeStep);
        _minuteHand.Rotate(0, 0, _minuteEyeStep);
    }
}
