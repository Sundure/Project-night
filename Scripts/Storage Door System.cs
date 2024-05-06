using UnityEngine;

public class StorageDoorSystem : MonoBehaviour
{
    [SerializeField] private bool _doorIsOpen;
    [SerializeField] private bool _needAKey;
    [SerializeField] private Animator _storageAnimator;
    [SerializeField] private Playeritems _playerItems;
    [SerializeField] private AudioSource _storageAudioSource;
    [SerializeField] private AudioClip _storageAudio;
    [SerializeField] private AudioClip _storageAudioLock;
    public void DoorCheck()
    {
        if (_doorIsOpen == false && _needAKey == false)
        {
            DoorOpen();
        }
        else if (_doorIsOpen == true)
        {
            DoorClose();
        }
        else if (_needAKey == true && _playerItems.Key == true)
        {
            _needAKey = false;
            DoorOpen();
        }
        else
        {
            DoorLock();
        }
    }
    public void DoorClose()
    {
        _doorIsOpen = false;
        _storageAnimator.SetBool("Is Open", false);
        _storageAudioSource.clip = _storageAudio;
        _storageAudioSource.Play();
    }
    public void DoorOpen()
    {
        _doorIsOpen = true;
        _storageAnimator.SetBool("Is Open", true);
        _storageAudioSource.clip = _storageAudio;
        _storageAudioSource.Play();
    }
    public void DoorLock()
    {
        _storageAudioSource.clip = _storageAudioLock;
        _storageAudioSource.Play();
    }
}
