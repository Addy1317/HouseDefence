using HouseDefence.Services;
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

        private void OnEnable()
        {
            GameService.Instance.eventManager.OnHouseDeathEvent.AddListener(ActivateGameOverMenu);
        }

        private void OnDisable()
        {
            GameService.Instance.eventManager.OnHouseDeathEvent.RemoveListener(ActivateGameOverMenu);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            InputsforPauseButton();
        }

        #region GameOver Methods
        internal void ActivateGameOverMenu()
        {
            _gameOverMenuUI.SetActive(true);
        }
        #endregion

        #region Pause|Resume Methods

        private void InputsforPauseButton()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
            }
        }

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
