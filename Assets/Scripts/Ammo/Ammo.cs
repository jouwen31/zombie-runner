using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
  // NEW
  [SerializeField] AmmoSlot[] ammoSlots;


  [System.Serializable]
  private class AmmoSlot
  {
    public AmmoType ammoType;
    public int ammoAmount;
  }

  public int GetAmmoAmount(AmmoType ammoType)
  {
    AmmoSlot slot = GetAmmoSlot(ammoType);
    if (slot != null)
    {
      return slot.ammoAmount;
    }
    return 0;
  }

  public void ReduceAmmoAmount(AmmoType ammoType)
  {
    AmmoSlot slot = GetAmmoSlot(ammoType);
    if (slot != null)
    {
      if (slot.ammoAmount > 0)
      {
        slot.ammoAmount--;
      }
      else
      {
        slot.ammoAmount = 0;
      }
    }
  }

  public void IncreaseAmmoAmount(AmmoType ammoType, int ammoAmount)
  {
    AmmoSlot slot = GetAmmoSlot(ammoType);
    if (slot != null)
    {
      slot.ammoAmount += Mathf.Abs(ammoAmount);
    }
  }

  AmmoSlot GetAmmoSlot(AmmoType ammoType)
  {
    foreach (AmmoSlot slot in ammoSlots)
    {
      if (slot.ammoType == ammoType)
      {
        return slot;
      }
    }
    return null;
  }
}
