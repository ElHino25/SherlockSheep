using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class SheepController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;

    [Header("Movement")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 500f;
    [SerializeField] private float navMeshSearchRadius = 2f;
    
    [Header("Interaction")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 10f;
    [SerializeField] private float collectionDistance = 2f;
    [SerializeField] TMP_Text hoverText;
    [SerializeField] TMP_Text interactionText;

    private NavMeshAgent agent;
    private Animator animator;

    private bool isInteracting;
    private IInteractable activeInteractable;
    private bool isCollecting;
    private float collectionTimer;
    
    private float hoverTimer;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        HandleActiveInteraction();
        HandleHoverTimer();
        HandleNewMouseInteraction();
        HandleAnimation();
    }
    
    private void HandleActiveInteraction()
    {
        if (!isInteracting || activeInteractable == null)
            return;

        if (!isCollecting)
        {
            TryStartCollecting();
            return;
        }

        UpdateCollecting();
    }

    private void TryStartCollecting()
    {
        if (!agent.pathPending && agent.remainingDistance <= collectionDistance)
        {
            isCollecting = true;
            collectionTimer = 1f;
            if (interactionText != null)
            {
                interactionText.text = activeInteractable.GetCollectionText();    
            }
        }
    }

    private void UpdateCollecting()
    {
        collectionTimer -= Time.deltaTime;

        if (collectionTimer <= 0f)
        {
            activeInteractable.Collect();
            activeInteractable = null;
            
            isInteracting = false;
            isCollecting = false;
            collectionTimer = 0f;
        }
    }
    
    private void HandleHoverTimer()
    {
        if (hoverTimer >= 0)
        {
            hoverTimer -= Time.deltaTime;
            if (hoverTimer <= 0 && hoverText != null)
            {
                hoverText.text = "";
            }
        }
    }
    
    private void HandleNewMouseInteraction()
    {
        if (isInteracting)
            return;
        
        if (IsPointerOverUI())
            return;
        
        Ray ray = CreateMouseRay();
        
        if (!WasLeftMouseButtonPressed())
        {
            TryHandleInteractableHover(ray);
        }
       else
       {
           if (interactionText != null)
           {
               interactionText.text = "";    
           }
           TryHandleInteractableClick(ray);
           TryHandleGroundClick(ray);
        }
    }

    private bool WasLeftMouseButtonPressed()
    {
        return Mouse.current != null && 
               Mouse.current.leftButton.wasPressedThisFrame;
    }
    
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject();
    }

    private Ray CreateMouseRay()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenPointToRay(mousePosition);
    }

    private void TryHandleInteractableHover(Ray ray)
    {
        if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                raycastDistance,
                interactableLayer,
                QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                hoverTimer = 0.2f;
                if (hoverText != null)
                {
                    hoverText.text = interactable.GetHoverText();
                }
            }
        }
    }
    
    private void TryHandleInteractableClick(Ray ray)
    {
        if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                raycastDistance,
                interactableLayer,
                QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                if (distance < interactionDistance)
                {
                    isInteracting = true;
                    activeInteractable = interactable;
                }
            }
        }
    }

    private void TryHandleGroundClick(Ray ray)
    {
        if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                raycastDistance,
                groundLayer,
                QueryTriggerInteraction.Ignore))
        {
            Move(hit);
        }
    }

    private bool Move(RaycastHit hit)
    {
        //gibt es in der Nähe eine Position auf dem NavMesh?
        if (NavMesh.SamplePosition(
                hit.point,
                out NavMeshHit navMeshHit,
                navMeshSearchRadius,
                NavMesh.AllAreas))
        {
            //Setze ein neues Navigationsziel
            agent.SetDestination(navMeshHit.position);
            return true;
        }

        return false;
    }

    protected void HandleAnimation()
    {
        animator.SetBool("Eat", isCollecting);
        
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("MoveForwards", true);
        }
        else
        {
            animator.SetBool("MoveForwards", false);
        }
    }
}
