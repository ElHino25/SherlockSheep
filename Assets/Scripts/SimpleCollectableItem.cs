using UnityEngine;

public class SimpleCollectableItem : InteractableItem
{
   public override void Collect()
   {
      Destroy(gameObject, 1f);
   }
}
