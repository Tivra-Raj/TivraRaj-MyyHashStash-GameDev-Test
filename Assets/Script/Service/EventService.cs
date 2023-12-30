using Enemy;
using System;
using Utilities;

namespace Events
{
    public class EventService : GenericMonoSingleton<EventService>
    {
        public event Action<EnemyParatrooperController> OnFourParatrooperSpawned;

        public void InvokeOnFourParatrooperSpawned(EnemyParatrooperController enemyParatrooper)
        {
            OnFourParatrooperSpawned?.Invoke(enemyParatrooper);
        }
    }
}