using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    [SerializeField] Vector2 _posicao;
    [SerializeField] private Transform respawnPoint;
    Rigidbody2D _rb;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (respawnPoint == null)
        {
            GameObject startPoint = new GameObject("InitialRespawnPoint");
            startPoint.transform.position = transform.position;
            respawnPoint = startPoint.transform;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("checkpoint"))
        {
            respawnPoint = other.transform;
        }

        else if (other.CompareTag("Void"))
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        transform.position = respawnPoint.position;
        _rb.linearVelocity = Vector2.zero;
    }

}
