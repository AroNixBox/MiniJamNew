using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private float _currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Collider _collider;
    [SerializeField] private AudioSource enemyHitSoundEffect;
    [SerializeField] private AudioSource enemyDieSoundEffect;
    [SerializeField] private AudioSource moanAudioSource;
    private Animator _animator;
    private Rigidbody _rb;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float hitDamageAmount)
    {
        _currentHealth -= maxHealth;
        enemyHitSoundEffect.Play();
        if (_currentHealth <= 0)
        {
            enemyDieSoundEffect.Play();
            Die();
        }
    }

    public void Die()
    {
        moanAudioSource.Stop();
        _collider.enabled = false;
        _rb.isKinematic = true;
        GetComponent<EnemyController>().enabled = false;
        _animator.SetBool("Die", true);
        Destroy(gameObject, 5f);
    }
}
