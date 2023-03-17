using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] _quads;
    [SerializeField] private GameObject[] _blackLines;
    [SerializeField] private Transform[] _whiteLines;
    [SerializeField] private ReferencePoint[] _referencePoints;
    [SerializeField] private GameObject _levelTransitioner;

    private void Awake()
    {
        GameplayManager.Instance.quads = _quads;
        GameplayManager.Instance.whiteLines = _whiteLines;
        GameplayManager.Instance.blackLines = _blackLines;
        GameplayManager.Instance.referencePoints = _referencePoints;
        GameplayManager.Instance.levelTransitioner = _levelTransitioner;
        GameplayManager.Instance.pointsToWin = _whiteLines.Length;

        SceneController.Instance.LoadNextSceneAsync();
    }





}//class
