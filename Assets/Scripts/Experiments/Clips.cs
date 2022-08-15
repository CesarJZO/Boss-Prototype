using System;
using UnityEngine;

[Serializable]
public struct Clips
{
    [SerializeField] private AnimationClip idle;
    public float IdleTIme => idle.length;

    [SerializeField] private AnimationClip run;
    public float RunTime => run.length;
    
    [SerializeField] private AnimationClip jump;
    public float JumpTime => jump.length;
    public string JumpName => jump.name;
}