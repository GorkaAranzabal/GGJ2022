using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MyMonoBehaviour
{
    public bool sortingOut;
    public event Action OnGameCompleted;

    // Start is called before the first frame update
    void Start()
    {
        crono.OnCronoCompleted += CronoCompleted;
        uiController.mensajeInicioRonda.text = "Now sort out the house!";
    }

    private void CronoCompleted()
    {
        if (sortingOut)
        {
            StartCoroutine(uiController.mostrarMnsInicial());

            //Message
            uiController.mensajeInicioRonda.text = "Now make a mess!";
            StartCoroutine(uiController.ShowTotalRoundPointsText());
            if (PhotonNetwork.IsMasterClient)
                uiController.totalRoundPointsText.text = "" + InGameController.instance.pointFirstRoundPlayer1 + " points";
            else
                uiController.totalRoundPointsText.text = "" + InGameController.instance.pointFirstRoundPlayer2 + " points";

            uiController.rondaText.text = "Round: 2/2";
            crono.totalTimer = ProjectSettings.countdownRoundTime;
            crono.timerIsRunning = true;
            sortingOut = !sortingOut;
        }
        else
        {
            StartCoroutine(uiController.mostrarMnsInicial());
            uiController.mensajeInicioRonda.text = "Game Over";

            StartCoroutine(uiController.ShowTotalRoundPointsText());
            if (PhotonNetwork.IsMasterClient)
                uiController.totalRoundPointsText.text = "" + InGameController.instance.pointSecondRoundPlayer1 + " points";
            else
                uiController.totalRoundPointsText.text = "" + InGameController.instance.pointSecondRoundPlayer2 + " points";

            StartCoroutine(uiController.ShowGameOverPanel());

            if (OnGameCompleted != null)
                OnGameCompleted();
        }
    }
}
