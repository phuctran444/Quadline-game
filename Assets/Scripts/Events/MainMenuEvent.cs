using UnityEngine;

public class MainMenuEvent : MonoBehaviour
{
    private void DisassembleTitle()
    {
        StartCoroutine(MainMenuController.Instance.DisassembleTitle());
    }

    private void AssembleLine()
    {
        MainMenuController.Instance.AssembleLine();
    }

    private void DisassembleLine()
    {
        StartCoroutine(MainMenuController.Instance.DisassembleLine());
    }

    private void RotateSubQuad()
    {
        MainMenuController.Instance.RotateSubQuad();
    }

    private void ActivateScene()
    {
        StartCoroutine(SceneController.Instance.ActivateNextScene());
    }





}//class
