#region Summary
// TowerController is a subclass of TowerBase responsible for implementing tower-specific behaviors and health UI updates.
// It overrides the UpdateTowerHealthBar method to handle the visual representation of the tower's health using a UI Slider.
// The health bar will be updated to reflect the tower's current health whenever the tower takes damage.
#endregion
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Tower
{
    public class TowerController : TowerBase
    {
        [SerializeField] private Slider _towerHealthBar;
        protected override void UpdateTowerHealthBar()
        {
           
        }
    }
}
