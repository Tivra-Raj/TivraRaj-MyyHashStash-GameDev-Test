using System.Collections.Generic;

namespace Enemy
{
    public class EnemyParatrooperPool : EnemyPool
    {
        private List<EnemyParatrooperController> activeParatroopers = new List<EnemyParatrooperController>();

        public EnemyParatrooperPool(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData) { }

        protected override EnemyController CreateItem()
        {
            var paratrooper = new EnemyParatrooperController(enemyPrefab, enemyData);
            return paratrooper;
        }

        public List<EnemyParatrooperController> GetActiveParatroopers()
        {
            return activeParatroopers;
        }

        public void RemoveFromActiveParatroopers(EnemyParatrooperController paratrooper)
        {
            activeParatroopers.Remove(paratrooper);
        }
    }
}