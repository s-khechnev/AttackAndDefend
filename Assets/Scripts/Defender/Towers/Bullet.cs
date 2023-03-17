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

        public void Launch(Attacker attacker, int damage)
        {
            _damage = damage;
            StartCoroutine(LaunchCoroutine(attacker));
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
                attacker.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}