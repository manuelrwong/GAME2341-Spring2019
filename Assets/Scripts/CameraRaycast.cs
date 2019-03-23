using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraRaycast : MonoBehaviour
{
    private Camera camera;
    public GameObject particle;
    public GameObject selected;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                if( selected )
                {
                    selected.GetComponent<MeshRenderer>().material.color = Color.white;
                }
                selected = hit.collider.gameObject;
                switch (hit.collider.gameObject.tag)
                {
                    case "Enemy":
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    case "Player":
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case "Bomb":
                        GameObject p = Instantiate(particle, hit.collider.gameObject.transform.position, Quaternion.identity);
                        Destroy(p, 1);
                        break;
                    case "Floor":
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        selected = null;
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Floor")
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 1);

                    if (selected)
                    {
                        selected.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        //selected.transform.position = hit.point + (new Vector3(0, .5f, 0));
                    }
                }
            }
        }
    }
}
