#region Summary
/// <summary>
/// Manages the Main Menu UI, including navigation between panels and game start functionality.
/// </summary>
#endregion
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HouseDefence.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("HomeScreen Reference")]
        [SerializeField] private GameObject _homeScreenPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _controlsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        [Header("Controls Panel References")]
        [SerializeField] private GameObject _controlsPanel;
        [SerializeField] private Button _controlsPanelCloseButton;

        [Header("Settings Panel References")]
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _settingsPanelCloseButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButton);
            _controlsButton.onClick.AddListener(OnControlsButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _quitButton.onClick.AddListener(OnQuitButton);

            _controlsPanelCloseButton.onClick.AddListener(OnControlPanelCloseButton);

            _settingsPanelCloseButton.onClick.AddListener(OnSettingsPanelCloseButton);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButton);
            _controlsButton.onClick.RemoveListener(OnControlsButton);
            _settingsButton.onClick.RemoveListener(OnSettingsButton);
            _quitButton.onClick.RemoveListener(OnQuitButton);

            _controlsPanelCloseButton.onClick.RemoveListener(OnControlPanelCloseButton);

            _settingsPanelCloseButton.onClick.RemoveListener(OnSettingsPanelCloseButton);
        }

        #region HomeScreen Buttons Methods

        private void OnPlayButton()
        {
            SceneManager.LoadScene("MainGame");
        }

        private void OnControlsButton()
        {
            _controlsPanel.SetActive(true);
        }

        private void OnSettingsButton()
        {
            _settingsPanel.SetActive(true);
        }

        private void OnQuitButton()
        {
            Application.Quit();
            Debug.Log("Application is Quitting");
        }

        #endregion

        #region Control Panels Methods

        private void OnControlPanelCloseButton()
        {
            _controlsPanel.SetActive(false);
        }

        #endregion

        #region Settings Panels Methods

        private void OnSettingsPanelCloseButton()
        {
            _settingsPanel.SetActive(false);
        }
        #endregion
    }
}