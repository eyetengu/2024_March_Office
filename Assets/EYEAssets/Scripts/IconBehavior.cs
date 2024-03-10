using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconBehavior : MonoBehaviour
{
    [Header("Rotating")]
    [SerializeField] bool _isRotatingYAxis;
    [SerializeField] bool _isRotatingXAxis;
    [SerializeField] bool _isRotatingZAxis;

    [SerializeField] private float _rotationSpeed = 8f;
    private float _rotationSpeedMultiplier = 1f;
    private float _rotationStep;
    private bool _isRotatingCW;
    [SerializeField] private float _reboundDelay = 3f;
    private bool _isReboundTimerActive;

    [Header("Moving Up & Down")]
    [SerializeField] bool _isBouncing;
    [SerializeField] private float _upDownSpeed = 1f;
    private float _upDownSpeedMultiplier = 1f;
    private float _upDownStep;
    [SerializeField] private float _bounceDelay = 3f;
    bool _isMovingUp = true;
    bool _isBounceTimerActive;

    [Header("Spin Icon CW/CCW")]
    [SerializeField] private bool _spinIcon;
    [SerializeField] private bool _spinCWXAxis;
    [SerializeField] private bool _spinCWYAxis;
    [SerializeField] private bool _spinCWZAxis;
    [SerializeField] private float _spinSpeed = 1;
    private float _spinSpeedMultiplier = 1;
    private float _spinStep;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _rotationStep = _rotationSpeed * _rotationSpeedMultiplier * Time.deltaTime;
        _upDownStep = _upDownSpeed * _upDownSpeedMultiplier * Time.deltaTime;
        _spinStep = _spinSpeed * _spinSpeedMultiplier * Time.deltaTime;


        //EnableGuttenbergPress();

        
        if (_isBouncing)
            BounceUpAndDown();

        if (_spinIcon)
        {
            if (_spinCWXAxis)
                SpinIcon_XAxis(-1f);
            //else 
                //SpinIcon_XAxis(-1f);

            if (_spinCWYAxis)
                SpinIcon_YAxis(1f);
            //else
                //SpinIcon_YAxis(-1f);

            if (_spinCWZAxis)
                SpinIcon_ZAxis(1f);
            //else
                //SpinIcon_ZAxis(-1f);
        }
        
        if (_isRotatingXAxis)
            RotateBackAndForth_XAxis();
        if (_isRotatingYAxis)
            RotateBackAndForth_YAxis();
        if (_isRotatingZAxis)
            RotateBackAndForth_ZAxis();

        
    }

    void EnableGuttenbergPress()
    {
        _isMovingUp = true;        

        _upDownSpeedMultiplier = 1f;
        _upDownSpeed = .3f;
        _spinSpeed = 30;

        transform.Translate(new Vector3(0, _upDownStep, 0));
        SpinIcon_XAxis(-1);
    }

    //Spin Methods
    void RotateBackAndForth_XAxis()
    {
        if (_isReboundTimerActive == false)
        {
            _isReboundTimerActive = true;
            StartCoroutine(ReboundTimer());
        }

        if(_isRotatingCW)
        {
            SpinIcon_XAxis(1);
        }
        else if(_isRotatingCW == false)
        {
            SpinIcon_XAxis(-1);
        }        
    }

    void RotateBackAndForth_YAxis()
    {
        if (_isReboundTimerActive == false)
        {
            _isReboundTimerActive = true;
            StartCoroutine(ReboundTimer());
        }

        if (_isRotatingCW)
        {
            SpinIcon_YAxis(1);
        }
        else if (_isRotatingCW == false)
        {
            SpinIcon_YAxis(-1);
        }
    }

    void RotateBackAndForth_ZAxis()
    {
        if (_isReboundTimerActive == false)
        {
            _isReboundTimerActive = true;
            StartCoroutine(ReboundTimer());
        }

        if (_isRotatingCW)
        {
            SpinIcon_ZAxis(1);
        }
        else if (_isRotatingCW == false)
        {
            SpinIcon_ZAxis(-1);
        }
    }

    //Icon Spin Methods
    void SpinIcon_XAxis(float multiplier)
    {
        transform.Rotate(_spinStep * multiplier,0, 0);
    }

    void SpinIcon_YAxis(float multiplier)
    {
        transform.Rotate(0, _spinStep * multiplier, 0);
    }

    void SpinIcon_ZAxis(float multiplier)
    {
        transform.Rotate(0, 0,_spinStep * multiplier);
    }

    //MoveUpDownMethods
    void BounceUpAndDown()
    {
        if (_isBounceTimerActive == false)
        {
            _isBounceTimerActive = true;
            StartCoroutine(BounceTimer());
        }

        if(_isMovingUp)        
            _upDownSpeedMultiplier = 1f;        
        else        
            _upDownSpeedMultiplier = -1f;       
        
        transform.Translate(new Vector3(0, _upDownStep, 0));
   
    }

    //PlayerPickupMethods
    void PlayerPickup()
    {
        _audioSource.PlayOneShot(_audioClip);
        Destroy(gameObject, 1f);
    }

    //TIMERS
    IEnumerator ReboundTimer()
    {
        yield return new WaitForSeconds(_reboundDelay);
        _isRotatingCW = !_isRotatingCW;
        _isReboundTimerActive = false;
    }

    IEnumerator BounceTimer()
    {
        yield return new WaitForSeconds(_bounceDelay);
        _isMovingUp = !_isMovingUp;
        _isBounceTimerActive = false;
    }
}
