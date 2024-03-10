using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _rotationSpeed = 3f;
    [SerializeField] float _speedMultiplier = 1f;
    [SerializeField] float _rotationSpeedMultiplier = 30f;
    float _step;
    float _rotationStep;
    float _rotateValue;
    float _rotateCamVal;
    [SerializeField] Transform _cameraPlatform;
    private PlayerAnimator _playerAnimator;


    void Start()
    {
        //_agent = GetComponent<NavMeshAgent>();
        _playerAnimator= GetComponent<PlayerAnimator>();
    }

    void FixedUpdate()
    {
        _step = _speed * _speedMultiplier * Time.deltaTime;
        _rotationStep = _rotationSpeed * _rotationSpeedMultiplier * Time.deltaTime;

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalCamera = Input.GetAxis("Mouse X");
        float verticalCamera = Input.GetAxis("Mouse Y");
        
        if(Input.GetKeyDown(KeyCode.Space) && verticalInput > 0)
        {
            PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerCrouch();
        }

        //Movement
        Vector2 moveDir = new Vector2(horizontalMovement, verticalInput);
        Vector3 newMoveDir = new Vector3(-moveDir.x, 0, -moveDir.y);
        MovePlayer(moveDir * _step);

        //Rotations
        _rotateValue = horizontalCamera;
        RotatePlayer(_rotateValue * _rotationStep);
        _rotateCamVal = verticalCamera;
        RotateCamera(_rotateCamVal * _rotationStep);
    }

    public void MovePlayer(Vector2 moveDir)
    {
        var direction = new Vector3(moveDir.x, 0, moveDir.y);
        transform.Translate (direction);

        _playerAnimator.MovePlayer(moveDir);
    }

    void RotatePlayer(float rotateValue)
    {
        transform.Rotate(0, rotateValue * _rotationStep, 0);

        _playerAnimator.RotatePlayer(_rotationStep);
    }

    void PlayerJump()
    {
        _playerAnimator.JumpPlayer();
    }

    void PlayerCrouch()
    {
        _playerAnimator.CrouchPlayer();
    }

    void RotateCamera(float mouseY)
    {
        _cameraPlatform.transform.Rotate(-mouseY, 0, 0);
    }
}
