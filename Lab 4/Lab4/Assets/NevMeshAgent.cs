using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NevMeshAgent : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEngine.AI.NavMeshAgent agent;

    public Camera cam;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

        }
    }
}
