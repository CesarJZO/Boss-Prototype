using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animations", menuName = "Animations/Clips")]
public class AnimationClips : ScriptableObject
{
    private List<AnimationClip> _list;
    private Dictionary<string, AnimationClip> _dictionary;

    private void Awake()
    {
        
    }
}