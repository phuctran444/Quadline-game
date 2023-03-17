using System.Collections;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{
    [SerializeField] private Animator _mainMenuAnim;

    public IEnumerator DisassembleTitle()
    {
        yield return new WaitForSeconds(1.5f);
        _mainMenuAnim.SetTrigger("DisassembleTitle");
    }

    public void AssembleLine()
    {
        _mainMenuAnim.SetTrigger("AssembleLine");
    }

    public IEnumerator DisassembleLine()
    {
        yield return new WaitForSeconds(0.25f);
        _mainMenuAnim.SetTrigger("DisassembleLine");
    }

    public void RotateSubQuad()
    {
        _mainMenuAnim.SetTrigger("RotateSubQuad");
    }

}//class
