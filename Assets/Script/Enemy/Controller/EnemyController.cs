using Interface;
using Player;
using Service;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyController : IDamageable
    {
        // Dependencies:
        protected EnemyView enemyView;
        protected EnemyModel enemyData;

        // Variables:
        private int currentHealth;

        public EnemyController(EnemyView enemyPrefab, EnemyModel enemyData)
        {
            enemyView = Object.Instantiate(enemyPrefab);
            this.enemyData = enemyData;
        }

        public abstract void GetUpdate();

        public abstract void GetFixedUpdate();

        public abstract void GetOnTrigger2D(Collider2D other);

        public void Configure(Vector3 positionToSet, Quaternion rotation)
        {
            enemyView.transform.SetPositionAndRotation(positionToSet, rotation);
            currentHealth = enemyData.MaxHealth;
            enemyView.gameObject.SetActive(true);
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
                EnemyDestroyed();
        }

        public void OnEnemyCollided(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<PlayerView>() != null)
            {
                GameService.Instance.GetPlayerService().GetPlayerController().TakeDamage(enemyData.DamageToInflict);
                EnemyDestroyed();
            }
        }

        private void EnemyDestroyed()
        {
            enemyView.gameObject.SetActive(false);
            GameService.Instance.GetEnemyService().ReturnEnemyToPool(this);
        }
    }
}