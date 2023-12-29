using UnityEngine;

namespace Enemy
{
    public class EnemyService
    {
        private EnemyModel enemyData;
        private EnemyHelicopterPool helicopterPool;
        private EnemyParatrooperPool paratrooperPool;

        public EnemyService(EnemyView enemyPrefabType1, EnemyView enemyPrefabType2, EnemyModel enemyData)
        {
            this.enemyData = enemyData;
            this.helicopterPool = new EnemyHelicopterPool(enemyPrefabType1, enemyData);
            this.paratrooperPool = new EnemyParatrooperPool(enemyPrefabType2, enemyData);
        }

        public void Update(Transform[] spawnPositionsForType1)
        {
            SpawnEnemiesOfType1(spawnPositionsForType1);
        }

        private void SpawnEnemiesOfType1(Transform[] spawnPositions)
        {
            foreach (var position in spawnPositions)
            {
                SpawnEnemyAtPosition(helicopterPool.GetEnemy(), position);
            }
        }

        public void SpawnEnemiesOfType2(Transform spawnPositions)
        {
            SpawnEnemyAtPosition(paratrooperPool.GetEnemy(), spawnPositions);
        }

        private void SpawnEnemyAtPosition(EnemyController enemy, Transform spawnPositionAndRotation)
        {
            enemy.Configure(spawnPositionAndRotation.position, spawnPositionAndRotation.rotation);
        }

        public void ReturnEnemyToPool(EnemyController enemyToReturn)
        {
            if (enemyToReturn is EnemyHelicoptorController) // Assuming EnemyType1 and EnemyType2 are the two derived classes
            {
                helicopterPool.ReturnItem(enemyToReturn);
            }
            else if (enemyToReturn is EnemyParatrooperController)
            {
                paratrooperPool.ReturnItem(enemyToReturn);
            }
            // Add additional conditions for more enemy types if needed
        }
    }
}