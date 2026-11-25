using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class movePlayer : MonoBehaviour
{
    public float _velocidadeX;

    bool canDash = true;
    bool isDashing;
    [SerializeField] Vector2 _posicao;

    Rigidbody2D _rig2d;

    [SerializeField] bool _checkGround;

    [SerializeField] Animator _anim;

    [SerializeField] float _speedAnim;

    [SerializeField] float _jumpForce;

    [Header("Dash Settings")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float dashingcooldown;
    private bool _isDashing;

    public bool _isface;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _rig2d= GetComponent<Rigidbody2D>();
        _rig2d.angularVelocity = 0;
    }
    public void SetMove(InputAction.CallbackContext value)
    {
        _posicao = value.ReadValue<Vector2>();
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        if ( _checkGround==true)
        {
            _checkGround = false;
            _rig2d.AddForceY(10 * _jumpForce);
            _rig2d.linearVelocityY = 0;
        }
    }

    public void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    // Update is called once per frame
    public void Update()
    {

        if (isDashing)
        {
            return;
        }
        _rig2d.linearVelocityX = _posicao.x * _velocidadeX;

        _speedAnim = MathF.Abs(_rig2d.linearVelocityX);

        _anim.SetFloat("speed", _speedAnim);
        _anim.SetFloat("pulo", _rig2d.linearVelocityY);
        _anim.SetBool("Ground", _checkGround);

        if (_posicao.x < 0 && _isface == false)
        {
            flip();
        }
        else if (_posicao.x > 0 && _isface == true)
        {
            flip();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("tocou no chão");
            _checkGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("saio do chão");
            _checkGround = false;
        }
    }
    private void flip()
    {
        _isface = !_isface;
        Vector2 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale; 
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = _rig2d.gravityScale;
        _rig2d.gravityScale = 0f;
        _rig2d.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, 0f);
        yield return new WaitForSeconds(_dashDuration);
        _rig2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingcooldown);
        canDash = true;
    }
}