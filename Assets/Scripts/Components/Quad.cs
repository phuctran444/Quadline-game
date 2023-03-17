using UnityEngine;

public class Quad : MonoBehaviour
{
    public Vector3 CornerPoint_1 { get; private set; }
    public Vector3 CornerPoint_2 { get; private set; }
    public Vector3 CornerPoint_3 { get; private set; }
    public Vector3 CornerPoint_4 { get; private set; }

    [field: SerializeField] public int QuadCode { get; private set; }

    private void Awake()
    {
        InitializeCornerPoints();
    }

    private void OnMouseDown()
    {
        if (!GameplayManager.Instance.QuadIsRotating)
        {
            GameplayManager.Instance.OnMouseClick(this);
        }
    }

    private void InitializeCornerPoints()
    {
        CornerPoint_1 = transform.position + new Vector3(-2f, 0f, 0f);
        CornerPoint_2 = transform.position + new Vector3(0f, 2f, 0f);
        CornerPoint_3 = transform.position + new Vector3(2f, 0f, 0f);
        CornerPoint_4 = transform.position + new Vector3(0f, -2f, 0f);
    }


}//class
