using UnityEngine;

public class ForwardTower : Tower
{
    
    private readonly Quaternion _rotation = Quaternion.Euler(0f, 0f, Angle);

    protected override void Shoot()
    {
        Instantiate(attackPrefab, transform.position, _rotation);
    }
}