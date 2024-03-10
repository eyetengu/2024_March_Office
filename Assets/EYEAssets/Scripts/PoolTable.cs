using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PoolTable : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private Manager_UI _uiManager;
    [SerializeField] private CinemachineVirtualCamera _poolVCam;
    //[SerializeField] private Transform _poolCue;
    [SerializeField] private Transform _cueTransform;
    [SerializeField] private Vector3 _cueTarget;
    private Vector3 _targetRotation;

    [SerializeField] private float _cueSpeed;
    private float _step;
    private bool _canShoot = true;

    private void Update()
    {
        _step = _cueSpeed * Time.deltaTime;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            _cueTarget = hitInfo.point;
        }
        RotateCueTowardsMousePosition();

        if (Input.GetMouseButtonDown(0))
        {
            if (_canShoot)
            {
                _canShoot = false;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        StartCoroutine(ReturnCueTimer());

        _cueTransform.Translate(new Vector3(0, 0, .2f));
    }

    void ReturnCueToStart()
    {
        _cueTransform.Translate(new Vector3(0, 0, -.2f));
    }

    IEnumerator ReturnCueTimer()
    {
        yield return new WaitForSeconds(.15f);
        ReturnCueToStart();
        _canShoot = true;
    }

    void RotateCueTowardsMousePosition()
    {
        //var mousePos = Mouse.current.position;
        //_targetRotation = mousePos - _cueTransform.position;
        //var newDirection = Vector3.RotateTowards(transform.forward, _targetRotation, _step, 0.0f);
        //_cueTransform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.gameObject.SetActive(false);
            _score++;

            _uiManager.UpdateScoreText(_score);
        }

        if(other.tag == "Player")
        {
            _poolVCam.m_Priority = 55;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _poolVCam.m_Priority = 10;
        }
    }
}
