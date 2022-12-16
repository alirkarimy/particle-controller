using UnityEngine;

[CreateAssetMenu(fileName = "Particle Force Fields", menuName = "ScriptableObjects/Particle Force Fields", order = 1)]
public class ParticleForceFieldsSO : ScriptableObject
{
    public ParticleSystemForceFieldShape m_Shape = ParticleSystemForceFieldShape.Sphere;
    public float m_StartRange = 0.0f;
    public float m_EndRange = 3.0f;
    public Vector3 m_Direction = Vector3.zero;
    public float m_Velocity = 0.02f;
    public Vector3 m_TargetPos = Vector3.zero ;
    public float m_Gravity = 0.0f;
    public float m_GravityFocus = 0.0f;
    public float m_RotationSpeed = 0.0f;
    public float m_RotationAttraction = 0.0f;
    public Vector2 m_RotationRandomness = Vector2.zero;
    public float m_Drag = 0.0f;
    public bool m_MultiplyDragByParticleSize = false;
    public bool m_MultiplyDragByParticleVelocity = false;

    private ParticleSystemForceField m_ForceField;
    ParticleSystem.Particle[] m_Particles;
    private ParticleSystem m_Ps;

    public void InitializeForceFields(ParticleSystem ps)
    {
        m_Ps = ps;
        m_ForceField = m_Ps.gameObject.AddComponent<ParticleSystemForceField>();
        var forces = ps.externalForces;
        forces.enabled = true;        
    }

    public void ForceFields()
    {
        
        m_ForceField.shape = m_Shape;
        m_ForceField.startRange = m_StartRange;
        m_ForceField.endRange = m_EndRange;
        m_ForceField.directionX = m_Direction.x;
        m_ForceField.directionY = m_Direction.y;
        m_ForceField.directionZ = m_Direction.z;
       

        m_ForceField.gravity = m_Gravity;
        m_ForceField.gravityFocus = m_GravityFocus;
        m_ForceField.rotationSpeed = m_RotationSpeed;
        m_ForceField.rotationAttraction = m_RotationAttraction;
        m_ForceField.rotationRandomness = m_RotationRandomness;
        m_ForceField.drag = m_Drag;
        m_ForceField.multiplyDragByParticleSize = m_MultiplyDragByParticleSize;
        m_ForceField.multiplyDragByParticleVelocity = m_MultiplyDragByParticleVelocity;

        if (m_Particles == null || m_Particles.Length < m_Ps.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_Ps.main.maxParticles];
        
        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_Ps.GetParticles(m_Particles);
     
        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            //Keep in mind that the particles do not necessarily reach the designated target and only move towards it.
            m_Particles[i].position = Vector3.MoveTowards(m_Particles[i].position,m_TargetPos, m_Velocity);
        }

        // Apply the particle changes to the Particle System
        m_Ps.SetParticles(m_Particles, numParticlesAlive);

    }


}