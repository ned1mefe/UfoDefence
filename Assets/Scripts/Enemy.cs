using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Enter speed as blocks(3 tiles)/s")] 
    [SerializeField] private float speed;
    [SerializeField] private int health;

    
    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Constants.EnemyWalkDirection * Constants.BlockSize * speed; 
    }

    public void TakeDamage(int damage)
    {
        if (health <= damage)
        {
            Die();
            return;
        }
        
        health -= damage;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CheckWinLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.LoseLevel();
        }
    }
}
