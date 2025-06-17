using UnityEngine;

public class OmniTower : Tower
{
    protected override void Shoot()
    {
        float[] angles = {  // cant just add 45, its an isometric map
            Angle,                 
            180f - Angle,          
            -Angle,                
            -(180f - Angle)        
        };
        var position = transform.position;

        foreach (float a in angles)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, a);
            Instantiate(projectilePrefab, position, rotation);
        }
        
    }
    
}
