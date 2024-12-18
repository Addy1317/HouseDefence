#region Summary
// GameManager is responsible for managing core game states in the HouseDefence namespace. 
// It handles:
// - Pause and Resume functionality using Time.timeScale.
// - Game Over state activation when the house is destroyed, triggered via OnHouseDeathEvent.
// - Scene initialization when the "MainGame" scene is loaded.
//
// Key Features:
// - Pause and Game Over UI panels activation/deactivation.
// - Handles game pause toggle via a Pause button.
// - Manages totalKills tracking and game initialization methods.
//
// Dependencies:
// - EventManager for listening to game events.
// - GameService for accessing global services.
//
// Notes:
// - Time.timeScale is used to pause and resume game time.
// - The class is scene-aware and sets up necessary systems when "MainGame" is loaded.
#endregion
using TowerDefence.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Pause Menu UI")]
        [SerializeField] GameObject _pauseMenuUI;

        [Header("PauseButton")]
        [SerializeField] private Button _pauseButton;

        [Header("GameOver Menu UI")]
        [SerializeField] private GameObject _gameOverMenuUI;

        private bool _isPaused = false;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameService.Instance.eventManager.OnHouseDeathEvent.AddListener(ActivateGameOverMenu);
            _pauseButton.onClick.AddListener(OnPauseButton);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            GameService.Instance.eventManager.OnHouseDeathEvent.RemoveListener(ActivateGameOverMenu);
            _pauseButton.onClick.RemoveListener(OnPauseButton);
        }

        private void Start()
        {
            GameService.Instance.vfxManager.AddHoverEffect(_pauseButton);
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

        internal void TogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        internal void PauseGame()
        {
            Time.timeScale = 0f;
            _isPaused = true;
            Debug.Log("Game Paused");
        }

        internal void ResumeGame()
        {
            Time.timeScale = 1f;
            _isPaused = false;
            Debug.Log("Game Resumed");
        }

        internal bool IsGamePaused()
        {
            return _isPaused;
        }

        #endregion

        #region Game Initialization Methods

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainGame")
            {
                // Initialize or reset all systems (player, enemies, UI, etc.)
                InitializeGame();
            }
        }

        private void InitializeGame()
        {

        }
        #endregion
    }
}
