using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public sealed class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundsSlider;

        [SerializeField] private AudioMixer _mixer;

        private void Start()
        {
            _musicSlider
                .onValueChanged
                .AddListener(ChangeMusicVolume);
            
            _soundsSlider
                .onValueChanged
                .AddListener(ChangeSoundsVolume);
        }

        private void ChangeMusicVolume(float value) 
            => _mixer.SetFloat(
                "Music",
                Mathf.Clamp(value, -80, 0));
        
        private void ChangeSoundsVolume(float value) 
            => _mixer.SetFloat(
                "SFX",
                Mathf.Clamp(value, -80, 0));
    }
}
