using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NormalPhaseManager : Singleton<NormalPhaseManager>
{
    [Header("References")]
    [SerializeField] RhythmBrick currentBrick;
    public List<RhythmBrick> allBrick;
    [Header("Parameter")]
    public bool isNormalPhase = false;
    public NormalSpawningPattern song;
    #region Timer
    public float elaspedTime = 0f;
    #endregion

    private void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!isNormalPhase) return;

        elaspedTime += Time.deltaTime;

        if (allBrick.Count > 0)
            currentBrick = allBrick[0];
    }

    void StartGame()
    {
        isNormalPhase = true;
    }

    public void Press(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            currentBrick.Check();
        }
    }
}
