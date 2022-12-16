using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFlow : MonoBehaviour
{
    ParticleSystem m_System;

    public ParticleForceFieldsSO fieldsSO;
    private void Start()
    {
        InitializeIfNeeded();
        fieldsSO?.InitializeForceFields(m_System);
    }
    private void LateUpdate()
    {
        fieldsSO?.ForceFields();       
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();
       
    }
}