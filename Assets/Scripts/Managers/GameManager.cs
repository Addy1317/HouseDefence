using HouseDefence.Services;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Pause Menu UI")]
        [SerializeField]  GameObject _pauseMenuUI; 
        [Header("PauseButton")]
        [SerializeField] private Button _pauseButton;

        [Header("GameOver Menu UI")]
        [SerializeField] private GameObject _gameOverMenuUI;

        private bool isPaused = false;
        private int totalKills = 0;

        private void OnEnable()
        {
            GameService.Instance.eventManager.OnHouseDeathEvent.AddListener(ActivateGameOverMenu);
            _pauseButton.onClick.AddListener(OnPauseButton);
        }

        private void OnDisable()
        {
            GameService.Instance.eventManager.OnHouseDeathEvent.RemoveListener(ActivateGameOverMenu);
            _pauseButton.onClick.RemoveListener(OnPauseButton);
        }

        #region GameOver Methods
        internal void ActivateGameOverMenu()
        {
            _gameOverMenuUI.SetActive(true);
            TogglePause();
        }
        #endregion

        #region Pause|Resume Methods
        private void OnPauseButton()
        {
            _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
            TogglePause();
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
