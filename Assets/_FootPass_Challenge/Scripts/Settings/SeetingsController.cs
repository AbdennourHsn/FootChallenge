using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SeetingsController : MonoBehaviour
{

    [Header("Effects")]
    [SerializeField] private GameObject m_Effects;
    [SerializeField] private GameObject m_EffectButtonOn;
    [SerializeField] private GameObject m_EffectButtonOff;

    [Header("Audio")]
    [SerializeField] private AudioMixer m_MasterAudioMixer;
    [SerializeField] private Slider m_VolumeSlider;


    void Start()
    {
        LoadEffectsSettings();

        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            m_MasterAudioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
            m_VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            m_VolumeSlider.value = 1;
            m_MasterAudioMixer.SetFloat("volume", Mathf.Log10(m_VolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("Volume", m_VolumeSlider.value);
        }
    }

    private void LoadEffectsSettings()
    {
        if (PlayerPrefs.HasKey("Effects"))
        {
            m_Effects.SetActive(PlayerPrefs.GetInt("Effects") == 0 ? false : true);
            m_EffectButtonOn.SetActive(m_Effects.activeInHierarchy);
            m_EffectButtonOff.SetActive(!m_Effects.activeInHierarchy);
        }
        else
        {
            m_Effects.SetActive(true);
            PlayerPrefs.SetInt("Effects", 1);
            m_EffectButtonOn.SetActive(true);
            m_EffectButtonOff.SetActive(false);
        }
    }

    public void ToggleEffects()
    {
        if (m_Effects.activeInHierarchy)
        {
            m_Effects.SetActive(false);
            PlayerPrefs.SetInt("Effects", 0);
            m_EffectButtonOn.SetActive(false);
            m_EffectButtonOff.SetActive(true);
        }
        else
        {
            m_Effects.SetActive(true);
            PlayerPrefs.SetInt("Effects", 1);
            m_EffectButtonOn.SetActive(true);
            m_EffectButtonOff.SetActive(false);
        }
    }

    public void OnVolumeChange(float volume)
    {
        m_MasterAudioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", m_VolumeSlider.value);

    }

}
