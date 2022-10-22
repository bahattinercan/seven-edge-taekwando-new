using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PickableItems
{
    public class PickableItemBase : MonoBehaviour
    {
        public enum DisableTypes
        {
            Disable,
            Destroy,
        }

        [Tooltip("Amount of energy to be given to the player")]
        public float energyAmount;
        [Tooltip("the amount of consumption if the item will cause more energy consumption")]
        public float newEnergyDropPenaltyAmaount;
        [Tooltip("Duration of penalty")]
        public float penaltyTime;
        [Tooltip("What happens when obje picked up")]
        public DisableTypes disableType = DisableTypes.Disable;

       
        
       


        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) PickUp();
        }

        //Called when collided with player tag
        protected virtual void PickUp()
        {
            DisabLeObj();
        }
        
        protected virtual void DisabLeObj()
        {
            switch (disableType)
            {
                case DisableTypes.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case DisableTypes.Destroy:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
