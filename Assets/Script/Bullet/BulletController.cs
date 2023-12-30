using Interface;
using Service;
using UnityEngine;

namespace Bullet
{
    public class BulletController : IBullet 
    {
        private BulletView bulletView;
        private BulletModel bulletData;

        public BulletController(BulletView bulletViewPrefab, BulletModel bulletData)
        {
            bulletView = Object.Instantiate(bulletViewPrefab);
            bulletView.SetController(this);
            this.bulletData = bulletData;
        }

        public void ConfigureBullet(Transform spawnTransform)
        {
            bulletView.gameObject.SetActive(true);
            bulletView.transform.position = spawnTransform.position;
            bulletView.transform.rotation = spawnTransform.rotation;
        }

        public void UpdateBulletMotion() => bulletView.transform.Translate(Vector2.up * Time.deltaTime * bulletData.speed);

        public void OnBulletEnterTrigger(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<IDamageable>() != null)
            {
                collidedGameObject.GetComponent<IDamageable>().TakeDamage(bulletData.damage);
                DestroyBullet();
            }
        }

        public void CheckIfBulletGoingOffScreen()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(bulletView.transform.position);
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            bulletView.gameObject.SetActive(false);
            GameService.Instance.GetPlayerService().ReturnBulletToPool(this);
        }
    }
}