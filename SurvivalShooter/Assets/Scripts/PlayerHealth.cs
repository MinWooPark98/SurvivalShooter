using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
        isAlive = true;
    }

    public override void OnDamage(int dmg, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!isAlive)
            return;
        SoundManager.instance.PlayEffectSound("Player Hurt");
        base.OnDamage(dmg, hitPoint, hitNormal);
        UiManager.instance.SetHpBar((float)currHp / maxHp);
        StartCoroutine(UiManager.instance.HitEffect());
    }
    
    public override void Die()
    {
        base.Die();
        GameManager.instance.Unpause();
        SoundManager.instance.PlayEffectSound("Player Death");
        StartCoroutine(UiManager.instance.GameOver());
    }
}
