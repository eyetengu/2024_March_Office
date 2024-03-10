using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuttingGreen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _bossAnimator;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bossAnimator.SetTrigger("Putt");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger " + other.name);

        if(other.tag == "Club")
        {
            _animator.SetTrigger("Putt");
        }
    }
}
