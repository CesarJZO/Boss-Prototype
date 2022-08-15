using UnityEngine;

public class DurationDisplayer : MonoBehaviour
{
    public Clips clips;
    public AnimationClip animationClip;

    private void Start()
    {
        print(clips.JumpName);
    }
}