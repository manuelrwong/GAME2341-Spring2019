using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private string _name;
    public float speed = 1;

    public int myInt;
    public float myFloat;
    public double myDouble;
    public bool myBool;
    public string myString;
    public char myChar;
    public byte myByte;
    public GameObject particle;

    public Rigidbody rb;
    public AudioListener al;

    void Awake()
    {
        _name = gameObject.name;
        Debug.Log("The " + _name + " is awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>() ;
        al = GetComponent<AudioListener>();
        Debug.Log(_name + ": start");
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        transform.Translate(move * speed);
        //Debug.Log(_name + " Update");

        //if(rb != null)
        //{
        //    Debug.Log(rb.mass);
        //}

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(name + " Collision Enter with " + col.gameObject.name);
        switch(col.gameObject.tag)
        {
            case "Enemy":
                col.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case "Player":
                col.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case "Bomb":
                GameObject p = Instantiate(particle, transform.position, Quaternion.identity );
                Destroy(p, 1);
                break;
        }
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log(name + " Collision Exit with " + col.gameObject.name);
    }

    void OnCollisionStay(Collision col)
    {
        Debug.Log(name + " Collision Stay with " + col.gameObject.name);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(name + " Trigger Enter with " + col.gameObject.name);
        col.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log(name + " Trigger Stay with " + col.gameObject.name);
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            Debug.Log("HIIIIIII");
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log(name + " Trigger Exit with " + col.gameObject.name);
        col.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    

    private void myMethod()
    {

    }

    public float getSpeed()
    {
        return speed;
    }
}
