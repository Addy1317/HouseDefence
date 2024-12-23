#region Summary
/// <summary>
/// Manages the house's health, damage handling, and destruction events in the game.
/// </summary>
#endregion
using TowerDefence.Audio;
using TowerDefence.Services;
using UnityEngine;

namespace TowerDefence.House
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

        private void Update()
        {

        }

        #region House Methods
        public void TakeDamage(int damage)
        {
            _houseCurrentHealth -= damage;
            _houseCurrentHealth = Mathf.Clamp(_houseCurrentHealth, 0, _houesMaxHealth);
            GameService.Instance.audioManager.PlaySFX(SFXType.OnHouseTakingDamageSFX);
            GameService.Instance.vfxManager.ShakeCameraEffect();

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
            GameService.Instance.audioManager.PlaySFX(SFXType.OnEnemyDestroyingHouseSFX);
            Debug.Log("House Destroyed");
            GameService.Instance.eventManager.OnHouseDeathEvent.InvokeEvent();
        }
        #endregion
    }
}
