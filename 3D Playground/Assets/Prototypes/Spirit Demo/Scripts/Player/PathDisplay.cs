using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class PathDisplay : MonoBehaviour
{
    public List<SpiritController> allTargets;
    List<SpiritController> activeTargets;
    List<SpiritController> targets;

    public GameObject targetMarker;
    public LineRenderer targetLine;

    SpiritManager spiritManager;

    [HideInInspector] public NavMeshPath path;
    private void Awake()
    {
        spiritManager = transform.GetComponent<SpiritManager>();

        activeTargets = spiritManager.spiritList;
        path = new NavMeshPath();

        targetLine.startWidth = 0.5f;
        targetLine.endWidth = 0.5f;
        targetLine.positionCount = 0;
    }

    private void Update()
    {
        if (allTargets.Count > 0 || allTargets.Count > 0)
        {
            Vector3 targetPosition = GetClosestEnemy().position;
            NavMesh.CalculatePath(transform.position, targetPosition, 1, path);
            if (path != null)
            {
                targetMarker.SetActive(true);
                targetMarker.transform.position = targetPosition;
                DrawPath();
            }
        }
    }

    private Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSquared = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if (activeTargets.Count > 0) {
            targets = activeTargets;
        } else if (allTargets.Count > 0) {
            targets = allTargets;
        } else {
            return null;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Transform targetTransform;
            if (!targets[i].isFollowing) {
                targetTransform = targets[i].spirit.transform;
            }
            else {
                targetTransform = targets[i].spiritBody.transform;
            }

            Vector3 directionToTarget = targetTransform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSquared)
            {
                closestDistanceSquared = dSqrToTarget;
                bestTarget = targetTransform;
            }
        }
        return bestTarget;
    }

    private void DrawPath()
    {
        targetLine.positionCount = path.corners.Length;
        targetLine.SetPosition(0, new(transform.position.x, transform.position.y - 2, transform.position.z));

        if (path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < path.corners.Length; i++)
        {
            Vector3 pointPosition = new(path.corners[i].x, path.corners[i].y, path.corners[i].z);
            targetLine.SetPosition(i, pointPosition);
        }
    }
}
