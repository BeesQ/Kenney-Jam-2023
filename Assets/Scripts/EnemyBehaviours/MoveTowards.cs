using Enums;
using Interfaces;
using System;
using UnityEngine;

namespace EnemyBehaviours
{
    public class MoveTowards : MonoBehaviour
    {
        public Transform moveTo;
        public float keepingDistanceRange = 5.0f;
        public float moveSpeed = 5.0f;

        private void Update()
        {

            if (Vector2.Distance(transform.position, moveTo.position) <= keepingDistanceRange)
            {
                Vector3 diff = (moveTo.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, transform.position - diff, Time.deltaTime * moveSpeed);
                return;
            }

            float deadZoneSize = 0.1f;
            if (Vector2.Distance(transform.position, moveTo.position) > keepingDistanceRange + deadZoneSize)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, moveTo.position, Time.deltaTime * moveSpeed);
            }
        }
    }
}