using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialCanvas;
    [SerializeField] private float _displayDuration = 5f;

    private bool isShowing = false;

    void Start()
    {
        if (_tutorialCanvas != null)
        {
            _tutorialCanvas.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowing)
        {
            StartCoroutine(ShowTutorialSequence());
        }
    }

    private IEnumerator ShowTutorialSequence()
    {
        isShowing = true;
        if (_tutorialCanvas != null)
        {
            _tutorialCanvas.SetActive(true);
        }
        yield return new WaitForSeconds(_displayDuration);
        if (_tutorialCanvas != null)
        {
            Destroy( _tutorialCanvas );
            isShowing = false;
        }
    }
}
