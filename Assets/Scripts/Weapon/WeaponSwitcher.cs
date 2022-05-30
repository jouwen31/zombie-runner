using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
  [SerializeField] int activeWeaponIndex = 0;

  int previousActiveWeapon;
  int currentActiveWeapon;
  bool initializingWeapons = true;

  void Start()
  {
    previousActiveWeapon = activeWeaponIndex;
    SetActiveWeapon();
  }
  void Update()
  {
    ProcessScrollWheel();
    ProcessKeyInput();

    SetActiveWeapon();
  }
  private void ProcessScrollWheel()
  {
    if (Input.GetAxis("Mouse ScrollWheel") > 0)
    {
      if (currentActiveWeapon == 2)
      {
        currentActiveWeapon = 0;
      }
      else
      {
        currentActiveWeapon++;
      }
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0)
    {
      if (currentActiveWeapon == 0)
      {
        currentActiveWeapon = 2;
      }
      else
      {
        currentActiveWeapon--;
      }
    }
  }

  private void ProcessKeyInput()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      currentActiveWeapon = 0;
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      currentActiveWeapon = 1;
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      currentActiveWeapon = 2;
    }
  }
  void SetActiveWeapon()
  {
    if (previousActiveWeapon != currentActiveWeapon || initializingWeapons)
    {
      initializingWeapons = false;
      previousActiveWeapon = currentActiveWeapon;
      int weaponIndex = 0;

      foreach (Transform weapon in transform)
      {
        if (weaponIndex == currentActiveWeapon)
        {
          weapon.gameObject.SetActive(true);
        }
        else
        {
          weapon.gameObject.SetActive(false);
        }
        weaponIndex++;
      }

    }
  }
}
