#region Summary
/// <summary>
/// Manages the Main Menu UI, including navigation between panels and game start functionality.
/// </summary>
#endregion
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TowerDefence.Services;
using TowerDefence.VFX;
using TowerDefence.Audio;

namespace TowerDefence.UI
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

        [Header("VFX/Audio Manager")]
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private VFXManager _vfxManager;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButton);
            _controlsButton.onClick.AddListener(OnControlsButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _quitButton.onClick.AddListener(OnQuitButton);

            _controlsPanelCloseButton.onClick.AddListener(OnControlPanelCloseButton);

            _settingsPanelCloseButton.onClick.AddListener(OnSettingsPanelCloseButton);

            _vfxManager.AddHoverEffect(_playButton);
            _vfxManager.AddHoverEffect(_controlsButton);
            _vfxManager.AddHoverEffect(_settingsButton);
            _vfxManager.AddHoverEffect(_quitButton);
            _vfxManager.AddHoverEffect(_controlsPanelCloseButton);
            _vfxManager.AddHoverEffect(_settingsPanelCloseButton);
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
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            SceneManager.LoadScene("MainGame");
        }

        private void OnControlsButton()
        {
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            _controlsPanel.SetActive(true);
        }

        private void OnSettingsButton()
        {
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            _settingsPanel.SetActive(true);
        }

        private void OnQuitButton()
        {
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            Application.Quit();
        }

        #endregion

        #region Control Panels Methods

        private void OnControlPanelCloseButton()
        {
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            _controlsPanel.SetActive(false);
        }

        #endregion

        #region Settings Panels Methods

        private void OnSettingsPanelCloseButton()
        {
            _audioManager.PlaySFX(SFXType.OnButtonClickSFX);
            _settingsPanel.SetActive(false);
        }
        #endregion
    }
}