#region Summary
#endregion
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

        internal void UpdateWaveCount(int currentWave)
        {
            _wavesText.text = $"Wave: {currentWave}";
        }

        internal void UpdateKillCount(int kills)
        {
            _killCountText.text = $"Kills: {kills}";
        }

        internal void UpdateCurrency(int coins)
        {
            _coinText.text = $"Coins: {coins}";
        }

        internal void ActivateTowerSelectionPanel()
        {
            _towerSelectionPanel.SetActive(true);
        }
    }
}
