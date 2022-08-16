using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public Angle angleScript;

    [Range(-1, 1)] public float factor;
    
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, angleScript.angle * factor);
    }
}