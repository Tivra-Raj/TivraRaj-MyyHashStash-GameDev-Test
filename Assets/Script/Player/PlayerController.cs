using Bullet;
using Service;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerData;
        private BulletPool bulletPool;

        private float horizontalInput;

        public PlayerController(PlayerView playerView, PlayerModel playerData, BulletPool bulletPool)
        {
            this.playerView = playerView;
            this.playerView.SetPlayerController(this);

            this.playerData = playerData;
            this.playerData.SetPlayerController(this);

            this.bulletPool = bulletPool;
        }

        #region CannonRotation
        public void HandleRotationInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        public void HandleCannonRotation(Transform TurretRotationPoint)
        {
            float rotation = -horizontalInput * playerData.RotationSpeed * Time.deltaTime;

            TurretRotationPoint.Rotate(0f, 0f, rotation);

            float currentRotation = TurretRotationPoint.localRotation.eulerAngles.z;
            currentRotation = (currentRotation > 180f) ? currentRotation - 360f : currentRotation;
            currentRotation = Mathf.Clamp(currentRotation, playerData.MinRotationAngle, playerData.MaxRotationAngle);

            TurretRotationPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
        #endregion

        public void HandleShooting(Transform fireLocation)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FireWeapon(fireLocation);
        }

        // Firing Weapons:
        private void FireWeapon(Transform fireLocation)
        {
            FireBulletAtPosition(fireLocation);
        }

        private void FireBulletAtPosition(Transform fireLocation)
        {
            BulletController bulletToFire = bulletPool.GetBullet();
            bulletToFire.ConfigureBullet(fireLocation);
        }
    }
}