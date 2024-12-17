using HouseDefence.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence
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
