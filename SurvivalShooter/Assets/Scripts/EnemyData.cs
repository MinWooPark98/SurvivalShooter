using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;
    public int maxHp;
    public int attackDmg;
    public float attackDelay;
    public int score;
    public string hitSound;
    public string dieSound;
}
