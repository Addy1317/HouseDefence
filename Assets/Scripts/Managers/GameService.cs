using HouseDefence.Audio;
using HouseDefence.Bullet;
using HouseDefence.EnemySpawn;
using HouseDefence.Generic;
using HouseDefence.Grid;
using HouseDefence.Manager;
using HouseDefence.Tower;
using HouseDefence.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Services
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] internal GameManager gameManager;
        [SerializeField] internal AudioManager audioManager;
        [SerializeField] internal BulletManager bulletManager;
        [SerializeField] internal CurrencyManager currencyManager;
        [SerializeField] internal SpawnManager enemySpawnManager;
        [SerializeField] internal TowerManager towerManager;
        [SerializeField] internal GridManager gridManager;
        [SerializeField] internal UIManager uiManager;
        [SerializeField] internal EventManager eventManager;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeServices();
        }

        private void InitializeServices()
        {
            var services = new Dictionary<string, Object>
            {
            { "GameManager", gameManager },
            { "AudioManager", audioManager },
            { "BulletManager", bulletManager },
            { "CurrencyManager", currencyManager },
            { "EnemySpawnManager", enemySpawnManager },
            { "TowerManager", towerManager },
            { "CustomGridManager", gridManager },
            { "UIManager", uiManager },
            { "EventManager", eventManager }
            };

            foreach (var service in services)
            {
                if (service.Value == null)
                {
                    Debug.LogError($"{service.Key} failed to initialize.");
                }
                else
                {
                    Debug.Log($"{service.Key} is initialized.");
                }
            }
        }
    }
}
