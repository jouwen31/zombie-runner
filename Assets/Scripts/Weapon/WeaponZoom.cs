using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Weapon))]
public class WeaponZoom : MonoBehaviour
{
  [SerializeField][Range(20, 100)] int normalFOV = 60;
  [SerializeField][Range(20, 100)] int zoomedFOV = 30;

  [SerializeField][Range(1, 5)] int normalMouseSensitivity = 2;
  [SerializeField][Range(1, 5)] int zoomedMouseSensitivity = 1;

  Camera playerCamera;
  RigidbodyFirstPersonController rigidbodyFirstPersonController;

  bool isNormalFOV = true;

  void Awake()
  {
    playerCamera = FindObjectOfType<Camera>();
    rigidbodyFirstPersonController = FindObjectOfType<RigidbodyFirstPersonController>();
  }

  void OnEnable()
  {
    UpdatePlayerCameraFOV();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Z))
    {
      isNormalFOV = !isNormalFOV;
      UpdatePlayerCameraFOV();
    }
  }

  void UpdatePlayerCameraFOV()
  {
    if (isNormalFOV)
    {
      playerCamera.fieldOfView = normalFOV;
      rigidbodyFirstPersonController.mouseLook.XSensitivity = normalMouseSensitivity;
      rigidbodyFirstPersonController.mouseLook.YSensitivity = normalMouseSensitivity;
    }
    else
    {
      playerCamera.fieldOfView = zoomedFOV;
      rigidbodyFirstPersonController.mouseLook.XSensitivity = zoomedMouseSensitivity;
      rigidbodyFirstPersonController.mouseLook.YSensitivity = zoomedMouseSensitivity;
    }
  }
}
