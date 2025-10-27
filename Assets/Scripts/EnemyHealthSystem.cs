using System.Collections;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IDamageable, IHealthSystem
{
    public int Health { get; private set; } = 100;

    private Coroutine _coroutine;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }

        _coroutine ??= StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            renderer.material.color = originalColor;
        }
        
        _coroutine = null;
    }
}