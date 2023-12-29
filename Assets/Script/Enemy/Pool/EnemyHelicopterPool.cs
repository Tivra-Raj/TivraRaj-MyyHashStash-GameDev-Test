namespace Enemy
{
    public class EnemyHelicopterPool : EnemyPool
    {
        public EnemyHelicopterPool(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData) { }

        protected override EnemyController CreateItem()
        {
            return new EnemyHelicoptorController(enemyPrefab, enemyData);
        }
    }
}