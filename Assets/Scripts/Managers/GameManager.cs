using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Pause Menu UI")]
        [SerializeField] private GameObject _pauseMenuUI;

        [Header("GameOver Menu UI")]
        [SerializeField] private GameObject _gameOverMenuUI;

        private bool isPaused = false;

        private void Update()
        {
            InputsforPauseButton();
        }

        private void InputsforPauseButton()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
            }
        }

        internal void ActivateGameOverMenu()
        {
            _gameOverMenuUI.SetActive(true);
        }

        #region Pause|Resume Methods
        public void TogglePause()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log("Game Paused");
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Game Resumed");
        }

        public bool IsGamePaused()
        {
            return isPaused;
        }

        #endregion
    }
}
