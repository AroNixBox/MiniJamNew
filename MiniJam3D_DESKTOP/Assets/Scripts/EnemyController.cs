using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float speed = 5f;
    public float detectionRadius = 5f;
    public float attackRange = 2.0f;
    public float _attackDamage = 50f;

    private Animator _animator;
    private GameObject _player;
    private List<Vector3> _wanderPoints = new List<Vector3>();
    private int _currentPoint = 0;
    private bool _isChasingPlayer = false;
    private float _canAttack = 0f;

    private Vector3 _currentForward = Vector3.forward;
    public float rotationSpeed = 1.0f;
    private const float AngleThreshold = 5.0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _wanderPoints = GenerateWanderPoints();
    }

    private List<Vector3> GenerateWanderPoints()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
            randomPoint.y = transform.position.y; // Stelle sicher, dass es auf der gleichen Höhe wie der Gegner ist.
            points.Add(randomPoint);
        }
        return points;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

        _isChasingPlayer = distanceToPlayer <= detectionRadius;

        if (_isChasingPlayer)
        {
            MoveTowards(_player.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            MoveTowards(_wanderPoints[_currentPoint]);
            Vector3 currentPositionFlat = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 wanderPointPositionFlat = new Vector3(_wanderPoints[_currentPoint].x, 0, _wanderPoints[_currentPoint].z);

            if (Vector3.Distance(currentPositionFlat, wanderPointPositionFlat) < 0.5f)
            {
                _currentPoint = (_currentPoint + 1) % _wanderPoints.Count;
            }


        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;

        _currentForward = Vector3.Slerp(_currentForward, direction, rotationSpeed * Time.deltaTime);
        transform.forward = _currentForward;

        if (!_isChasingPlayer)
        {
            float angleToTarget = Vector3.Angle(transform.forward, direction);
            if (angleToTarget > AngleThreshold)
            {
                _animator.SetBool("isMoving", false);
                return;
            }
        }

        transform.position += direction * (speed * Time.deltaTime);
        _animator.SetBool("isMoving", true);
    }

    private void AttackPlayer()
    {
        _animator.SetTrigger("isAttacking");
    }

    public void CanAttackPlayer(float canAttackValue)
    {
        _canAttack = canAttackValue;
    }

    public void RequestDoDamageToPlayer(IDamageable player)
    {
        if (_canAttack == 1f)
            player.TakeDamage(_attackDamage);
    }
}
