using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; private set; }

        [SerializeField] private Transform TurretBasePoint;
        [SerializeField] private Transform cannonFireLocation;

        private void Update()
        {
            PlayerController.HandleRotationInput();
            PlayerController.HandleShooting(cannonFireLocation);
        }

        private void FixedUpdate()
        {
            PlayerController.HandleCannonRotation(TurretBasePoint);
        }

        public void SetPlayerController(PlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}