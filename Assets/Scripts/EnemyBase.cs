using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public float damage = 100f;

    protected float playerRealizeRange = 10f;
    protected float attackRange = 5f;
    protected float attackCoolTime = 2f;
    protected float attackCoolTimeCacl = 2f;
    protected bool canAtk = true;

    protected float moveSpeed = 2f;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;

    protected GameObject parentRoom;

    protected Animator Anim;
    protected Rigidbody rb;

    public LayerMask layerMask;

    // Use this for initialization
    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
         nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

        parentRoom = transform.parent.transform.parent.gameObject;

        StartCoroutine(CalcCoolTime());
    }

    protected bool CanAtkStateFun()
    {
        Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z);

        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f, layerMask);
        distance = Vector3.Distance(Player.transform.position, transform.position);

        if (hit.transform == null)
        {
            Debug.Log("nn");
            return false;
        }

        if (hit.transform.CompareTag("Player") && distance <= attackRange)
        {
            Debug.Log("mm");
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;
                if (attackCoolTimeCacl <= 0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }
}