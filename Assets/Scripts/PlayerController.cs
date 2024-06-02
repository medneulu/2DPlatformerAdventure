using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private float _walkSpeed;
    private Vector2 _moveInput;

    [SerializeField]
    private bool _isMoving = false;
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int IsMove = Animator.StringToHash("isMoving");
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            _animator.SetBool("isMoving", value);
        }
    }
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
                _isFacingRight = value;
            }
        }
    }
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput.x * _walkSpeed, _rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        IsMoving = _moveInput != Vector2.zero;

        SetFacingDir(_moveInput);
    }

    private void SetFacingDir(Vector2 moveInput)
    {
        if (moveInput.x > 0)
        {
            // Face the right
            IsFacingRight = true;
        }else if(moveInput.x < 0)
        {
            // Face the left
            IsFacingRight = false;
        }
        else if (moveInput.x == 0)
        {
            // Keep the current direction when not moving horizontally
            // This prevents unnecessary flipping while idle
            return;
        }
    }
}
