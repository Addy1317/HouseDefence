#region Summary
#endregion
using HouseDefence.Services;
using UnityEngine;

namespace HouseDefence.House
{
    public class HouseController : MonoBehaviour
    {
        private void OnHouseDeath()
        {
            GameService.Instance.eventManager.OnHouseDeathEvent.InvokeEvent();
        }
    }
}
