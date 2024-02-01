using UnityEngine;

public class TriggerAnimationsFootsteps : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioSource m_AudioSrc;
    [SerializeField] private AudioClip[] m_ClipsFootsteps;


    public void TriggerSound()
    {
        m_AudioSrc.volume = Random.Range(0.2f, .45f);
        m_AudioSrc.pitch = Random.Range(.85f, 1f);
        m_AudioSrc.PlayOneShot(m_ClipsFootsteps[Random.Range(0, m_ClipsFootsteps.Length)]);
    }

}
