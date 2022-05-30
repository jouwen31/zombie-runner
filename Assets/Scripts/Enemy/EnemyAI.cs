using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth), typeof(EnemyAttack), typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
  [SerializeField] Transform target;
  public Transform Target { get { return target; } }
  [SerializeField][Range(0, 50)] float attackRange = 5f;
  [SerializeField][Range(0, 10)] int turnSpeed = 5;

  NavMeshAgent navMeshAgent;
  Animator animator;

  float distanceToTarget = Mathf.Infinity;
  bool isProvoked = false;

  void Start()
  {
    navMeshAgent = GetComponent<NavMeshAgent>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    if (target != null)
    {
      distanceToTarget = Vector3.Distance(target.position, transform.position);
      if (isProvoked)
      {
        EngageTarget();
      }
      else if (distanceToTarget <= attackRange)
      {
        EngageTarget();
      }
      else
      {
        PlayIdelAnimation();
      }
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);
  }

  void PlayIdelAnimation()
  {
    if (animator != null)
    {
      animator.SetTrigger("idel");
    }
  }

  void EngageTarget()
  {
    FaceTarget();
    if (navMeshAgent.stoppingDistance < distanceToTarget)
    {
      ChaseTarget();
    }
    else
    {
      AttachTarget();
    }
  }

  void FaceTarget()
  {
    // my solution (not giving smooth transition)
    // transform.LookAt(target.position);

    // cp solution
    Vector3 direction = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
  }

  void ChaseTarget()
  {
    // we can set is provoked to true if we want enemy to keep chasing target once it get in range even if it then gets out of range
    // isProvoked = true;
    if (animator != null)
    {
      animator.SetBool("attack", false);
      animator.SetTrigger("move");
    }
    navMeshAgent.SetDestination(target.position);
  }

  void AttachTarget()
  {
    if (animator != null)
    {
      animator.SetBool("attack", true);
    }
  }

  void OnTakeDamage()
  {
    isProvoked = true;
  }

  public void Die()
  {
    this.enabled = false;
    if (navMeshAgent != null)
    {
      navMeshAgent.enabled = false;
    }

    if (animator != null)
    {
      animator.SetTrigger("die");
    }
  }
}
