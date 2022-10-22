using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PK.PickableItems
{
    public class PickableFruit : PickableItemBase
    {

        protected override void PickUp()
        {
            base.PickUp();
            AddEnergySignal.Trigger(energyAmount);
        }
    }
}
