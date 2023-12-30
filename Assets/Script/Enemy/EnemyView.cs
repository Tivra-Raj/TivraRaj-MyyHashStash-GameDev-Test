using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController enemyController;
        public Rigidbody2D EnemyRigidbody;

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

        public bool isOnLeftSide { get; private set; }

        // Add a method to set the side during initialization
        public void SetSide(bool leftSide)
        {
            isOnLeftSide = leftSide;
        }

        public Rigidbody2D GetEnemyRigibody()
        {
            if(EnemyRigidbody != null)
                return EnemyRigidbody;
            return null;
        }
    }
}