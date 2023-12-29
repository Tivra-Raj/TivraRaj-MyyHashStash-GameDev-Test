using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController;

        public void SetController(EnemyController enemyController) => this.enemyController = enemyController;

        private void Update()
        {
            enemyController.GetUpdate();
        }

        private void FixedUpdate()
        {
            enemyController.GetFixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            enemyController.GetOnTrigger2D(collision);
            enemyController?.OnEnemyCollided(collision.gameObject);
        }
        

        public void TakeDamage(int damageToTake) => enemyController.TakeDamage(damageToTake);
    }
}