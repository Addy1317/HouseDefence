#region Summary
// The GameOverUI class handles the display of the game over screen, including buttons for replaying the game, going back to the main menu, and quitting the game.
// It also displays the total number of kills achieved during the session.
// The class manages button clicks to navigate between scenes and updates the UI to show the total kills count from the UIManager.
#endregion
using TowerDefence.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence.UI
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

        }

        private void OnDisable()
        {
            _replayButton.onClick.RemoveListener(OnRelayButton);
            _homeButton.onClick.RemoveListener(OnHomeButton);
            _quitButton.onClick.RemoveListener(OnQuitButton);
        }

        private void Start()
        {
            int _totalKills = GameService.Instance.uiManager.GetKillCount();
            UpdateKillCountDisplay(_totalKills);

            GameService.Instance.vfxManager.AddHoverEffect(_replayButton);
            GameService.Instance.vfxManager.AddHoverEffect(_homeButton);
            GameService.Instance.vfxManager.AddHoverEffect(_quitButton);
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
