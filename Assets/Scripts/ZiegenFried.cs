using System;
using Unity.VisualScripting;
using UnityEngine;

public class ZiegenFried : MonoBehaviour
{
  private Animator animator;
  
  private void Awake()
  {
     animator = GetComponent<Animator>();
      
  }
  
  private void OnTriggerEnter(Collider other)
  { 
      animator.SetBool("isDying" , true);
  }
      
}
