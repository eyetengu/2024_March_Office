using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_AirMover : MonoBehaviour
{
    [SerializeField] private Transform _fanBlades;
    [SerializeField] private Transform _rotatingUpper;
    [SerializeField] private float _speed = 1200;
    [SerializeField] private float _speedMultiplier = 1;
    private float _step;
    [SerializeField] private bool _isPoweredOn;
    [SerializeField] private bool _isRotating;
    [SerializeField] private bool _isRotatingCW = true;
    private bool _canChangeRotation = true;
    [SerializeField] private bool _lo, _mid, _hi;
    [SerializeField] private float _fanRotationDelay = 5;



    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;

        if(_isPoweredOn)
        {
            FanSpeedCheck();
            RotateFanBlades();

            if(_isRotating)
            {
                RotateFanUpper();
                if(_canChangeRotation)
                {
                    _canChangeRotation= false;

                    StartCoroutine(RotatingTimer());
                }
            }
        }
    }

    void FanSpeedCheck()
    {
        if (_lo) _speedMultiplier = 1;
        if (_mid) _speedMultiplier = 2;
        if(_hi) _speedMultiplier = 3;
    }

    void RotateFanBlades()
    {
        _fanBlades.Rotate(0, 0, _step);
    }

    void RotateFanUpper()
    {
        if(_isRotatingCW)
        {
            _rotatingUpper.Rotate(0, .01f * (_step/2), 0);
        }
        else
        { 
            _rotatingUpper.Rotate(0, -.01f * (_step/2), 0);
        }
    }

    IEnumerator RotatingTimer()
    {
        yield return new WaitForSeconds(_fanRotationDelay);
        _isRotatingCW = !_isRotatingCW;
        _canChangeRotation = true;
    }
}
