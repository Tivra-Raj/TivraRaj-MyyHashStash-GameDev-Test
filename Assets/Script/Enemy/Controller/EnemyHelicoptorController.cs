using Service;
using UnityEngine;

namespace Enemy
{
    public class EnemyHelicoptorController : EnemyController
    {
        public EnemyHelicoptorController(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData) 
        {
            enemyView.SetController(this as EnemyController);
        }

        public override void GetUpdate()
        {
            
        }

        public override void GetFixedUpdate()
        {
            HelicopterMovement();
        }

        public override void GetOnTrigger2D(Collider2D other)
        {
            OnCollideWithOtherobject(other);
        }

        private void HelicopterMovement()
        {
            enemyView.transform.Translate(Vector3.right * enemyData.Speed * Time.deltaTime);
        }

        private void OnCollideWithOtherobject(Collider2D other)
        {
            if (other.CompareTag("ParatrooperSpawner"))
            {
                int chance = Random.Range(0, 5);
                if (chance == 0)
                {
                    GameService.Instance.GetEnemyService().SpawnEnemiesOfType2(other.transform);
                }
            }
        }
    }
}