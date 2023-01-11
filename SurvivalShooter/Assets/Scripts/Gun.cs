using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    private float lastShot;
    public Transform gunBarrelEndTransform;
    public LineRenderer bulletLine;
    public ParticleSystem fireEffect;

    // Start is called before the first frame update
    void Start()
    {
        bulletLine = GetComponent<LineRenderer>();
        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    public void Fire()
    {
        if (Time.time - lastShot < gunData.attackDelay)
            return;
        SoundManager.instance.PlayEffectSound("Player GunShot");
        lastShot = Time.time;
        Vector3 hitPos;
        RaycastHit hit;
        var ray = new Ray(gunBarrelEndTransform.position, gunBarrelEndTransform.forward);
        if (Physics.Raycast(ray, out hit, gunData.attackDistance))
        {
            hitPos = hit.point;
            var target = hit.collider.GetComponent<LivingEntity>();
            if (target != null)
                target.OnDamage(gunData.attackDmg, hitPos, hit.normal);
        }
        else
            hitPos = gunBarrelEndTransform.position + gunBarrelEndTransform.forward * gunData.attackDistance;
        StartCoroutine(FireEffect(hitPos));
    }

    private IEnumerator FireEffect(Vector3 hitPos)
    {
        fireEffect.Play();

        bulletLine.SetPosition(0, gunBarrelEndTransform.position);
        bulletLine.SetPosition(1, hitPos);
        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.015f);
        bulletLine.enabled = false;
    }
}
