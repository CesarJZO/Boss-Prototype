using UnityEngine;

public class Centry : MonoBehaviour
{
    [Header("Horizontal")]
    public bool allowX;
    public float hSpeed = 1f;
    public float hRange = 1f;
    
    [Header("Vertical")]
    public bool allowY;
    public float vSpeed = 1f;
    public float vRange = 1f;

    private void Update()
    {
        var h = 0f;
        var v = 0f;
        
        if (allowX)
            h = Mathf.Sin(Time.time * hSpeed) * hRange;
        if (allowY)
            v = Mathf.Sin(Time.time * vSpeed) * vRange;
        
        transform.position = new Vector3
        {
            x = h,
            y = v
        };
    }
}