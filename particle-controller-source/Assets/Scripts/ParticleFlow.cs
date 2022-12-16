using UnityEngine;

[RequireComponent(typeof(ParticleSystemForceField))]
public class ParticleFlow : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystemForceField m_SystemForce;

    public ParticleForceFieldsSO fieldsSO;
    private void Start()
    {
        InitializeIfNeeded();
        fieldsSO?.InitializeForceFields(m_System, m_SystemForce);
    }
    private void LateUpdate()
    {
        fieldsSO?.ForceFields();       
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();
        
        if (m_SystemForce == null)
            m_SystemForce = GetComponent<ParticleSystemForceField>();

    }
}
