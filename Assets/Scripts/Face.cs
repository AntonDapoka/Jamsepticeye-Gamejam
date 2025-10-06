using System.Collections;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private AudioClip clip;              // ����� ��������� � ����������
    [SerializeField] private GameObject objectToActivate; // ������, ������� ������ �������� ����� ������������
    private bool isActive = false;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        isActive = true;
        if (clip == null)
        {
            Debug.LogWarning($"{name}: AudioClip �� ��������!");
            return;
        }

        if (objectToActivate == null)
        {
            Debug.LogWarning($"{name}: ObjectToActivate �� ��������!");
            return;
        }

        // ��������� ��� ������� AudioSource
        if (audioSource == null)
        {
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(WaitForAudioEnd());
        
    }

    private IEnumerator WaitForAudioEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        objectToActivate.SetActive(true);
        Destroy(gameObject);

    }

    private void Update()
    {
        gameObject.SetActive(isActive);
    }
}