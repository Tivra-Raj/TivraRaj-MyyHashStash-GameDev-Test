using Service;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyService
    {
        private EnemyModel enemyData;
        private EnemyHelicopterPool helicopterPool;
        private EnemyParatrooperPool paratrooperPool;

        private List<EnemyParatrooperController> leftParatroopers = new List<EnemyParatrooperController>();
        private List<EnemyParatrooperController> rightParatroopers = new List<EnemyParatrooperController>();

        public EnemyService(EnemyView enemyHelicopterPrefab, EnemyView enemyParatrooperPrefab, EnemyModel enemyData)
        {
            this.enemyData = enemyData;
            helicopterPool = new EnemyHelicopterPool(enemyHelicopterPrefab, this.enemyData);
            paratrooperPool = new EnemyParatrooperPool(enemyParatrooperPrefab, this.enemyData);
        }

        public void Update(Transform[] spawnPositionAndRotation)
        {
            SpawnHelicopterEnemy(spawnPositionAndRotation);
        }

        private void SpawnHelicopterEnemy(Transform[] spawnPositionAndRotation)
        {
            foreach (var position in spawnPositionAndRotation)
            {
                SpawnEnemyAtPosition(helicopterPool.GetEnemy(), position);
            }
        }

        public void SpawnParatrooperEnemy(Transform spawnPositionAndRotation)
        {
            EnemyParatrooperController paratrooper = (EnemyParatrooperController)paratrooperPool.GetEnemy();
            SpawnEnemyAtPosition(paratrooper, spawnPositionAndRotation);
            paratrooperPool.GetActiveParatroopers().Add(paratrooper);

            AddEachSideSpawnedParatrooperToList(paratrooper);
        }

        private void AddEachSideSpawnedParatrooperToList(EnemyParatrooperController enemyParatrooper)
        {
            if (enemyParatrooper.enemyView.transform.position.x < GameService.Instance.GetPlayerPrefab().transform.position.x)
            {
                leftParatroopers.Add(enemyParatrooper);
            }
            else
            {
                rightParatroopers.Add(enemyParatrooper);
            }
        }

        private void SpawnEnemyAtPosition(EnemyController enemy, Transform spawnPositionAndRotation)
        {
            enemy.Configure(spawnPositionAndRotation.position, spawnPositionAndRotation.rotation);
        }

        public void ReturnEnemyToPool(EnemyController enemyToReturn)
        {
            if (enemyToReturn is EnemyHelicoptorController)
            {
                helicopterPool.ReturnItem(enemyToReturn);
            }
            else if (enemyToReturn is EnemyParatrooperController paratrooper)
            {
                // Remove the paratrooper from the active list before returning it to the pool.
                if (leftParatroopers.Contains(paratrooper))
                {
                    leftParatroopers.Remove(paratrooper);
                }
                else if (rightParatroopers.Contains(paratrooper))
                {
                    rightParatroopers.Remove(paratrooper);
                }

                paratrooperPool.RemoveFromActiveParatroopers(paratrooper);
                paratrooperPool.ReturnItem(paratrooper);
            }
        }

        public List<EnemyParatrooperController> GetLeftParatrooper()
        {
            return leftParatroopers;
        }

        public List<EnemyParatrooperController> GetRightParatrooper()
        {
            return rightParatroopers;
        }
    }
}