using System;
using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;

public class playerMovementRework : Attractable
{

    #region Attributes

    [SerializeField, Range(0, 100F)] private float _maxSpeed = 4f;
    [SerializeField, Range(0, 100F)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0, 100F)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0, 100F)] private float _friction = 4f;
    
    [SerializeField]
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    #endregion

    #region Methods

    #endregion
    
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _friction, 0F);
    }

    void FixedUpdate()
    {
        Vector2 _velocity = _body.velocity;
        float _acceleration = true ? _maxAcceleration : _maxAirAcceleration;
        
        float _maxSpeedChange = _acceleration * Time.deltaTime;
        
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
        
        _body.velocity = _velocity;
    }
    
    private bool IsGrounded()
    {
        return _body.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
