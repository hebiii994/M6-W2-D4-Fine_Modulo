using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private AudioClip _breakSound;

    public void DestroyCrate()
    {

        if (_breakSound != null)
        {
            AudioSource.PlayClipAtPoint(_breakSound, transform.position);
        }
        GameManager.Instance.CrateDestroyed();
        //animazione distruzione o altro
        gameObject.SetActive(false);
    }
}
