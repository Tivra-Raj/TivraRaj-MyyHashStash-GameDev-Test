using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemySO")]
    public class EnemyModel : ScriptableObject
    {
        public int MaxHealth;
        public float HelicopterSpeed;
        public float ParatrooperSpeed;
        public int DamageToInflict;
    }
}