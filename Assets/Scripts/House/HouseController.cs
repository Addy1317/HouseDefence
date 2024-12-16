#region Summary
#endregion
using HouseDefence.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.House
{
    public class HouseController : MonoBehaviour
    {
        [Header("House Health")]
        [SerializeField] private int _houesMaxHealth = 100; 
        private int _houseCurrentHealth;

        private void Start()
        {
            _houseCurrentHealth = _houesMaxHealth;
            UpdateHouseUI();
        }

        public void TakeDamage(int damage)
        {
            _houseCurrentHealth -= damage;
            _houseCurrentHealth = Mathf.Clamp(_houseCurrentHealth, 0, _houesMaxHealth);

            UpdateHouseUI();

            if (_houseCurrentHealth <= 0)
            {
                OnHouseDeath();
            }
        }

        private void UpdateHouseUI()
        {
            float healthPercent = (float)_houseCurrentHealth / _houesMaxHealth;
            GameService.Instance.uiManager.UpdateHealthbarUI(_houseCurrentHealth, _houesMaxHealth, healthPercent);
        }

        private void OnHouseDeath()
        {
            Debug.Log("House Destroyed");
            GameService.Instance.eventManager.OnHouseDeathEvent.InvokeEvent();
        }
    }
}
