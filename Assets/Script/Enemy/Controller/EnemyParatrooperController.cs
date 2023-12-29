using UnityEngine;

namespace Enemy
{
    public class EnemyParatrooperController : EnemyController
    {
        public EnemyParatrooperController(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData)
        {
            enemyView.SetController(this as EnemyController);
        }

        public override void GetUpdate()
        {
            
        }

        public override void GetFixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public override void GetOnTrigger2D(Collider2D other)
        {
            
        }
    }
}