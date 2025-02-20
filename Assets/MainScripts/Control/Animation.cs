using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsMoverMoving = Animator.StringToHash("isMove");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }


    public void MoverMove(Vector2 obj)
    {
        animator.SetBool(IsMoverMoving, obj.magnitude > .5f);
    }

    
    public void isOnMover()
    {
        
    }

    public void OutMover()
    {
        
    }

}
