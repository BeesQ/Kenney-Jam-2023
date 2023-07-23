using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBetweenManager : Singleton<PointBetweenManager>
{
    [SerializeField] List<Transform> playersToCalculate;
    [SerializeField] Transform objectBetween;

    void Update()
    {
        if (playersToCalculate.Count <= 0) { return; }
        
        Vector3 pointBetweenObjects = GetPointBetweenPlayers();
        objectBetween.transform.position = pointBetweenObjects;
        //Debug.Log(GetLongestDistanceBetweenPlayers(playersToCalculate));
    }

    public Vector3 GetPointBetweenPlayers()
    {
        return GetPointBetweenObjects(playersToCalculate);
    }

    public Vector3 GetPointBetweenObjects(List<Transform> objects)
    {
        Vector3 sumPositions = Vector3.zero;

        foreach (Transform obj in objects)
        {
            sumPositions += obj.position;
        }

        Vector3 averagePosition = sumPositions / objects.Count;

        return averagePosition;
    }



    public float GetLongestDistanceBetweenPlayers()
    {
        return GetLongestDistanceBetweenObjects(playersToCalculate);
    }

    public float GetLongestDistanceBetweenObjects(List<Transform> objects)
    {
        float longestDistance = 0f;
        int count = objects.Count;

        for (int i = 0; i < count - 1; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                float distance = Vector3.Distance(objects[i].position, objects[j].position);
                if (distance > longestDistance)
                {
                    longestDistance = distance;
                }
            }
        }

        return longestDistance;
    }
}
