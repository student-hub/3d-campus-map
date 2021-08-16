using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public Camera camera;
    NavMeshAgent _navMeshAgent;
    LineRenderer myLineRenderer;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        myLineRenderer = this.GetComponent<LineRenderer>();

        myLineRenderer.startWidth = 0.15f;
        myLineRenderer.endWidth = 0.15f;
        myLineRenderer.positionCount = 0;
    }

    private void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            ClickToMove();
        }*/
        if (_navMeshAgent.hasPath)
            DrawPath();
    }

    void ClickToMove()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
            SetDestination(hit.point);
        Debug.Log(hit.point);
    }

    public void SetDestination(Vector3 target)
    {
        _navMeshAgent.SetDestination(target);
        myLineRenderer.enabled = true;
        _navMeshAgent.Stop(true);
    }

    public void DeleteDestination()
    {
        _navMeshAgent.ResetPath();
        myLineRenderer.enabled = false;
    }

    void DrawPath()
    {
        //Draws the path the player will take to reach its destination
        myLineRenderer.positionCount = _navMeshAgent.path.corners.Length;
        myLineRenderer.SetPosition(0, transform.position);

        if (_navMeshAgent.path.corners.Length < 3)
        {
            _navMeshAgent.ResetPath();
            myLineRenderer.enabled = false;
        }

        for (int i = 1; i < _navMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(_navMeshAgent.path.corners[i].x, _navMeshAgent.path.corners[i].y, _navMeshAgent.path.corners[i].z);
            myLineRenderer.SetPosition(i, pointPosition);
        }
    }
}
