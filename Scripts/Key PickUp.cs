using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] private AudioSource _keyAudioSource;

    public void KeyPickUpAudio()
    {
        _keyAudioSource.Play();
    }
}
