using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Transform _swivelHead;

    [Header("Speed Values")]
    [SerializeField] private float _speed = 1200;
    [SerializeField] private float _speedMultiplier = 1;
    private float _step;

    [Header("Rotation Values")]
    [SerializeField] private bool _isRotatingCW = true;
    private bool _canChangeRotation = true;
    [SerializeField] private float _fanRotationDelay = 5;


    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;

        RotateSwivelHead();

        if (_canChangeRotation)
        {
            _canChangeRotation = false;

            StartCoroutine(RotatingTimer());
        }            
    }

    void RotateSwivelHead()
    {
        if (_isRotatingCW)
        {
            _swivelHead.Rotate(0, .01f * (_step / 2), 0);
        }
        else
        {
            _swivelHead.Rotate(0, -.01f * (_step / 2), 0);
        }
    }

    IEnumerator RotatingTimer()
    {
        yield return new WaitForSeconds(_fanRotationDelay);
        _isRotatingCW = !_isRotatingCW;
        _canChangeRotation = true;
    }
}
