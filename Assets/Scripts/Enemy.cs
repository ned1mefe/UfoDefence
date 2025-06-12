using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Enter speed as blocks(3 tiles)/s")] 
    [SerializeField] private float speed;
    [SerializeField] private int health;

    private readonly float unitsPerBlock = 1.74f; // should took 7 secs to get to next side, found experimentally
    private readonly Vector3 direction = new Vector3(-0.865746f, -0.500484f); // units move to the same direction, no need to calculate

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * unitsPerBlock * speed; 

    }
    
}
