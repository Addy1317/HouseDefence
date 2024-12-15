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
}
