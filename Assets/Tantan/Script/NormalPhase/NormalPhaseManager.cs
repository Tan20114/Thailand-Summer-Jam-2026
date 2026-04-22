using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using Lean.Pool;

public class NormalPhaseManager : Singleton<NormalPhaseManager>
{
    public event Action OnChangeNote;

    [Header("References")]
    public RhythmBrick currentBrick;
    public Vector2 brickStartPoint;
    public List<RhythmBrick> allBrick;
    public Minigame currentMinigame;
    [SerializeField] Minigame[] minigames;
    int currentMinigameIndex = 0;
    [Header("Parameter")]
    public bool isNormalPhase = false;
    public NormalSpawningPattern song;

    private void Start()
    {
        StartGame();
        brickStartPoint = GameObject.FindGameObjectWithTag("BrickPool").transform.position;
        currentMinigame = minigames[currentMinigameIndex];
    }

    void Update()
    {
        if (!isNormalPhase) return;

        GameManager.Instance.elaspedTime += Time.deltaTime;

        if (allBrick.Count > 0)
            currentBrick = allBrick[0];
        else
            currentBrick = null;

        if (GameManager.Instance.elaspedTime > (song.halfTime / minigames.Length) * (currentMinigameIndex + 1))
        {
            ResetMinigame();
            if (currentMinigameIndex >= minigames.Length - 1) return;
            currentMinigameIndex++;
            currentMinigame = minigames[currentMinigameIndex];
            currentMinigame.gameObject.SetActive(true);
        }

        currentMinigame.Tick();
    }

    public void ResetMinigame()
    {
        foreach (Minigame m in minigames)
            m.gameObject.SetActive(false);
    }

    void StartGame()
    {
        isNormalPhase = true;
    }

    public void Press(InputAction.CallbackContext context)
    {
        if(context.started && currentBrick)
        {
            currentBrick.Check();
            brickStartPoint = currentBrick.transform.position;
        }
    }

    public void OnPerfect() => currentMinigame.Perfect();
    public void OnGood() => currentMinigame.Good();
    public void OnBad() => currentMinigame.Bad();
    public void OnMiss() => currentMinigame.Miss();

    public void OnNoteChanged() => OnChangeNote?.Invoke();
}
