using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    
    public Animation animation;

    public AnimationClip Idle, Walk, Attack;

    bool IsAttacking;
    
    public void Walking(bool state)
    {
        if (state && !IsAttacking)
        {
            animation.Play(Walk.name);
        }
        else if(!IsAttacking)
        {
            animation.Play(Idle.name);
        }
    }

    public void Attacking(bool state)
    {
        if (state)
        {
            IsAttacking = true;
            animation.Play(Attack.name, PlayMode.StopAll);
        }
        else
        {
            IsAttacking = false;
            animation.Play(Idle.name);
        }
    }
}
