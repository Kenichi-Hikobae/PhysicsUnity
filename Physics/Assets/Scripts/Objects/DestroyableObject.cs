using System.Collections;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;                   //  Maximum health of the object
    [SerializeField]
    private bool canHealPerTime = true;              //  If the object can heal over time
    [SerializeField]
    private float damageEffectIntesity = 2.0f;       //  Intensity of effect when the player damages the enemy
    [SerializeField]
    private float damageEffectDuration = 0.1f;       //  Duration of the effect
    [SerializeField]
    private GameObject destroyEffect;                //  Effect when the object is destroyed

    private Material m_ObjectMaterial;          //  Material of the object
    private Color m_Color;                      //  Default color of the object, it's required to make the damage effect
    private float m_CurrentHealth;
    private float m_Intensity;
    private float m_DamageEffectTimer;

    private void Start()
    {
        m_CurrentHealth = health;
        m_Intensity = damageEffectIntesity;
        //  Get the material and the default color of the object
        m_ObjectMaterial = GetComponent<Renderer>().material;
        m_Color = m_ObjectMaterial.color;
    }

    private void Update()
    {
        if (m_CurrentHealth <= 0)
        {
            //  If the objecto has no life
            DestroyObject();
        }

        if (m_CurrentHealth != health && canHealPerTime)
        {
            //  If the object can heal then it heals over time
            m_CurrentHealth += Time.deltaTime;
        }

        if (m_DamageEffectTimer > 0)
        {
            m_DamageEffectTimer -= Time.deltaTime;
            //  Change the brightness of the mesh material to make the damage effect
            m_ObjectMaterial.color *= m_Intensity;
        }
        else
        {
            //  Restart the color of the mesh material
            m_ObjectMaterial.color = m_Color;
        }
    }

    public void TakeDamage(float damage)
    {
        //  Take damage to the current health of the object
        m_CurrentHealth -= damage;
        if (m_CurrentHealth > 0)
        {
            //  Start the damage effect
            m_DamageEffectTimer = damageEffectDuration;
        }
    }

    private void DestroyObject()
    {
        //  Destroy the object and play the particle effect
        Destroy(gameObject);
        GameObject effect = Instantiate<GameObject>(destroyEffect, transform.position, Quaternion.identity);
        effect.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        StartCoroutine(DestroyParticle(effect));
    }

    private IEnumerator DestroyParticle(GameObject effect)
    {
        yield return new WaitForSeconds(effect.GetComponent<ParticleSystem>().main.duration);
        Destroy(effect);
    }
}
