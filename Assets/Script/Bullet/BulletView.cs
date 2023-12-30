using UnityEngine;

namespace Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;

        public void SetController(BulletController bulletController) => this.bulletController = bulletController;

        private void Update()
        {
            bulletController?.UpdateBulletMotion();
            bulletController?.CheckIfBulletGoingOffScreen();
        }

        private void OnTriggerEnter2D(Collider2D collision) => bulletController?.OnBulletEnterTrigger(collision.gameObject);
    }
}