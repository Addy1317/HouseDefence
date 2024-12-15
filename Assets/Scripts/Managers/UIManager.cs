#region Summary
#endregion
using TMPro;
using UnityEngine;

namespace HouseDefence.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("House Health UI")]
        [SerializeField] private TextMeshProUGUI _houseHealthText;

        [Header("Waves UI")]
        [SerializeField] private TextMeshProUGUI _wavesText;

        [Header("Kills Count UI")]
        [SerializeField] private TextMeshProUGUI _killCountText;

        [Header("Currency UI")]
        [SerializeField] private TextMeshProUGUI _coinText;

        [Header("Tower Selection Panel")]
        [SerializeField] private GameObject _towerSelectionPanel;

        internal void UpdatePlayerHealth(int currentHealth, int maxHealth)
        {
            _houseHealthText.text = $"Health: {currentHealth}/{maxHealth}";
        }

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
