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
    
}
