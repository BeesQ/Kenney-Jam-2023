using System;
using UnityEngine;

namespace EnemyBehaviours
{
    public class MoveTowards : MonoBehaviour
    {
        private Transform moveTo;
        public float keepingDistanceRange = 5.0f;
        public float moveSpeed = 5.0f;
        private PlayerController[] players;

        private void Start()
        {
            players = FindObjectsOfType<PlayerController>();
        }

        private void Update()
        {
            float closestDistance = float.MaxValue;
            foreach (var player in players)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    moveTo = player.transform;
                    closestDistance = distance;
                }
            }

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