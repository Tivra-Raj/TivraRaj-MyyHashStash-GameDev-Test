using Utilities;

namespace Enemy
{
    public abstract class EnemyPool : GenericObjectPool<EnemyController>
    {
        protected EnemyView enemyPrefab;
        protected EnemyModel enemyData;

        public EnemyPool(EnemyView enemyPrefab, EnemyModel enemyData)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
        }

        public EnemyController GetEnemy() => GetItem();
    }
}