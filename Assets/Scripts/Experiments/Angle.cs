using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Angle : MonoBehaviour
{
    public Method method;
    
    //Use these to get the GameObject's positions
    private Vector2 _myFirstVector;
    private Vector2 _mySecondVector;

    float _angle;

    //You must assign to these two GameObjects in the Inspector
    public Transform myObject;
    public Transform myOtherObject;

    void Start()
    {
        //Initialize the Vector
        _myFirstVector = Vector2.zero;
        _mySecondVector = Vector2.zero;
        _angle = 0.0f;
    }

    void Update()
    {
        // if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;

        if (method== Method.Unity)
        {
            //Fetch the first GameObject's position
            _myFirstVector = new Vector2(myObject.transform.position.x, myObject.position.y);
            //Fetch the second GameObject's position
            _mySecondVector = new Vector2(myOtherObject.transform.position.x, myOtherObject.position.y);
            //Find the angle for the two Vectors
            _angle = Vector2.Angle(_myFirstVector, _mySecondVector);

            //Draw lines from origin point to Vectors
            Debug.DrawLine(Vector2.zero, _myFirstVector, Color.magenta);
            Debug.DrawLine(Vector2.zero, _mySecondVector, Color.blue);

            //Log values of Vectors and angle in Console
            Debug.Log("MyFirstVector: " + _myFirstVector);
            Debug.Log("MySecondVector: " + _mySecondVector);
            Debug.Log("Angle Between Objects: " + _angle);
        }
        else
        {
            var myPos = myObject.position;
            var myOtherPos = myOtherObject.position;
            
            Debug.DrawLine(myOtherPos, myPos, Color.green);
            
            Debug.DrawLine(Vector3.zero, Direction(myPos, myOtherPos));
        }
    }

    static Vector2 Direction(Vector2 from, Vector2 to)
    {
        return (from - to).normalized;
    }
    
    void OnGUI()
    {
        //Output the angle found above
        GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects" + _angle);
    }
}

[Serializable]
public enum Method { Cesar, Unity }