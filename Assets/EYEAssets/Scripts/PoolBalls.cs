using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBalls : MonoBehaviour
{
    private Rigidbody _rb;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        AddForceToBallAtPosition();
    }

    
    void Update()
    {
    }

    void AddForceToBallAtPosition()
    {
        Vector3 direction = _rb.transform.position - transform.position;
        _rb.AddForceAtPosition(direction.normalized, transform.position, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cue")
        {
            AddForceToBallAtPosition();
        }
    }
}
