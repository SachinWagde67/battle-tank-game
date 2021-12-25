using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventService : SingletonGeneric<EventService>
    {
        public event Action OnBulletsFired;
        public event Action OnEnemiesKilled;

        public void invokeOnBulletsFired()
        {
            OnBulletsFired?.Invoke();
        }

        public void invokeOnEnemiesKilled()
        {
            OnEnemiesKilled?.Invoke();
        }
    }
}
