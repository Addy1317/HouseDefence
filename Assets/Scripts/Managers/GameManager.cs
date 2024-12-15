using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Pause Menu UI")]
        [SerializeField] private GameObject _pauseMenuUI;

        private void Update()
        {
            InputsforPauseButton();
        }

        private void InputsforPauseButton()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
            }
        }
    }
}
