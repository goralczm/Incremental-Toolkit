using UnityEngine;
using UnityEngine.Audio;

namespace Settings
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _exposedVolumeName;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_exposedVolumeName))
            {
                float value = PlayerPrefs.GetFloat(_exposedVolumeName);
                _mixer.SetFloat(_exposedVolumeName, value);
            }
        
        }

        public void SetVolume(float value)
        {
            _mixer.SetFloat(_exposedVolumeName, value);
            PlayerPrefs.SetFloat(_exposedVolumeName, value);
        }
    }
}
