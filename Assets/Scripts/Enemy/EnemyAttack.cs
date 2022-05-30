using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  [SerializeField][Range(0, 100)] int damage = 40;
  Transform target;

  EnemyAI enemyAI;
  PlayerHealth playerHealth;
  void Start()
  {
    enemyAI = GetComponent<EnemyAI>();
    if (enemyAI.Target != null)
    {
      target = enemyAI.Target;
      playerHealth = target.GetComponent<PlayerHealth>();
    }
  }

  void AttackHitEvent()
  {
    if (playerHealth != null)
    {
      playerHealth.TakeDamage(damage);
    }
  }
}
