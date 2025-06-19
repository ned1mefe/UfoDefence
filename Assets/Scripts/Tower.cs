using System.Collections;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    [SerializeField] protected GameObject attackPrefab;
    private Coroutine _attackRoutine;
    protected static readonly float Angle = Mathf.Atan2(Constants.ShootDirection.y, Constants.ShootDirection.x) * Mathf.Rad2Deg - 90f;


    protected abstract void Shoot();

    public void Activate()
    {
        if (_attackRoutine is not null) return;
        
        _attackRoutine = StartCoroutine(AttackLoop());
    }
    
    public void Deactivate()
    {
        if (_attackRoutine == null) return;
        
        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
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
