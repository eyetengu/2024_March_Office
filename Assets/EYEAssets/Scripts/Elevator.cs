using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _isMovingUp;
    private bool _isMovingDown = true;

    [SerializeField] private Transform _elevator;
    [SerializeField] private Transform[] _upperLevelDoors;
    [SerializeField] private Transform[] _lowerLevelDoors;
    [SerializeField] private Transform[] _elevatorDoors;

    [SerializeField] private Transform _upperPosition;
    [SerializeField] private Transform _lowerPosition;
    private float _step;
    private float _speed = 0.5f;

    private bool _doorsCanOpen = true;
    private bool _isElevatorMoving;
    private Transform[] _targetDoors;

    private AudioSource _audioSource;


    void Start()
    {
        _audioSource= GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        _step = _speed * Time.deltaTime;
        
        UserInputChecker();
        ElevatorMover();
    }

    void UserInputChecker()
    {
       if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenElevatorDoors("upper");
            OpenElevatorDoors("lower");
        } 
    }

    void ElevatorMover()
    {
        if(_isElevatorMoving)
        {            
            if(_isMovingDown)
            {
                _elevator.position = Vector3.MoveTowards(_elevator.position, _lowerPosition.position, _step);

                if(_elevator.position == _lowerPosition.position)
                {
                    _isElevatorMoving= false;
                    OpenElevatorDoors("lower");
                    PlayElevatorDing();

                    _isMovingDown = false;
                    _isMovingUp = true;
                }    
            }

            else if(_isMovingUp)
            {
                _elevator.position = Vector3.MoveTowards(_elevator.position, _upperPosition.position, _step);
                if(_elevator.position == _upperPosition.position)
                {
                    _isElevatorMoving = false;
                    OpenElevatorDoors("upper");
                    PlayElevatorDing();

                    _isMovingUp = false;
                    _isMovingDown= true;
                }
            }
        }
    }

    void PlayElevatorDing()
    {
        _audioSource.Play();
    }

    void OpenElevatorDoors(string condition)
    {
        switch(condition)
        {
            case "upper":
                _targetDoors = _upperLevelDoors;
                break;
            case "lower":
                _targetDoors= _lowerLevelDoors;
                break;
        }

        if(_doorsCanOpen )
        {
            _doorsCanOpen= false;
            StartCoroutine(ElevatorDoorTimer());

            foreach(var door in _targetDoors)
            {
                door.localScale = new Vector3(0.1f, 1, 1);
            }

            foreach(var door in _elevatorDoors)
            {
                door.localScale = new Vector3(0.1f, 0, 0);
            }
        }
    }

    void CloseElevatorDoors()
    {
        foreach(var door in _targetDoors)
        {
            door.localScale = new Vector3(1, 1, 1);
        }

        foreach(var door in _elevatorDoors)
        {
            door.localScale = new Vector3(1, 1, 1);
        }
    }


    IEnumerator ElevatorDoorTimer()
    {
        yield return new WaitForSeconds(8);

        CloseElevatorDoors();

        _doorsCanOpen= true;
        _isElevatorMoving = true;
    }

}
