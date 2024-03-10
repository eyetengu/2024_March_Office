using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Foos : MonoBehaviour
{
    private Rigidbody _rb;
    

    void Start()
    {
     _rb = GetComponent<Rigidbody>();   
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FoosBall")
        {
            _rb.AddForceAtPosition(Vector3.forward, Vector3.up);
            //_rb.AddForce(new Vector3(1 * Time.deltaTime, 0, 0), ForceMode.Impulse);
        }
    }
}
