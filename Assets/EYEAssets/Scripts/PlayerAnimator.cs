using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private bool _isCrouched;


    void Start()
    {
        _animator = GetComponentInChildren<Animator>();    
    }
    
    public void Update()
    {
        
    }

    public void MovePlayer(Vector2 value)
    {
        float movex = value.x * 120;
        float movez = value.y * 120;

        _animator.SetFloat("MovementZ", 1);
        _animator.SetFloat("MovementX", 1);
        
        //_animator.SetFloat("Direction", )
    }

    public void RotatePlayer(float value)
    {
        //_animator.SetFloat("MovementX", value);
    }

    public void RunPlayer()
    {
        _animator.SetFloat("Speed", .8f);
    }

    public void CrouchPlayer()
    {
        _isCrouched = !_isCrouched;

        _animator.SetBool("Crouch", _isCrouched);
    }

    public void JumpPlayer()
    {
        _animator.SetBool("Jump", true);
        StartCoroutine(JumpTimer());
    }

    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(.2f);
        _animator.SetBool("Jump", false);
    }

}
