using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorBehaviors : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    [Header("Route Attributes")]
    [SerializeField] bool _isLooping;
    [SerializeField] bool _isRandom;
    [SerializeField] bool _isFollowingForward;

    [Header("Waypoints")]
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] int _waypointID;
    private Transform _currentWaypoint;
    private bool _isChoosingWaypoint;

    [Header("Speed")]
    float _step;
    float _speed = 3;
    float _speedMultiplier = 1;

     [SerializeField] float _distance;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _currentWaypoint = _waypoints[0];
    }

    void Update()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;

        MoveToCurrentWaypoint();
        FaceCurrentWaypoint();
    }

    void MoveToCurrentWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _step);

        _distance = Vector3.Distance(transform.position, _currentWaypoint.position);

        if (_distance < .2f)
        {
            _animator.SetBool("Walk", false);

            if (_isChoosingWaypoint == false)
            {
                _isChoosingWaypoint = true;

                if (_isRandom)
                    ChooseRandomWaypoint();
                else
                    ChooseNextWaypoint();
            }
        }
        else
            _animator.SetBool("Walk", true);
    }

    void ChooseRandomWaypoint()
    {
        _waypointID = Random.Range(0, _waypoints.Count - 1);
        _currentWaypoint = _waypoints[_waypointID];
        _isChoosingWaypoint = false;
    }

    void ChooseNextWaypoint()
    {
        if(_isFollowingForward)
        {
            _waypointID++;
        }
        else
        {
            _waypointID--;
        }
    
        if(_waypointID > _waypoints.Count - 1)
        {
            if(_isLooping)
            {
                _waypointID = 0;
            }
            else
            {
                _waypointID= _waypoints.Count - 2;
                _isFollowingForward= false;
            }
        }
        else if(_waypointID < 0)
        {
            if(_isLooping)
            {
                _waypointID= _waypoints.Count - 1;
            }
            else
            {
                _isFollowingForward= true;
                _waypointID = 1;
            }
        }
        
        _currentWaypoint= _waypoints[_waypointID];
        _isChoosingWaypoint = false;
    }

    void FaceCurrentWaypoint()
    {
        Vector3 targetDirection = _currentWaypoint.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void FaceAwayFromCurrentWaypoint()
    {
        Vector3 targetDirection = transform.position - _currentWaypoint.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

}
