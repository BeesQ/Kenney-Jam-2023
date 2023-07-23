using Enums;
using Interfaces;
using System;
using UnityEngine;

namespace EnemyBehaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveTowards : MonoBehaviour
    {
        private Transform moveTo;
        private float keepingDistanceRange = 5.0f;
        private float moveSpeed = 5.0f;
        public EnemyStats stats;
        private Rigidbody2D rb;

        private void Start()
        {
            moveSpeed = stats.MovementSpeed;
            keepingDistanceRange = stats.KeepingDistanceRange;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {

            (float dist, PlayerController closestPlayer) = PlayersManager.Instance.GetClosestPlayerToPos(transform.position);
            moveTo = closestPlayer.transform;
            
            Vector3 diff = (moveTo.position - transform.position).normalized;
            
            if (dist <= keepingDistanceRange)
            {
                rb.velocity = -diff * (stats.MovementSpeed);
                return;
            }

            float deadZoneSize = 0.1f;
            if (dist > keepingDistanceRange + deadZoneSize)
            {
                rb.velocity = diff * (stats.MovementSpeed);
            }
        }
    }
}