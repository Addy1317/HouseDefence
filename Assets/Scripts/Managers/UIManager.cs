#region Summary
// UIManager is responsible for managing and updating the game's UI elements, including health, wave count, kills, currency, and tower selection.
// It updates the UI when certain events happen, such as enemy deaths or health changes.
#endregion
using HouseDefence.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("House Health UI")]
        [SerializeField] private TextMeshProUGUI _houseHealthText;
        [SerializeField] private Slider _houseHealthBarSlider;
        [SerializeField] private Image _houseHealthBarFill;

        [Header("Health Bar Colors")]
        [SerializeField] private Color _healthyColor = Color.green;
        [SerializeField] private Color _midHealthColor = Color.yellow;
        [SerializeField] private Color _criticalHealthColor = Color.red;

        [Header("Waves UI")]
        [SerializeField] private TextMeshProUGUI _wavesText;

        [Header("Kills Count UI")]
        [SerializeField] private TextMeshProUGUI _killCountText;

        [Header("Currency UI")]
        [SerializeField] private TextMeshProUGUI _coinText;

        [Header("Tower Selection Panel")]
        [SerializeField] private GameObject _towerSelectionPanel;

        private int _killCount = 0;
        private int _coinCount = 0;

        private void OnEnable()
        {
            GameService.Instance.eventManager.OnEnemyDeathEvent.AddListeners(OnEnemyDeath);
        }

        private void OnDisable()
        {
            GameService.Instance.eventManager.OnEnemyDeathEvent.RemoveListeners(OnEnemyDeath);

        }
        #region House UI Methods
        internal void UpdateHealthbarUI(int currentHealth, int maxHealth, float healthPercentage)
        {
            _houseHealthText.text = $"Health: {currentHealth}/{maxHealth}";

            _houseHealthBarSlider.value = healthPercentage;

            if (healthPercentage > 0.5f)
            {
                _houseHealthBarFill.color = _healthyColor;
            }
            else if (healthPercentage > 0.2f)
            {
                _houseHealthBarFill.color = _midHealthColor;
            }
            else
            {
                _houseHealthBarFill.color = _criticalHealthColor;
            }
        }
        #endregion

        #region Enemy Death Methods
        public int GetKillCount()
        {
            return _killCount;
        }

        private void OnEnemyDeath(float goldReward)
        {
            _killCount++;
            _coinCount += Mathf.FloorToInt(goldReward);
            UpdateKillCount(_killCount);
            UpdateCurrency(_coinCount);
        }

        internal void UpdateKillCount(int kills)
        {
            _killCount = kills;
            _killCountText.text = $"Kills: {kills}";
        }

        internal void UpdateCurrency(int coins)
        {
            _coinText.text = $"Coins: {coins}";
        }
        #endregion

        internal void ActivateTowerSelectionPanel()
        {
            _towerSelectionPanel.SetActive(true);
        }

        internal void UpdateWaveCount(int currentWave)
        {
            _wavesText.text = $"Wave: {currentWave}";
        }
    }
}
