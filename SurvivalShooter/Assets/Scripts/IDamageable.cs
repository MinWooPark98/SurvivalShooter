using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(int dmg, Vector3 hitPoint, Vector3 hitNormal);
}
