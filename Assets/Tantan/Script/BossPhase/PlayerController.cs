using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    Collider2D col;

    PlayerInputAction action;

    [Header("References")]
    [SerializeField] LayerMask damageMask;
    [SerializeField] LayerMask sunMask;
    [SerializeField] GameObject tail;
    [SerializeField] GameObject iceOverlay;
    [SerializeField] GameObject powderOverlay;
    [SerializeField] GameObject heatOverlay;
    [SerializeField] Slider dadSlider;

    [Header("Properties")]
    [SerializeField] bool isInvincible = false;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float hp = 4;
    bool isDash = false;
    bool isDashCooldown = false;
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
    float dadValue = 0f;
    float DhadValue
    {
        get => dadValue;
        set => dadValue = Mathf.Clamp(value, 0, dadSlider.maxValue);
    }

    [Header("Status")]
    [SerializeField] bool isIce = false;
    [SerializeField] bool isPowder = false;
    [SerializeField] bool isHeat = false;

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

        GameManager.Instance.OnGameEnded += OnEnded;
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
        col = Physics2D.OverlapBox(transform.position, transform.localScale, 0, damageMask);

        if (col)
        {
            if (!isInvincible)
            {
                if(col.gameObject.CompareTag("Bullet"))
                {
                    Bullet bul = col.GetComponent<Bullet>();

                    if (bul)
                        bul.Despawn(0);
                }
                else if(col.gameObject.CompareTag("Water"))
                {
                    isIce = true;
                }
                else if (col.gameObject.CompareTag("Powder"))
                {
                    isPowder = true;
                }
                TakeDamage();
            }
        }

        DhadValue += Physics2D.OverlapBox(transform.position, transform.localScale, 0, sunMask) ? Time.deltaTime : -Time.deltaTime;

        GameOverCheck();

        if (DhadValue >= dadSlider.maxValue)
        {
            DhadValue = 0;
            isHeat = true;
            TakeDamage();
        }
    }

    void ResetStat(float originalSpeed)
    {
        speed = originalSpeed;
    }

    private void FixedUpdate()
    {
        if (isDash) return;

        rb.linearVelocity = moveInput * speed;
    }

    private void LateUpdate()
    {
        HealthVisual();
        UIVisualize();
    }

    void Move(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();

    void StopMove(InputAction.CallbackContext context) => moveInput = Vector2.zero;

    void Dash(InputAction.CallbackContext context)
    {
        if (isDash) return;
        if (isDashCooldown) return;

        isDash = true;
        isInvincible = true;

        tail.SetActive(true);

        Vector2 direction = moveInput.normalized;

        if (direction == Vector2.zero)
            direction = Vector2.zero;

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

        tail.SetActive(false);

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

        if (isIce)
            StartCoroutine(ResetIce());

        if (isPowder)
            StartCoroutine(ResetPowder());

        if (isHeat)
            StartCoroutine(ResetHeatStroke());

        StartCoroutine(IFrame());
    }

    IEnumerator IFrame()
    {
        isInvincible = true;
        sr.color = new Color(1, 1, 1, .5f);

        yield return new WaitForSeconds(2.5f);

        isInvincible = false;
        sr.color = new Color(1, 1, 1, 1);
    }

    IEnumerator ResetIce()
    {
        speed *= 2;
        Camera.main.GetComponent<CameraShake>().Shake(2f, .1f, CameraShake.ShakeStyle.Vigorous);
        sr.color = new Color(0, 0, 1, .5f);

        iceOverlay.SetActive(true);

        yield return new WaitForSeconds(.5f);

        speed /= 2;

        ResetStat(speed);
        iceOverlay.SetActive(false);
        isIce = false;
        sr.color = new Color(1, 1, 1, sr.color.a);
    }

    IEnumerator ResetPowder()
    {
        powderOverlay.SetActive(true);
        yield return new WaitForSeconds(2f);
        powderOverlay.SetActive(false);
        isPowder = false;
    }

    IEnumerator ResetHeatStroke()
    {
        heatOverlay.SetActive(true);
        Camera.main.GetComponent<CameraShake>().Shake(2f, .5f, CameraShake.ShakeStyle.Smooth);
        yield return new WaitForSeconds(2f);
        heatOverlay.SetActive(false);
        isHeat = false;
    }

    void HealthVisual() => sr.material.SetFloat("_FillAmount",HP/maxHp);

    void UIVisualize()
    {
        if(!dadSlider) return;

        dadSlider.value = dadValue;
    }

    void GameOverCheck()
    {
        if (HP <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    void OnEnded()
    {
        GameManager.Instance.health = $"{HP}/{maxHp}";
        GameManager.Instance.score += (HP / maxHp) * 50;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
