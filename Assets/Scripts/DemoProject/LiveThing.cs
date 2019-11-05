using System.Collections;
using UnityEngine;

public abstract class LiveThing : PhysicsObject
{
    public float Hp = 100f;
    public float SoulPoints = 100f;
    public GameObject Soul;

    private float _currentHp;
    private float _maxHp;
    private bool _invulnerability;

    protected bool IsDead = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _currentHp = Hp;
        _maxHp = Hp;
    }

    protected override void ComputeVelocity()
    {
        base.ComputeVelocity();

        if (!IsAlive()) Die();
    }

    public float GetCurrentHp()
    {
        return _currentHp;
    }

    public float GetMaxHp()
    {
        return _maxHp;
    }

    public void ApplyTrapDamage()
    {
        _currentHp = 0;
    }

    public void ApplyDamage(float damageToApply)
    {
        if (_invulnerability || IsDead) return;
        
        _currentHp -= damageToApply;

        if (!IsAlive()) return;
        
        Animator.SetTrigger("isHurt");
        StartCoroutine(Invinsivble());
    }

    public void ApplyHealing(float healToApply)
    {
        if (IsDead) return;
        
        if (_currentHp + healToApply > _maxHp)
        {
            _currentHp = _maxHp;
        }
        else
        {
            _currentHp += healToApply;
        }
    }

    protected virtual IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(1);
        DropSoul();
        Destroy(gameObject);
    }

    private IEnumerator Invinsivble()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);

        SpriteRenderer.color = Color.white;
        _invulnerability = false;
    }

    private bool IsAlive()
    {
        return _currentHp > 0;
    }

    private void Die()
    {
        if (!IsDead)
        {
            IsDead = true;
            Animator.SetTrigger("isDead");
            StartCoroutine(Respawn());
        }
    }

    private void DropSoul()
    {
        Soul.GetComponent<SoulTrigger>().SetPoints(SoulPoints);
        Instantiate(Soul, transform.position, Quaternion.identity);
    }
}