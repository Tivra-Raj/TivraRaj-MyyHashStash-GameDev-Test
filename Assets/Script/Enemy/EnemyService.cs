using Events;
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

        private bool canSpawn;
        private bool paratrooperReachedLeftSide;
        private bool paratrooperReachedRightSide;
        private bool isMovingParatroopers;
        private int indexOfEnemyToMove;

        public EnemyService(EnemyView enemyHelicopterPrefab, EnemyView enemyParatrooperPrefab, EnemyModel enemyData)
        {
            this.enemyData = enemyData;
            helicopterPool = new EnemyHelicopterPool(enemyHelicopterPrefab, this.enemyData);
            paratrooperPool = new EnemyParatrooperPool(enemyParatrooperPrefab, this.enemyData);
            canSpawn = true;
        }

        public void Spawn(Transform[] spawnPositionAndRotation)
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
            if (!canSpawn)
                return;

            EnemyParatrooperController paratrooper = (EnemyParatrooperController)paratrooperPool.GetEnemy();
            SpawnEnemyAtPosition(paratrooper, spawnPositionAndRotation);
            paratrooperPool.GetActiveParatroopers().Add(paratrooper);
            AddEachSideSpawnedParatrooperToList(paratrooper);
            UpdateCanSpawn();
        }

        private void UpdateCanSpawn()
        {
            if (leftParatroopers.Count >= 4 || rightParatroopers.Count >= 4)
            {
                canSpawn = false;
                return;
            }
                canSpawn = true;
        }

        private void AddEachSideSpawnedParatrooperToList(EnemyParatrooperController enemyParatrooper)
        {
            if (enemyParatrooper.enemyView.transform.position.x < GameService.Instance.GetPlayerPrefab().transform.position.x)
            {
                leftParatroopers.Add(enemyParatrooper);

                if (leftParatroopers.Count >= 4)
                    paratrooperReachedLeftSide = true;
                Debug.Log("all troop reached left side : " + paratrooperReachedLeftSide);
            }
            else
            {
                rightParatroopers.Add(enemyParatrooper);

                if (rightParatroopers.Count >= 4)
                    paratrooperReachedRightSide = true;
                Debug.Log("all troop reached right side : " + paratrooperReachedRightSide);
            }
           
        }
        
        public void MoveParatroopersTowardsTurret()
        {
            if (!isMovingParatroopers)
            {
                isMovingParatroopers = true;

                if (paratrooperReachedLeftSide && indexOfEnemyToMove < leftParatroopers.Count)
                {
                    EventService.Instance.InvokeOnFourParatrooperSpawned(leftParatroopers[indexOfEnemyToMove]);
                }

                if (paratrooperReachedRightSide && indexOfEnemyToMove < rightParatroopers.Count)
                {
                    EventService.Instance.InvokeOnFourParatrooperSpawned(rightParatroopers[indexOfEnemyToMove]);
                }

                indexOfEnemyToMove++;
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
    }
}