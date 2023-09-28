using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Combat : MonoBehaviour, IDamageable
{
    private float _attackCycle = 0f;
    private bool _isAttacking = false;
    private Animator _animator;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    public Image HealthBar;

    [SerializeField] private float maxHealth;
    public float _currentHealth;
    [SerializeField] private float damageAmount;
    [SerializeField] private AudioSource pickUpSoundEffect;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        HealthBar.fillAmount = _currentHealth * 0.002f;
    }

    private void OnAttack(InputValue value)
    {
        _isAttacking = value.isPressed; 
        Debug.Log($"isAttacking is {_isAttacking}");
    }
    void Update()
    {
        if (_isAttacking)
        {
            _animator.SetBool(IsAttacking, true);
        }
        else
        {
            _animator.SetBool(IsAttacking, false);
        }
    }

    public void SetCanAttack(float canAttack)
    {
        _attackCycle = canAttack;
        Debug.Log(_attackCycle);
    }
    public void HandleSwordTriggerEnter(Collider other)
    {
        if (_attackCycle == 1f)
        {
            Debug.Log("HitEnemy");
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Pickable"))
        {
            _animator.SetTrigger("Pickup");
            pickUpSoundEffect.Play();
            Destroy(other.GameObject());
            GameManager.Instance.ObjectiveCollected();
        }
    }

    public void TakeDamage(float hitDamageAmount)
    {
        _currentHealth -= hitDamageAmount;
        HealthBar.fillAmount = _currentHealth * 0.002f;
        Debug.Log($"Player took {hitDamageAmount} and has now {_currentHealth}HP left");
        //TODO Add GetHitEffects/ Anims
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("LoseScene");
        Destroy(gameObject);
    }
}

public interface IDamageable
{
    void TakeDamage(float hitDamageAmount);
    void Die();
}
