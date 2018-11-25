using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;


// Build and update a localized navmesh from the sources marked by NavMeshSourceTag
//[DefaultExecutionOrder(-102)]
public class Door : DynamicFieldObject
{

    bool opened;

    [SerializeField]
    private bool locked; //true closed, false opened

    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
            doorway.GetComponent<NavMeshModifier>().overrideArea = value;
            navMeshSurface.BuildNavMesh();
        }
    }
    public bool canBeLockPicked;
    Quaternion newRot;
    public GameObject door02;
    public AudioSource openingAudio;
    public AudioSource closingingAudio;
    private float baseRotation;
    public GameObject doorway;
    private NavMeshSurface navMeshSurface;
    
    [Range(10, 20)]
    public int keyID;

    // Use this for initialization
    void Start()
    {
        navMeshSurface = GameObject.FindObjectOfType<NavMeshSurface>();
        opened = false;
        baseRotation = this.transform.parent.eulerAngles.y;
        doorway.GetComponent<NavMeshModifier>().area = keyID;
        navMeshSurface.BuildNavMesh();
    }

    public override void Interaction(bool IsManualyOperated)
    {
        if (IsManualyOperated)
        {
            if (opened == false && !Locked)
            {
                opened = true;
                openingAudio.Play();
            }
            else
            {
                closingingAudio.Play();
                opened = false;
            }
        }
        else
        {
            opened = !opened;
            if (opened)
                openingAudio.Play();
            else
                closingingAudio.Play();

        }

        //if (opened) {
        //	door02.GetComponent<Collider> ().isTrigger = true;
        //} else {
        //	door02.GetComponent<Collider> ().isTrigger = false;
        //}

    }

    private void Update()
    {
        if (!Locked)
            doorway.isStatic = false;
        if (opened)
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, baseRotation + 90.0f, 0.0f), Time.deltaTime * 100);
            transform.rotation = newRot;

        }
        else
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, baseRotation, 0.0f), Time.deltaTime * 100);
            transform.rotation = newRot;

        }
    }

}
