using UnityEngine;
using System;

namespace HouseDefence.Events
{
    public class EventController : MonoBehaviour
    {
        public event Action baseEvent;
        public void InvokeEvent() => baseEvent?.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;
    }

    public class EventsController<T> : MonoBehaviour
    {
        public event Action<T> baseEvent;
        public void InvokeEvents(T value) => baseEvent?.Invoke(value); 
        public void AddListeners(Action<T> listener) => baseEvent += listener;
        public void RemoveListeners(Action<T> listener) => baseEvent -= listener;
    }
}
