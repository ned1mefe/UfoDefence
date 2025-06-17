using System.Collections;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    [SerializeField] protected GameObject projectilePrefab;
    private Coroutine _attackRoutine;
    protected static readonly float Angle = Mathf.Atan2(Constants.ShootDirection.y, Constants.ShootDirection.x) * Mathf.Rad2Deg - 90f;


    protected abstract void Shoot();

    protected virtual void Start()
    {
        _attackRoutine = StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(attackInterval);
        }
    }
    
    
}
