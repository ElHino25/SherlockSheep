using System;
using Unity.VisualScripting;
using UnityEngine;

public class ZiegenFried : MonoBehaviour, IInteractable
{
    [SerializeField] private string collectionText;
    [SerializeField] private string postDeathHoverText;
    [SerializeField] private string preDeathHoverText;
    [SerializeField] private string dyingHoverText;
    [SerializeField] private string postDeathCollectionText;
    [SerializeField] private string preDeathCollectionText;
    [SerializeField] private string dyingCollectionText;
    
    private Animator animator;
    private string zustand;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        zustand = "alive";
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isDying", true);
        if (zustand == "alive")
        {
           zustand = "dying"; 
        }
        
    }
    
    private void DeathAnimationFinished()
    {
      zustand = "dead";
    }

    public string GetHoverText()
    {
        if (zustand == "alive")
        {
            return preDeathHoverText;
        }
        else if  (zustand == "dying")
        {
            return dyingHoverText;
        }
        else if (zustand == "dead")
        {
            return postDeathHoverText;
        }
        else 
        {
            return collectionText; 
        }
        
    }

    public string GetCollectionText()
    {
        if (zustand == "alive")
        {
            return preDeathCollectionText;
        }
        else if  (zustand == "dying")
        {
            return dyingCollectionText;
        }
        else if  (zustand == "dead")
        {
            return postDeathCollectionText;
        }
        else 
        {
            return collectionText;
        }
    }

    public void Collect()
    {
       
    }
}