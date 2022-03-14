using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _jumpForce;
    [SerializeField] float _speed;
    PlayerInput _input;
    InputAction _direction;
    Rigidbody2D _body;
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _direction = _input.actions["Move"];
        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(
            _direction.ReadValue<Vector2>().x * _speed,
            _body.velocity.y
        );
        _body.velocity = direction;
    }

    void OnJump()
    {
        _body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
