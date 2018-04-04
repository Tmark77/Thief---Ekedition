using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BaseGuard : Creature
	{

		public BaseGuard ()
		{
			RangeOfVision = 20;
			MaxHealth = 100;
			Health = MaxHealth;
		}

		public override bool IsInFieldOfView (float angle)
		{
			return Vector3.Distance (player.position, this.transform.position) < RangeOfVision && angle < 90f;
		}

		public int damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<DynamicFieldObject>())
            {
                DynamicFieldObject obj = other.gameObject.GetComponent<DynamicFieldObject>();
                if ((obj as Door).locked && (obj as Door).keyID == this.e[0].Kod)
                {
                    (obj as Door).locked = false;
                    obj.Interaction(true);
                }
                else
                {
                    obj.Interaction(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<DynamicFieldObject>())
            {
                DynamicFieldObject obj = other.gameObject.GetComponent<DynamicFieldObject>();
                obj.Interaction(true);
                if ((obj as Door).keyID == this.e[0].Kod)
                {
                    (obj as Door).locked = true;
                }
            }
        }
    }
}

