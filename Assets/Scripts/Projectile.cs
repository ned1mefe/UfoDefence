using UnityEngine;

public class Arrow : MonoBehaviour
{ 
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [Tooltip("Enter range as blocks (3 tiles).")] 
    [SerializeField] private int range;
    private float _startTime;
    private float _rangeAsCoordinates;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startTime = Time.time;
        _rangeAsCoordinates = range * Constants.BlockSize;
        _rb.velocity = new Vector2(transform.up.x * speed, transform.up.y * speed);
    }

    private void Update()
    {
        CheckRange();
    }
    
    private void CheckRange()
    {
        float elapsedTime = Time.time - _startTime;
        float traveledDistance = speed * elapsedTime;

        if (traveledDistance >= _rangeAsCoordinates)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}