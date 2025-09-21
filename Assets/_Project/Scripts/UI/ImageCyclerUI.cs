using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCyclerUI : MonoBehaviour
{
    [SerializeField] private Image _screenImage;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float changeInterval = 5f;

    private int _currentIndex = 0;
    void Awake()
    {
        if (_screenImage == null || _sprites == null || _sprites.Length == 0)
        {
            Debug.LogError("controllare il setup dello schermo tutorial");
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(CycleImages());
    }

    private IEnumerator CycleImages()
    {

        while (true)
        {
            _screenImage.sprite = _sprites[_currentIndex];
            yield return new WaitForSeconds(changeInterval);
            _currentIndex = (_currentIndex + 1) % _sprites.Length;
        }
    }
}
