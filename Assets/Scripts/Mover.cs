using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Mover : MonoBehaviour
{
    public float speed;
    InputAction moveAction;
    void Awake()
    {
        moveAction = GetComponent<PlayerInput>().actions["Move"];
    }
    void Update()
    {
        transform.Translate(moveAction.ReadValue<Vector2>() * Time.deltaTime * speed);
    }
}
