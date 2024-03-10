using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoosballTable : MonoBehaviour
{
    [SerializeField] private List<Transform> _teamALineup;
    [SerializeField] private List<Transform> _teamBLineup;

    private float _step;
    [SerializeField] private float _speed = 3;
    private float _speedMultiplier = 100;
    private float _moveSpeedMultiplier = 1;
    private float _moveStep;
    private bool _isright;
    private bool _readyToMoveRods = true;

    
    void Start()
    {
        
    }


    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;
        _moveStep = 1f * _moveSpeedMultiplier* Time.deltaTime;

        RotateLineup();
        MoveRodsLaterally();
    }

    void RotateLineup()
    {
        foreach(var t in _teamALineup)
        {

            t.Rotate(-_step, 0, 0);
        }

        foreach(var t in _teamBLineup)
        {
            t.Rotate(_step, 0, 0);
        }
    }

    void MoveRodsLaterally()
    {
        foreach(var t in _teamALineup)
        {
            t.Translate(new Vector3(_moveStep * Time.deltaTime, 0, 0));
        }
        foreach(var t in _teamBLineup)
        {
            t.Translate(new Vector3(-_moveStep * Time.deltaTime, 0, 0));
        }

        if (_readyToMoveRods)
        {
            _readyToMoveRods = false;
            StartCoroutine(MoveTimer());
        }
    }

    IEnumerator MoveTimer()
    {
        yield return new WaitForSeconds(1f);
        
        if(_isright == false)
            _moveSpeedMultiplier = -1;
    
        else if(_isright)
            _moveSpeedMultiplier = 1;

        _isright = !_isright;
        _readyToMoveRods = true;
    }
}
