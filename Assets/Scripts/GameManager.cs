using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject gameOverCanvas;

  WeaponSwitcher weaponSwitcher;

  void Awake()
  {
    if (gameOverCanvas != null)
    {
      gameOverCanvas.SetActive(false);
    }
  }

  void Start()
  {
    weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
  }

  public void ShowGameOverUI()
  {
    StopGameInteractions();
    if (gameOverCanvas != null)
    {
      gameOverCanvas.SetActive(true);
    }
  }

  void StopGameInteractions()
  {
    weaponSwitcher.enabled = false;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    Time.timeScale = 0;
  }

  void StartGameInteractions()
  {
    Time.timeScale = 1;
  }

  public void ReloadScene()
  {
    StartGameInteractions();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void QuitGame()
  {
    StartGameInteractions();
    Application.Quit();
  }
}
