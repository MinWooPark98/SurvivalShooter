using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int maxHp = 100;
    public int currHp { get; protected set; }
    public bool isAlive { get; protected set; }

    public virtual void OnDamage(int dmg, Vector3 hitPoint, Vector3 hitNormal)
    {
        currHp -= dmg;
        if (currHp < 0)
        {
            currHp = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        isAlive = false;
    }
}
