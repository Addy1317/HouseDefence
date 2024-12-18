#region Summary
// EventManager serves as a central hub for managing game events in the HouseDefence namespace.
// It provides event controllers for handling:
// - OnHouseDeathEvent: Triggered when the house is destroyed.
// - OnWaveCompletedEvent: Triggered when a wave of enemies is completed.
// - OnEnemyDeathEvent (float): Triggered when an enemy dies, passing a float value (e.g., score).
//
// This class uses the Singleton pattern to ensure only one instance exists throughout the game.
// Events are initialized in the Awake method, and the class persists across scene transitions.
#endregion
using TowerDefence.Events;
using UnityEngine;

namespace TowerDefence
{
    public class EventManager : MonoBehaviour
    {
        public EventController OnHouseDeathEvent {  get; private set; }
        public EventController OnWaveCompletedEvent {  get; private set; }
        public EventsController<float> OnEnemyDeathEvent { get; private set; }

        public EventManager()
        {
            OnHouseDeathEvent = new EventController();
            OnWaveCompletedEvent = new EventController();
            OnEnemyDeathEvent = new EventsController<float>();
        }
    }
}
