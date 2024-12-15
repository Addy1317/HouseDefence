#region Summary
#endregion
using UnityEngine;

namespace HouseDefence.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Pause Menu UI")]
        [SerializeField] private GameObject _pauseMenuUI;


        private void Update()
        {
            InputsforPauseButton();   
        }

        private void InputsforPauseButton()
        {
           if( Input.GetKeyDown(KeyCode.P))
           {
                _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
           }
        }
    }
}
