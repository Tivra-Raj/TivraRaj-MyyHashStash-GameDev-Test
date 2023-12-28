using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/BulletSO")]
    public class BulletModel : ScriptableObject
    {
        public float speed;
        public int damage;
    }
}