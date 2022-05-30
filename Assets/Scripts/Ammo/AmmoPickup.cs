using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
  [SerializeField] AmmoType ammoType;
  [SerializeField][Range(0, 100)] int ammoAmount = 10;

  Player player;
  Ammo playerAmmo;

  bool isPickable = true;

  void Awake()
  {
    player = FindObjectOfType<Player>();
    if (player != null)
    {
      playerAmmo = player.GetComponent<Ammo>();
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player" && isPickable)
    {
      isPickable = false;
      ProcessPickup();
    }
  }

  void ProcessPickup()
  {
    if (playerAmmo != null)
    {
      playerAmmo.IncreaseAmmoAmount(ammoType, ammoAmount);
    }
    OnPickupFinish();
  }

  void OnPickupFinish()
  {
    Destroy(gameObject);
  }
}
