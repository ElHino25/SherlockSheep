using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{
   [SerializeField] [TextArea] private string hoverText;
   [SerializeField] [TextArea] private string collectionText;
   public string GetHoverText()
   {
      return hoverText;
      
   }

   public string GetCollectionText()
   {
      return collectionText;
   }
   
   public abstract void Collect();
   
   
}
