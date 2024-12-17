using HouseDefence.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HouseDefence.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("GameOver Panel")]
        [SerializeField] private GameObject _gameOverPanel;

        [Header("GameOver UI Buttons")]
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _quitButton;

        [Header("Kills Count")]
        [SerializeField] private TextMeshProUGUI _totalKillsCountText;
       

        private void OnEnable()
        {
            _replayButton.onClick.AddListener(OnRelayButton);
            _homeButton.onClick.AddListener(OnHomeButton);
            _quitButton.onClick.AddListener(OnQuitButton);

            int _totalKills = GameService.Instance.uiManager.GetKillCount();
            UpdateKillCountDisplay(_totalKills);
        }

        private void OnDisable()
        {
            _replayButton.onClick.RemoveListener(OnRelayButton);
            _homeButton.onClick.RemoveListener(OnHomeButton);
            _quitButton.onClick.RemoveListener(OnQuitButton);
        }

        #region GameOver Buttons Methods
        private void OnRelayButton()
        {
            _gameOverPanel.SetActive(false);
            SceneManager.LoadScene("MainGame");
        }

        private void OnHomeButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnQuitButton()
        {
            Application.Quit();
        }

        #endregion

        #region Total Kills Count Method
        private void UpdateKillCountDisplay(int kills)
        {
            _totalKillsCountText.text = $"Total Kills: {kills}";
        }
        #endregion
    }
}
