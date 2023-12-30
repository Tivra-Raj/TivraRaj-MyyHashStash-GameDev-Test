using Interface;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IDamageable
    {
        private EnemyController enemyController;
        [SerializeField] private Rigidbody2D EnemyRigidbody;
        [SerializeField] private GameObject Parachute;

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

        public Rigidbody2D GetEnemyRigibody()
        {
            if(EnemyRigidbody != null)
                return EnemyRigidbody;
            return null;
        }

        public GameObject GetEnemyParachute() => Parachute;
    }
}