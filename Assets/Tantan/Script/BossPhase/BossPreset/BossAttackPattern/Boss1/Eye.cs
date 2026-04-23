using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Eye : MonoBehaviour
{
    Transform target => GameObject.FindGameObjectWithTag("Player").transform;
    Vector2 targetPos;

    [Header("References")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject attackObj;

    [Header("Properties")]
    [SerializeField] bool isFollow = false;
    [SerializeField] bool isAttack = false;
    [SerializeField] float stopRange = 1.25f;
    [SerializeField] float speed = 3f;
    [SerializeField] float chargedTime = 1.75f;  
    [SerializeField] float attackTime = 2;
    [SerializeField] float lifeTime = 5f;

    private void Start()
    {
        isFollow = true;
    }

    private void Update()
    {
        CheckPlayerArea();
        MoveTo(target.position);
        LookAt(target);
        Attack();

        Destroy(gameObject, lifeTime);
    }

    void MoveTo(Vector3 target)
    {
        if (!isFollow) return;

        Vector2 distance = target - transform.position;
        Vector2 dir = distance.normalized;

        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    void LookAt(Transform target)
    {
        if (isAttack) return;

        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void CheckPlayerArea()
    {
        if (Physics2D.OverlapCircle(transform.position,stopRange,playerMask))
        {
            isFollow = false;
        }
    }

    void Attack()
    {
        if (isFollow) return;
        if (isAttack) return;
        
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(chargedTime);
        isAttack = true;
        attackObj.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
