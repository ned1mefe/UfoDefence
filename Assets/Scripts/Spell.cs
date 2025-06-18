using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxScale;
    [SerializeField] private float lifeAfterFullSize;
    [SerializeField] private float xScalePerY;
    private bool _fullyGrown;
    private float _timer;
    private Vector3 _base;

    private void Start()
    {
        _fullyGrown = false;
        _timer = 0f;
        _base = new Vector3(xScalePerY, 1f, 1f);
    }

    private void Update()
    {
        if (!_fullyGrown)
        {
            transform.localScale += _base * (growSpeed * Time.deltaTime);

            if (transform.localScale.y >= maxScale)
            {
                _fullyGrown = true;
                _timer = lifeAfterFullSize;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}
