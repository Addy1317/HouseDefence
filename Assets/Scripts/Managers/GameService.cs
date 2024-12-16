using HouseDefence.Audio;
using HouseDefence.EnemySpawn;
using HouseDefence.Generic;
using HouseDefence.GridLayout;
using HouseDefence.Manager;
using HouseDefence.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Services
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] internal GameManager gameManager;
        [SerializeField] internal AudioManager audioManager;
        [SerializeField] internal CurrencyManager currencyManager;
        [SerializeField] internal EnemySpawnManager enemySpawnManager;
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
            { "CurrencyManager", currencyManager },
            { "EnemySpawnManager", enemySpawnManager },
            { "GridManager", gridManager },
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
