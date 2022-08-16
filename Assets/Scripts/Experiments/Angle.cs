using System;
using UnityEngine;

public class Angle : MonoBehaviour
{
    public Method method;

    [HideInInspector] public float angle;

    public Transform from;
    public Transform to;

    private void Update()
    {
        if (method == Method.Angle)
        {
            var fromPos = from.position;
            var toPos = to.position;
            angle = Vector3.SignedAngle(fromPos, toPos, Vector3.back);
            
            Debug.DrawLine(Vector2.zero, fromPos, Color.magenta);
            Debug.DrawLine(Vector2.zero, toPos, Color.blue);
        }
        else
        {
            Debug.DrawLine(from.position, to.position, Color.green);
            
            Debug.DrawLine(Vector3.zero, Direction);
        }
    }

    private Vector2 Direction => (to.position - from.position).normalized;

    private void OnGUI()
    {
        string message;
        if (method == Method.Angle)
            message = $"Angle Between Objects {angle}";
        else
        {
            message = $"Direction {Direction}";
        }
        GUI.Label(new Rect(25, 25, 200, 40), message);
    }
}

[Serializable]
public enum Method { Angle, Direction }