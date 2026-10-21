using UnityEngine;
using UnityEngine.InputSystem;

public class movePlayer : MonoBehaviour
{
    public float _velocidadeX;
 
    [SerializeField] Vector2 _posicao;

    Rigidbody2D _rig2d;

    [SerializeField] bool _checkGround;

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
            _rig2d.AddForceY(100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rig2d.linearVelocityX = _posicao.x * _velocidadeX;

        if (_posicao.x < 0 && _isface == false)
        {
            flip();
        }
        else if (_posicao.x > 0 && _isface == true)
        {
            flip();
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
}