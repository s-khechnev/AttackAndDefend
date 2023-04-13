using System.Collections;
using Attackers;
using Unity.VisualScripting;
using UnityEngine;

namespace Defender.Towers
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(1, 50)] private int _speed;

        private int _damage;
        private Attacker _target;

        /// <summary>
        /// Launch the bullet to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public void Launch(Attacker target, int damage)
        {
            _target = target;
            _damage = damage;
            StartCoroutine(LaunchCoroutine(target));
        }

        private IEnumerator LaunchCoroutine(Attacker target)
        {
            while (!target.IsDestroyed())
            {
                var step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                yield return null;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Attacker attacker))
            {
                if (attacker != _target)
                    return;
                    
                attacker.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}