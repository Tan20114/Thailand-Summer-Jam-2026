using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class NormalPhaseManager : Singleton<NormalPhaseManager>
{
    public event Action OnChangeNote;

    [Header("References")]
    public RhythmBrick currentBrick;
    public Vector2 brickStartPoint;
    public List<RhythmBrick> allBrick;
    public Minigame currentMinigame;
    [Header("Parameter")]
    public bool isNormalPhase = false;
    public NormalSpawningPattern song;
    #region Timer
    public float elaspedTime = 0f;
    #endregion

    private void Start()
    {
        StartGame();
        brickStartPoint = GameObject.FindGameObjectWithTag("BrickPool").transform.position;
    }

    void Update()
    {
        if (!isNormalPhase) return;

        elaspedTime += Time.deltaTime;

        if (allBrick.Count > 0)
            currentBrick = allBrick[0];
        else
            currentBrick = null;

        currentMinigame.Tick();
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
