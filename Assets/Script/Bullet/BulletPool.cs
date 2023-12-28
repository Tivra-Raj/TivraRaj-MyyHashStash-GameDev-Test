using Utilities;

namespace Bullet
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletModel bulletData;

        public BulletPool(BulletView bulletPrefab, BulletModel bulletData)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletData = bulletData;
        }

        public BulletController GetBullet() => GetItem();

        protected override BulletController CreateItem() => new BulletController(bulletPrefab, bulletData);
    }
}