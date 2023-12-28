using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; private set; }

        [SerializeField] private Transform TurretBasePoint;

        private void Update()
        {
            PlayerController.HandleRotationInput();
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