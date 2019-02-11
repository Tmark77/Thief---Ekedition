using System;
using UnityEngine;
using System.Collections;

public class BaseGuard : Creature
{
    public BaseGuard() : base()
    {
        RangeOfVision = 20;
        MaxHealth = 100;
        Health = MaxHealth;
    }

    public override bool IsInFieldOfView(float angle)
    {
        return Vector3.Distance(player.position, this.transform.position) < RangeOfVision && angle < VisionAngle();
    }

    
    public int damage;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<DynamicFieldObject>())
        {
            StartCoroutine(OpenClose(other.gameObject.GetComponent<DynamicFieldObject>()));
        }


    }

    private IEnumerator OpenClose(DynamicFieldObject obj)
    {
        if (this.condition.CanUseThings())
        {
            if ((obj as Door).Locked)
            {
                foreach (Collectible item in e)
                {
                    if ((item is Key) && (obj as Door).keyID == (item as Equipment).Kod)
                    {
                        (obj as Door).Locked = false;
                    }
                }
            }
            if (!(obj as Door).Locked)
            {
                obj.Interaction(true);
                //(obj as Door).door02.GetComponent<Collider> ().isTrigger = true;
                yield return new WaitForSecondsRealtime(2);
                obj.Interaction(true);
                yield return new WaitForSecondsRealtime(1);
                foreach (Collectible item in e)
                {
                    if ((item is Key) && (obj as Door).keyID == (item as Equipment).Kod)
                    {
                        (obj as Door).Locked = true;
                    }
                }
            }
            else
            {

                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                //Debug.Log(this.gameObject.GetComponent<Rigidbody>().isKinematic);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<DynamicFieldObject>())
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Debug.Log (this.gameObject.GetComponent<Rigidbody> ().isKinematic);
        }
    }

    public override float VisionAngle()
    {
        return 75f;
    }
}

