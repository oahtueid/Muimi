using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PersistentManager<Player>
{
    // Move
    private Vector2 moveInput;
    public float moveCooldown = 0.2f;
    private float lastMoveTime;
    private Rigidbody2D rb;
    private Animator animator;

    public float cellSize = 1f;

    private IInteractable currentInteractable;
    private Transform currentInteractableTransform;
    private Vector2 lastMoveDir;

    [SerializeField]
    GameObject hintUI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (!animator || !rb)
        {
            Debug.LogWarning("Animator or Rigidbody not found");
        }
    }

    // private void OnEnable()
    // {
    //     InputHandler.Instance.controls.Player.Move.performed += ctx =>
    //         moveInput = ctx.ReadValue<Vector2>();
    //     InputHandler.Instance.controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    // }

    // private void OnDisable()
    // {
    //     InputHandler.Instance.controls.Player.Move.performed -= ctx =>
    //         moveInput = ctx.ReadValue<Vector2>();
    //     InputHandler.Instance.controls.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
    // }

    private void Update()
    {
        animator.SetBool("IsMoving", false);
        if (moveInput != Vector2.zero && Time.time - lastMoveTime > moveCooldown)
        {
            Vector3 step = new Vector3(moveInput.x, moveInput.y, 0) * cellSize;
            Vector3 targetPos = rb.position + (Vector2)step;

            rb.MovePosition(targetPos);

            lastMoveTime = Time.time;
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            animator.SetBool("IsMoving", true);
            lastMoveDir = moveInput;
        }
    }

    public void Move(Vector2 dir)
    {
        moveInput = dir;
    }

    public void Stop()
    {
        moveInput = Vector2.zero;
    }

    public void TryInteract()
    {
        if (currentInteractable != null)
        {
            Vector2 toObject = (
                currentInteractableTransform.position - transform.position
            ).normalized;

            Debug.Log("dot: " + Vector2.Dot(lastMoveDir, toObject));
            // kiểm tra player có hướng về object không
            if (Vector2.Dot(lastMoveDir, toObject) >= 0f)
            {
                currentInteractable.Interact();
                currentInteractable = null;
            }
            else
            {
                Debug.Log("Player không hướng về object, không thể tương tác.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
            currentInteractableTransform = other.transform;
            Vector2 toObject = (
                currentInteractableTransform.position - transform.position
            ).normalized;

            if (Vector2.Dot(lastMoveDir, toObject) >= 0f)
            {
                DisplayInteractHint();
            }
            else
                HideInteractHint();
        }
    }

    void OnTriggerStay2D()
    {
        if (currentInteractableTransform)
        {
            Vector2 toObject = (
                currentInteractableTransform.position - transform.position
            ).normalized;
            if (Vector2.Dot(lastMoveDir, toObject) >= 0f)
            {
                DisplayInteractHint();
            }
            else
                HideInteractHint();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (
            other.TryGetComponent(out IInteractable interactable)
            && interactable == currentInteractable
        )
        {
            currentInteractable = null;
            currentInteractableTransform = null;
            HideInteractHint();
        }
    }

    void DisplayInteractHint()
    {
        hintUI.SetActive(true);
        hintUI.transform.position = currentInteractableTransform.position;
    }

    void HideInteractHint()
    {
        hintUI.SetActive(false);
    }
}
