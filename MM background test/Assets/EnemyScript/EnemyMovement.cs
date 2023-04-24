using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;

    private Playerawarenesscontroller _playerAwarenessController;
    private Vector2 _targetDirection;
   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<Playerawarenesscontroller>();
    }

private void FixedUpdate()
{
    UpdateTargetDirection();
    RotateTowardsTarget();
    SetVelocity();
}
private void UpdateTargetDirection() 
{
    if (_playerAwarenessController.AwareofPlayer)
    {
            _targetDirection = _playerAwarenessController.DirectiontoPlayer;
    }
    else 
    {
        _targetDirection = Vector2.zero;
    }
}

private void RotateTowardsTarget()
{
    if (_targetDirection == Vector2.zero) 
    {
        return;
    }

    Quaternion targetRotation = Quaternion.LookRotation(transform.forward,  _targetDirection);
    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed *Time.deltaTime);

    _rigidbody.SetRotation(rotation);

}

private void SetVelocity()
{
    if (_targetDirection == Vector2.zero)
    {
            _rigidbody.velocity = Vector2.zero;
    }
    else 
    {
        _rigidbody.velocity = transform.up * _speed;
    }
}

}
