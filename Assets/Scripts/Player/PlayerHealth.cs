using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  [SerializeField][Range(1, 1000)] int playerHealth = 100;

  GameManager gameManager;

  int currentHealth;
  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    currentHealth = playerHealth;
  }

  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      HandlePlayerDie();
    }
  }

  void HandlePlayerDie()
  {
    if (gameManager != null)
    {
      gameManager.ShowGameOverUI();
    }
  }
}
