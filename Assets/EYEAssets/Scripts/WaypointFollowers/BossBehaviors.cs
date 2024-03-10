using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviors : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

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

    bool _isPaused;

    [SerializeField] float _distance;

    //[SerializeField] private GameObject _golfClub;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        //var travelDirection = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _step);
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _step);

        //_agent.Move(travelDirection);
        _distance = Vector3.Distance(transform.position, _currentWaypoint.position);

        if (_distance < .2f)
        {
            transform.rotation = _currentWaypoint.rotation;
            PerformCharacterSpecificAction();

            if (_isChoosingWaypoint == false)
            {
                _isChoosingWaypoint = true;

                if (_isRandom)
                    ChooseRandomWaypoint();
                else
                    ChooseNextWaypoint();

                StartCoroutine(PauseMoveTimer());
                WaypointPauseCondition();
            }
        }
        //else
            //_animator.SetTrigger("Walk");
    }

    void ChooseRandomWaypoint()
    {
        _waypointID = Random.Range(0, _waypoints.Count - 1);
        _currentWaypoint = _waypoints[_waypointID];
        _isChoosingWaypoint = false;
    }

    void ChooseNextWaypoint()
    {
        if (_isFollowingForward)
        {
            _waypointID++;
        }
        else
        {
            _waypointID--;
        }

        if (_waypointID > _waypoints.Count - 1)
        {
            if (_isLooping)
            {
                _waypointID = 0;
            }
            else
            {
                _waypointID = _waypoints.Count - 2;
                _isFollowingForward = false;
            }
        }
        else if (_waypointID < 0)
        {
            if (_isLooping)
            {
                _waypointID = _waypoints.Count - 1;
            }
            else
            {
                _isFollowingForward = true;
                _waypointID = 1;
            }
        }

        _currentWaypoint = _waypoints[_waypointID];
        _isChoosingWaypoint = false;
    }

    void WaypointPauseCondition()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
            _speedMultiplier = 0;
        else
            _speedMultiplier = 1;
    }

    void PerformCharacterSpecificAction()
    {
        var actionName = _currentWaypoint.name;

        switch (actionName)
        {
            case "Golf":
                //_golfClub.SetActive(true);
                _animator.SetTrigger("Putt");
                Debug.Log("Putting");
                break;
            case "Present":
                //_golfClub.SetActive(true);
                _animator.SetTrigger("Presenting");
                Debug.Log("Presenting");
                break;
            case "Lecture":
                //_golfClub.SetActive(false);
                _animator.SetTrigger("Lecturing");
                Debug.Log("Lecturing");
                break;
            case "NoteBoard":
                //_golfClub.SetActive(false);
                _animator.SetTrigger("Noting");
                Debug.Log("NoteBoarding");
                break;
            case "Trophy":
                //_golfClub.SetActive(false);
                _animator.SetTrigger("Admiring");
                Debug.Log("Trophying");
                break;
        }

        _animator.SetTrigger(actionName);
    }

    void FaceCurrentWaypoint()
    {
        Vector3 targetDirection = _currentWaypoint.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator PauseMoveTimer()
    {
        yield return new WaitForSeconds(3);
        WaypointPauseCondition();
    }

}
