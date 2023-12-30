using Events;
using Service;
using UnityEngine;

namespace Enemy
{
    public class EnemyParatrooperController : EnemyController
    {
        private bool isMoving = false;

        public EnemyParatrooperController(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData)
        {
            enemyView.SetController(this as EnemyController);
            EventService.Instance.OnFourParatrooperSpawned += HandleParatrooperMovement;
        }

        ~EnemyParatrooperController()
        {
            EventService.Instance.OnFourParatrooperSpawned -= HandleParatrooperMovement;
        }

        public override void GetUpdate()
        {
            MoveParatrooper();
        }

        public override void GetFixedUpdate() { }

        public override void GetOnTrigger2D(Collider2D other) 
        {
            ActivateAndDeactivateParachute(other);
        }

        public void HandleParatrooperMovement(EnemyParatrooperController currentParatrooper)
        {
            isMoving = true;
        }

        private void MoveParatrooper()
        {
            if (isMoving)
            {
                Vector3 movement = (GameService.Instance.GetPlayerPrefab().transform.position - enemyView.transform.position).normalized;
                enemyView.GetEnemyRigibody().velocity = movement * enemyData.ParatrooperSpeed;
            }
            isMoving = false;
        }

        public void ActivateAndDeactivateParachute(Collider2D other)
        {
            if(other.CompareTag("OpenParachute"))
            {
                enemyView.GetEnemyParachute().gameObject.SetActive(true);
            }

            if(other.CompareTag("Ground"))
            {
                enemyView.GetEnemyParachute().gameObject.SetActive(false);
            }
        }
    }
}