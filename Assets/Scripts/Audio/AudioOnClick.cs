using UnityEngine;
using UnityEngine.EventSystems;

namespace Audio
{
    public class AudioOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip sound;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (sound is null) return;
            SoundManager.Instance.PlaySound(sound);
        }
    }
}