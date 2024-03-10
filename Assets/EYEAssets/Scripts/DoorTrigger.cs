using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger("OpenDoor");
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetTrigger("CloseDoor");
    }
}
