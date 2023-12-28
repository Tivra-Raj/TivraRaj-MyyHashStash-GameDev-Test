using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerData;

        private float horizontalInput;

        public PlayerController(PlayerView playerView, PlayerModel playerData)
        {
            this.playerView = playerView;
            this.playerView.SetPlayerController(this);

            this.playerData = playerData;
            this.playerData.SetPlayerController(this);
        }

        #region CannonRotation
        public void HandleRotationInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        public void HandleCannonRotation(Transform TurretRotationPoint)
        {
            // Calculate the desired rotation based on input
            float rotation = -horizontalInput * playerData.RotationSpeed * Time.deltaTime;

            // Rotate the pivot around the Z-axis directly
            TurretRotationPoint.Rotate(0f, 0f, rotation);

            // Clamp the rotation between minAngle and maxAngle
            float currentRotation = TurretRotationPoint.localRotation.eulerAngles.z;
            currentRotation = (currentRotation > 180f) ? currentRotation - 360f : currentRotation;
            currentRotation = Mathf.Clamp(currentRotation, playerData.MinRotationAngle, playerData.MaxRotationAngle);

            // Set the final rotation of the pivot point
            TurretRotationPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
        #endregion
    }
}