using System.Collections.Generic;
using UnityEngine;

public class ReferencePoint : MonoBehaviour
{
    public List<int> clickableQuads = new List<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClearList();
        GetClickableQuads(collision);
    }

    private void GetClickableQuads(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Length == 1)
        {
            int quadCode1 = int.Parse(tag);
            clickableQuads.Add(quadCode1);
        }
        else if (tag.Length > 1)
        {
            int quadCode1 = int.Parse(tag[0].ToString());
            clickableQuads.Add(quadCode1);

            int quadCode2 = int.Parse(tag[tag.Length - 1].ToString());
            clickableQuads.Add(quadCode2);
        }
    }

    private void ClearList()
    {
        clickableQuads.Clear();
    }



}//class
