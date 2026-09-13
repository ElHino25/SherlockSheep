using System;
using Unity.VisualScripting;
using UnityEngine;

public class ZiegenFried : MonoBehaviour
{
    [SerializeField] private string collectionText;
    [SerializeField] private string postDeathHoverText;
    [SerializeField] private string preDeathHoverText;
    [SerializeField] private string dyingHoverText;
    
    private Animator animator;
    private string zustand = "alive";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isDying", true);
    }

    private string PreDeathHoverText()
    {
        return preDeathHoverText; 
    }
    
    private string GetDeathHoverText()
    {
        return dyingHoverText;
    }
    private string GetPostDeathHoverText()
    {
        return postDeathHoverText;
    }
    
    
}