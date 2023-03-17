using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] private GameObject[] _lineMotions;

    public GameObject[] quads;
    public GameObject[] blackLines;
    public Transform[] whiteLines;
    public ReferencePoint[] referencePoints;
    public GameObject levelTransitioner;
    public int pointsToWin;

    private bool _isUpdated = false;
    public bool QuadIsRotating { get; private set; } = false;
    public bool LineIsMoving { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void OnMouseClick(Quad quad)
    {
        QuadIsRotating = true;
        _isUpdated = false;
        HandleMouseClick(quad);
    }

    private void HandleMouseClick(Quad quad)
    {
        int quadCode = quad.QuadCode;

        List<ReferencePoint> movableReferencePoints = GetMovableReferencePoints(quadCode);

        if (movableReferencePoints.Count == 0)
        {
            QuadIsRotating = false;
            return;
        }

        StartCoroutine(RotateCoroutine(quad, movableReferencePoints));
    }

    private IEnumerator RotateCoroutine(Quad quad, List<ReferencePoint> movableReferencePoints)
    {
        foreach (ReferencePoint point in movableReferencePoints)
        {
            StartCoroutine(MoveCoroutine(quad, point));
        }

        while (!_isUpdated)
        {
            yield return null;
        }

        int currentFinishedPoints = 0;
        for (int i = 0; i < pointsToWin; i++)
        {
            for (int j = 0; j < pointsToWin; j++)
            {
                if (referencePoints[i].transform.position == whiteLines[j].transform.position)
                {
                    currentFinishedPoints++;
                    break;
                }
            }
        }

        if (currentFinishedPoints == pointsToWin)
        {
            yield return new WaitForSeconds(0.5f);
            DisableAllObjects();
            levelTransitioner.SetActive(true);
        }

        QuadIsRotating = false;
    }

    private IEnumerator MoveCoroutine(Quad quad, ReferencePoint referencePoint)
    {
        Transform referencePointTransform = referencePoint.transform;

        int pointIndex = GetPointIndex(quad, referencePointTransform);
        int referencePointIndex = GetReferencePointIndex(referencePoint);

        PlayMotion(quad.transform.position, pointIndex - 1);
        DisableBlackLine(referencePointIndex);

        while (LineIsMoving)
        {
            yield return null;
        }

        EnableBlackLine(quad, pointIndex, referencePointIndex);
        UpdateReferencePointPosition(quad, referencePointTransform);

        yield return new WaitForFixedUpdate();
        _isUpdated = true;
    }

    private void DisableAllObjects()
    {
        foreach (GameObject blackLine in blackLines)
        {
            blackLine.SetActive(false);
        }
        foreach (Transform whiteLine in whiteLines)
        {
            whiteLine.gameObject.SetActive(false);
        }
        foreach (GameObject quad in quads)
        {
            quad.SetActive(false);
        }
    }

    private List<ReferencePoint> GetMovableReferencePoints(int quadCode)
    {
        List<ReferencePoint> movableReferencePoints = new List<ReferencePoint>();

        foreach (ReferencePoint point in referencePoints)
        {
            foreach (int code in point.clickableQuads)
            {
                if (code == quadCode)
                {
                    movableReferencePoints.Add(point);
                    break;
                }
            }
        }

        return movableReferencePoints;
    }

    private int GetPointIndex(Quad quad, Transform referencePoint)
    {
        int pointIndex = 0;
        if (referencePoint.position == quad.CornerPoint_1)
        {
            pointIndex = 1;
        }
        else if (referencePoint.position == quad.CornerPoint_2)
        {
            pointIndex = 2;
        }
        else if (referencePoint.position == quad.CornerPoint_3)
        {
            pointIndex = 3;
        }
        else if (referencePoint.position == quad.CornerPoint_4)
        {
            pointIndex = 4;
        }
        return pointIndex;
    }

    private int GetReferencePointIndex(ReferencePoint referencePoint)
    {
        string name = referencePoint.name;
        int index = int.Parse((name[name.Length - 1].ToString()));
        return index;
    }

    private void PlayMotion(Vector3 position, int pointIndex)
    {
        LineIsMoving = true;
        _lineMotions[pointIndex].transform.position = position;
        _lineMotions[pointIndex].SetActive(true);
    }

    private void DisableBlackLine(int index)
    {
        blackLines[index - 1].SetActive(false);
    }

    private void EnableBlackLine(Quad quad, int pointIndex, int referenceIndex)
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        if (pointIndex == 1)
        {
            position = quad.CornerPoint_2;
            rotation = Quaternion.AngleAxis(90f, transform.forward);
        }
        else if (pointIndex == 2)
        {
            position = quad.CornerPoint_3;
            rotation = Quaternion.AngleAxis(0f, transform.forward);
        }
        else if (pointIndex == 3)
        {
            position = quad.CornerPoint_4;
            rotation = Quaternion.AngleAxis(90f, transform.forward);
        }
        else if (pointIndex == 4)
        {
            position = quad.CornerPoint_1;
            rotation = Quaternion.AngleAxis(0f, transform.forward);
        }

        blackLines[referenceIndex - 1].transform.SetPositionAndRotation(position, rotation);
        blackLines[referenceIndex - 1].SetActive(true);
    }

    private void UpdateReferencePointPosition(Quad quad, Transform referencePoint)
    {
        if (referencePoint.position == quad.CornerPoint_1)
        {
            referencePoint.position = quad.CornerPoint_2;
        }
        else if (referencePoint.position == quad.CornerPoint_2)
        {
            referencePoint.position = quad.CornerPoint_3;
        }
        else if (referencePoint.position == quad.CornerPoint_3)
        {
            referencePoint.position = quad.CornerPoint_4;
        }
        else if (referencePoint.position == quad.CornerPoint_4)
        {
            referencePoint.position = quad.CornerPoint_1;
        }
    }








}//class
