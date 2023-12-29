using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyParatrooperPool : EnemyPool
    {
        public EnemyParatrooperPool(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData) { }

        protected override EnemyController CreateItem()
        {
            return new EnemyParatrooperController(enemyPrefab, enemyData);
        }
    }
}