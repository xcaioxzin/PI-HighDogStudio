using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float _SpeedX;
    public bool _isface;
    [Serializefield] vector2 _pisicao;

   // [Serializefield] rigidbody2d _rig2d

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rig2d = GetComponet<rigidbody2d>();
        _rig2d.AngularVelocity = 0;
    }
    public void SetMove(InputAction.CallbackContext value)
    {
        _posicao = value.ReadValue<vector2>();
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        if (checkGround == true)
        {
            _rig2d.AddForceY(100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rig2d.linearVelocityX = _pisicao.x * _velocidadeX;
        if (_posicao.x < 0 && _isface == false)
        {
            flip();
        }
        else if (_posicao.x > 0 && _isface == true)
        {
            flip();
        }
        private void (OnTriggerEnter2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                Debug.Log("tocou no chão");
                _checkGround = true;
            }
            private void OnTriggerExit2D(collider2D collision)
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    Debug.Log("saio do chão");
                    _checkground = false;
                }
            }
        }
        private void flip()
        {
            _isface = !_isface;
            vector2 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
}
