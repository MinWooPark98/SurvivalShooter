using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class Enemy : LivingEntity
{
    public EnemyData enemyData;
    private bool hasTarget = false;
    public LivingEntity targetEntity;
    public NavMeshAgent pathFinder;
    private float lastAttackTime;
    private Animator animator;
    public ParticleSystem hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        isAlive = false;
        maxHp = enemyData.maxHp;
    }

    public void ResetAll()
    {
        gameObject.SetActive(true);
        currHp = maxHp;
        isAlive = true;
        lastAttackTime = 0f;
        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        animator = GetComponent<Animator>();
        animator.SetBool("Move", false);
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (isAlive)
        {
            if (hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
                animator.SetBool("Move", true);
            }
            else
            {
                pathFinder.isStopped = true;
                animator.SetBool("Move", false);
                var players = GameObject.FindGameObjectsWithTag("Player");
                targetEntity = players[Random.Range(0, players.Length - 1)].GetComponent<LivingEntity>();
                if (targetEntity != null)
                    hasTarget = true;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time - lastAttackTime < enemyData.attackDelay)
            return;
        var target = other.GetComponent<LivingEntity>();
        if (target != null && target == targetEntity)
        {
            lastAttackTime = Time.time;
            target.OnDamage(enemyData.attackDmg, other.ClosestPoint(transform.position), transform.forward);
        }
    }

    public override void OnDamage(int dmg, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!isAlive)
            return;
        SoundManager.instance.PlayEffectSound(enemyData.hitSound);
        base.OnDamage(dmg, hitPoint, hitNormal);
        hitEffect.transform.position = hitPoint;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();
    }

    public override void Die()
    {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();
        SoundManager.instance.PlayEffectSound(enemyData.dieSound);
        //pathFinder.isStopped = true;
        pathFinder.enabled = false;
        animator.SetTrigger("Die");
        //enemyAudioPlayer.PlayOneShot(deathSound);
        GameManager.instance.AddScore(enemyData.score);
   }

    public void StartSinking()
    {
        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        StartCoroutine(ToDestroy());
    }

    private IEnumerator ToDestroy()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
