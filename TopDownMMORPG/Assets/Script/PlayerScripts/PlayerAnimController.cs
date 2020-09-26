using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    
    public Animation _playerAnimation;

    public AnimationClip Idle, Walk, Attack;

    [SerializeField]
    public bool IsAttacking;
    
    public void Walking(bool state)
    {
        if (state && !IsAttacking)
        {
            _playerAnimation.Play(Walk.name);
        }
        else if(!IsAttacking)
        {
            _playerAnimation.Play(Idle.name);
        }
    }

    public void Attacking(bool state)
    {
        if (state)
        {
            IsAttacking = true;
            _playerAnimation.Play(Attack.name, PlayMode.StopAll);
        }
        else
        {
            IsAttacking = false;
            _playerAnimation.Play(Idle.name);
        }
    }
}
