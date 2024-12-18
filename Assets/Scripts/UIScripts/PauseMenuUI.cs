#region Summary
// The PauseMenuUI class manages the pause menu functionality, including showing and hiding the pause menu and settings panel.
// It handles button clicks for resuming the game, restarting, going to the main menu, opening settings, and quitting the game.
// The class also allows for closing the settings panel via a dedicated button.
#endregion
using TowerDefence.Manager;
using TowerDefence.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [Header("PauseMenu Panels")]
        [SerializeField] private GameObject _pauseMenuPanel;
        [SerializeField] private GameObject _settingsPanel;

        [Header("PaueMenu UI Buttons")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        [Header("Settings Panel Buttons")]
        [SerializeField] private Button _settingsPanelCloseButton;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButton);
            _restartButton.onClick.AddListener(OnRestartButton);
            _homeButton.onClick.AddListener(OnHomeButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _quitButton.onClick.AddListener(OnQuitButton);

            _settingsPanelCloseButton.onClick.AddListener(OnSettingsPanelCloseButton);

        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButton);
            _restartButton.onClick.RemoveListener(OnRestartButton);
            _homeButton.onClick.RemoveListener(OnHomeButton);
            _settingsButton.onClick.RemoveListener(OnSettingsButton);
            _quitButton.onClick.RemoveListener(OnQuitButton);

            _settingsPanelCloseButton.onClick.RemoveListener(OnSettingsPanelCloseButton);
        }

        private void Start()
        {
            GameService.Instance.vfxManager.AddHoverEffect(_resumeButton);
            GameService.Instance.vfxManager.AddHoverEffect(_restartButton);
            GameService.Instance.vfxManager.AddHoverEffect(_homeButton);
            GameService.Instance.vfxManager.AddHoverEffect(_settingsButton);
            GameService.Instance.vfxManager.AddHoverEffect(_quitButton);
            GameService.Instance.vfxManager.AddHoverEffect(_settingsPanelCloseButton);
        }

        #region PauseMenu Buttons Methods

        private void OnResumeButton()
        {
            _pauseMenuPanel.SetActive(false);
        }

        private void OnRestartButton()
        {
            SceneManager.LoadScene("MainGame");
        }

        private void OnHomeButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnSettingsButton()
        {
            _settingsPanel.SetActive(true);
        }

        private void OnQuitButton()
        {
            Application.Quit();
        }
        #endregion

        #region SettingsPanel Buttons Methods
        private void OnSettingsPanelCloseButton()
        {
            _settingsPanel.SetActive(false);
        }
        #endregion
    }
}
