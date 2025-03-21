﻿using UnityEngine;

[RequireComponent(typeof(Transform))]
public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _waypointsContainer;

    private Transform[] _waypoints;
    private int _currentWaypointIndex;

    private void Start()
    {
        InitializeWaypoints();
        OrientToFirstWaypoint();
    }

    private void InitializeWaypoints()
    {
        _waypoints = new Transform[_waypointsContainer.childCount];

        for (int i = 0; i < _waypointsContainer.childCount; i++)
        {
            _waypoints[i] = _waypointsContainer.GetChild(i);
        }
    }

    private void OrientToFirstWaypoint()
    {
        if (_waypoints.Length > 0)
        {
            transform.forward = _waypoints[0].position - transform.position;
        }
    }

    private void Update()
    {
        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentWaypoint.position,
            _moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.01f)
        {
            SetNextWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        transform.forward = _waypoints[_currentWaypointIndex].position - transform.position;
    }
}