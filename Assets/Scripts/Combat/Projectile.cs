using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Health target;
        [SerializeField] float speed = 5;
        Collider targetCol;
        float damage;

        private void Start()
        {
            targetCol = target.GetComponent<Collider>();
        }

        private void Update()
        {
            if (target == null) return;
            Vector3 transition = Vector3.forward * speed * Time.deltaTime;
            transform.Translate(transition);
            transform.LookAt(GetAimLocation());
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            if (targetCol != null)
            {
                return target.transform.position + Vector3.up * targetCol.bounds.size.y / 2;
            }
            return target.transform.position + Vector3.up;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") return;
            Health target = other.gameObject.GetComponent<Health>();
            if (target == this.target)
            {
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    } 
}
