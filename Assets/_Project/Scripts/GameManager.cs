using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //gestione chiavi
    [SerializeField] private int _keysToCompleteLevel = 1;
    public int CollectedKeys { get; private set; }
    public UnityEvent<int, int> OnKeysChanged;
    public UnityEvent OnAllKeysCollected;

    //gestione casse
    [SerializeField] private int _totalCratesOnLevel = 1;
    public int DestroyedCrates { get; private set; }
    public UnityEvent<int, int> OnCratesChanged;

    //conto alla rovescia
    [SerializeField] private float _levelTimeInSeconds = 180f;
    public float TimeRemaining { get; private set; }
    public UnityEvent<float> OnTimeChanged;

    //gestione UI
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _gameOverFirstButton;
    [SerializeField] private GameObject _victoryFirstButton;
    public bool IsGameActive { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       
        IsGameActive = true;
        Time.timeScale = 1f;
        TimeRemaining = _levelTimeInSeconds;
    }

    public void Start()
    {
        _gameOverPanel.SetActive(false);
        _victoryPanel?.SetActive(false);
        
        OnKeysChanged?.Invoke(CollectedKeys, _keysToCompleteLevel);
        OnCratesChanged?.Invoke(DestroyedCrates, _totalCratesOnLevel);
    }

    private void Update()
    {
        if (IsGameActive)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                OnTimeChanged?.Invoke(TimeRemaining);
            }
            else
            {
                Debug.Log("Tempo scaduto! Game Over.");
                TimeRemaining = 0;
                OnTimeChanged?.Invoke(TimeRemaining);
                HandleGameOver(); 
            }
        }
    }

    public void CollectKey()
    {
        CollectedKeys++;
        Debug.Log($"Chiave raccolta! Totale: {CollectedKeys}/{_keysToCompleteLevel}");
        OnKeysChanged?.Invoke(CollectedKeys, _keysToCompleteLevel);
        if (CollectedKeys >= _keysToCompleteLevel)
        {
            OnAllKeysCollected?.Invoke();
            Debug.Log("Tutte le chiavi raccolte!");
        }
    }

    public void CrateDestroyed()
    {
        DestroyedCrates++;
        OnCratesChanged?.Invoke(DestroyedCrates, _totalCratesOnLevel);
        if (DestroyedCrates >= _totalCratesOnLevel)
        {
            Debug.Log("Tutte le casse distrutte! cosa vuoi un biscotto?");
        }
    }

    public void HandleGameOver()
    {
        IsGameActive = false;
        Debug.Log("GAME OVER!");
        _gameOverPanel?.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_gameOverFirstButton);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HandleVictory()
    {
        if (!IsGameActive) return;
        IsGameActive = false;
        Time.timeScale = 0f;
        _victoryPanel?.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_victoryFirstButton);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
