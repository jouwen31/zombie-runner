using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  [SerializeField][Range(0, 150)] int shootRange = 100;
  [SerializeField] Camera FPCamera;
  [SerializeField][Range(0, 100)] int shootDamage = 20;
  [SerializeField] ParticleSystem shootVFX;
  [SerializeField] GameObject hitImpactVFX;
  [SerializeField][Range(2, 5)] int delayToDeleteHitImpactGB = 3;
  [SerializeField][Range(0, 10)] float delayBetweenShoots = 1f;

  [SerializeField] Player player;
  [SerializeField] AmmoType ammoType;

  Ammo playerAmmo;

  bool canShoot = true;

  void Awake()
  {
    if (player != null)
    {
      playerAmmo = player.GetComponent<Ammo>();
    }
  }

  void OnEnable()
  {
    canShoot = true;
  }

  void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      StartCoroutine(Shoot());
    }
  }

  IEnumerator Shoot()
  {
    if (playerAmmo != null && playerAmmo.GetAmmoAmount(ammoType) > 0 && canShoot)
    {
      canShoot = false;
      playerAmmo.ReduceAmmoAmount(ammoType);
      PlaySHootingVFX();
      ProcessShooting();

      yield return new WaitForSeconds(delayBetweenShoots);

      canShoot = true;
    }

  }

  void PlaySHootingVFX()
  {
    if (shootVFX != null)
    {
      shootVFX.Play();
    }
  }

  void ProcessShooting()
  {
    if (FPCamera != null)
    {
      RaycastHit hit;

      bool hitSomething = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, shootRange);

      if (hitSomething)
      {
        PlayHitImpactVFX(hit);
        EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
          enemyHealth.TakeDamage(shootDamage);
        }
        else
        {
          EnemyHealth enemyHealthParent = hit.transform.GetComponentInParent<EnemyHealth>();
          if (enemyHealthParent != null)
          {
            enemyHealthParent.TakeDamage(shootDamage);
          }
        }
      }
    }
  }

  void PlayHitImpactVFX(RaycastHit hit)
  {
    if (hitImpactVFX != null && hit.point != null)
    {
      GameObject hitImpactGB = Instantiate(hitImpactVFX, hit.point, Quaternion.LookRotation(hit.point));
      hitImpactGB.GetComponent<ParticleSystem>().Play();
      Destroy(hitImpactGB, delayToDeleteHitImpactGB);
    }
  }
}
