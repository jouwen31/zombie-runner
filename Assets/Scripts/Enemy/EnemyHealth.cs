using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField] int hitPoints = 100;

  EnemyAI enemyAI;
  int currentHealth;
  bool isDead = false;

  void Awake()
  {
    enemyAI = GetComponent<EnemyAI>();
  }

  void OnEnable()
  {
    isDead = false;
    currentHealth = hitPoints;
  }

  public void TakeDamage(int damageAmount)
  {
    if (isDead == false)
    {
      BroadcastMessage("OnTakeDamage");
      if (damageAmount > currentHealth)
      {
        currentHealth = 0;
      }
      else
      {
        currentHealth -= damageAmount;
      }

      if (currentHealth <= 0)
      {
        HandleOnDie();
      }
    }
  }

  void HandleOnDie()
  {
    isDead = true;
    if (enemyAI != null)
    {
      enemyAI.Die();
    }
  }
}
