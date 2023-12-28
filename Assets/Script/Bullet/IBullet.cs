using UnityEngine;

namespace Bullet
{
    public interface IBullet
    {
        public void UpdateBulletMotion();

        public void OnBulletEnterTrigger(GameObject collidedObject);
    }
}