using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    PlayerInputAction action;

    [Header("References")]
    [SerializeField] LayerMask damageMask;

    [Header("Properties")]
    bool isDash = false;
    bool isDashCooldown = false;
    [SerializeField] bool isInvincible = false;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float hp = 4;
    float HP
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, maxHp);
    }
    [SerializeField] float maxHp = 4;
    [SerializeField] float speed = 5f;
    [SerializeField] float dashForce = 100f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 1f;

    private void Awake()
    {
        action = new PlayerInputAction();
    }

    private void OnEnable()
    {
        action.Enable();

        action.BossPhase.Move.performed += Move;
        action.BossPhase.Move.canceled += StopMove;

        action.BossPhase.Dash.performed += Dash;
    }

    private void OnDisable()
    {
        action.BossPhase.Move.performed -= Move;
        action.BossPhase.Move.canceled -= StopMove;

        action.BossPhase.Dash.performed -= Dash;

        action.Disable();
    }

    private void Update()
    {
        if (Physics2D.OverlapBox(transform.position, transform.localScale, 0, damageMask))
        {
            if (!isInvincible)
                TakeDamage();
        }
    }

    private void FixedUpdate()
    {
        if (isDash) return;

        rb.linearVelocity = moveInput * speed;
    }

    private void LateUpdate()
    {
        HealthVisual();
    }

    void Move(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();

    void StopMove(InputAction.CallbackContext context) => moveInput = Vector2.zero;

    void Dash(InputAction.CallbackContext context)
    {
        if (isDash) return;
        if (isDashCooldown) return;

        isDash = true;
        isInvincible = true;

        Vector2 direction = moveInput.normalized;

        if (direction == Vector2.zero)
            direction = Vector2.right;

        StartCoroutine(DashCoroutine(direction));
    }

    IEnumerator DashCoroutine(Vector2 direction)
    {
        float dashSpeed = dashForce;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = direction * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;

        isInvincible = false;
        isDash = false;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }

    public void TakeDamage()
    {
        HP--;
        StartCoroutine(IFrame());
    }

    IEnumerator IFrame()
    {
        isInvincible = true;
        sr.color = new Color(1, 1, 1, .5f);
        yield return new WaitForSeconds(5f);
        isInvincible = false;
        sr.color = new Color(1, 1, 1, 1);
    }

    void HealthVisual() => sr.material.SetFloat("_FillAmount",HP/maxHp);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
