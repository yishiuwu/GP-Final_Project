using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimState : MonoBehaviour
{
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    // public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    public static readonly int Fall = Animator.StringToHash(nameof(Fall));
    public static readonly int Run = Animator.StringToHash(nameof(Run));
    public static readonly int Victory = Animator.StringToHash(nameof(Victory));
    public static readonly int Melt = Animator.StringToHash(nameof(Melt));
}
