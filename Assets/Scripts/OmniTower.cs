using UnityEngine;

public class OmniTower : Tower
{
    protected override void Shoot()
    {
        Instantiate(attackPrefab, transform.position, Quaternion.identity);
    }
    
}
