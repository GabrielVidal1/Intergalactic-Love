using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth;

    private float health;

    private Animator animator;

    [SerializeField] Transform triggerBox;

    public SpacePhaseManager spacePhaseManager;

    private void Start()
    {
        health = totalHealth;

        animator = GetComponent<Animator>();

        UpdateHealthBar();
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapBox(triggerBox.position, triggerBox.localScale * 0.5f);

        foreach (Collider collider in cols)
        {
            if (collider.CompareTag("Asteroid"))
            {
                Asteroid a = collider.GetComponent<Asteroid>();
                TakeDamage(a.GetDamage());
                Destroy(a.gameObject);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        if (health - damage < 0f)
        {
            if (GameManager.gm.spacePhaseManager != null)
                GameManager.gm.spacePhaseManager.Die();
            return;
        }

        animator.SetTrigger("TakeDamage");

        health -= damage;
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        spacePhaseManager.healthBar.value = health / totalHealth ;
    }
}
