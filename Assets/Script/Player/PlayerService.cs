using Bullet;

namespace Player
{
    public class PlayerService
    {
        private BulletPool bulletPool;
        private PlayerController playerController;

        public PlayerService(PlayerView playerViewPrefab, PlayerModel playerData, BulletView bulletPrefab, BulletModel bulletData)
        {
            bulletPool = new BulletPool(bulletPrefab, bulletData);
            playerController = new PlayerController(playerViewPrefab, playerData, bulletPool);
        }

        public PlayerController GetPlayerController() => playerController;

        public void ReturnBulletToPool(BulletController bulletToReturn) => bulletPool.ReturnItem(bulletToReturn);
    }
}