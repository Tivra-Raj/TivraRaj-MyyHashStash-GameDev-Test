using Service;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyParatrooperController : EnemyController
    {       
        private GameService gameService = GameService.Instance;

        private bool isMoving = false;  // Flag to track whether paratroopers are currently moving.

        public EnemyParatrooperController(EnemyView enemyPrefab, EnemyModel enemyData) : base(enemyPrefab, enemyData)
        {
            enemyView.SetController(this as EnemyController);
        }

        public override void GetUpdate()
        {
            // Check if the count on either side has reached 4, then start moving the paratroopers towards the turret.
            if (gameService.GetEnemyService().GetLeftParatrooper().Count >= 4 && !isMoving)
            {
                enemyView.StartCoroutine(MoveParatroopers(true));
            }

            if (gameService.GetEnemyService().GetRightParatrooper().Count >= 4 && !isMoving)
            {
                enemyView.StartCoroutine(MoveParatroopers(false));
            }
        }

        public override void GetFixedUpdate() { }

        public override void GetOnTrigger2D(Collider2D other) { }

        private IEnumerator MoveParatroopers(bool fromLeft)
        {
            isMoving = true;  // Set the flag to prevent concurrent movements.

            // Get the list of paratroopers on the specified side.
            var paratroopers = fromLeft ? gameService.GetEnemyService().GetLeftParatrooper() : gameService.GetEnemyService().GetRightParatrooper();

            while (paratroopers.Count > 0)
            {
                // Move the first paratrooper in the list towards the turret.
                EnemyParatrooperController currentParatrooper = paratroopers[0];
                float distanceToPlayer = Mathf.Abs(currentParatrooper.enemyView.transform.position.x - gameService.GetPlayerPrefab().transform.position.x);

                HandleParatrooperMovement(fromLeft, currentParatrooper, distanceToPlayer);   

                if (distanceToPlayer < 0.2f)
                {
                    // If the current paratrooper has reached the player, remove it from the list.
                    paratroopers.RemoveAt(0);
                    currentParatrooper.enemyView.GetEnemyRigibody().velocity = Vector2.zero;
                }

                yield return null;
            }

            isMoving = false;  // Reset the flag when all paratroopers are moved.
        }

        public void HandleParatrooperMovement(bool fromLeft, EnemyParatrooperController currentParatrooper, float distanceToPlayer)
        {
            Vector3 movement = fromLeft ? Vector3.right : Vector3.left;
            movement *= enemyData.ParatrooperSpeed;   

            if (distanceToPlayer > 0.2f)
            {
                currentParatrooper.enemyView.GetEnemyRigibody().velocity = movement;
            }
        }
    }
}