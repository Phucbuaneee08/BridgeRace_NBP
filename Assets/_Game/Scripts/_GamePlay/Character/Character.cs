using Scriptable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Animator anim;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private GameObject dropBrickPrefab;
    [SerializeField] private GameObject brickHolder;
    [SerializeField] private LayerMask BRIDGE_BRICK;
    [SerializeField] protected GameObject spawn;

    public ColorType color;
    private string currentAnim;
    protected Stack<Brick> listBricks = new Stack<Brick>();
    private Vector3 offset;



    public virtual void OnInit()
    {
        ChangeAnim(Const.ANIM_IDLE);

    }
    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
    public void SetPosition(Transform tf)
    {
        transform.position = tf.position;
    }
    public int BrickCount()
    {
        return listBricks.Count;
    }
    public void AddBrick()
    {

        Brick newBrick = Instantiate(brickPrefab, brickHolder.transform);
        newBrick.transform.localPosition = new Vector3(0, listBricks.Count * Const.BRICK_HEIGHT, 0);
        newBrick.GetComponent<Brick>().SetColor(color);
        newBrick.transform.parent = this.brickHolder.transform;
        listBricks.Push(newBrick);
    }
    public bool CheckBrick() => listBricks.Count > 0;



    public bool CheckNextBrick()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, BRIDGE_BRICK))
        {
            return hit.collider.GetComponent<Brick>().CheckColor(color);
        }
        else return false;


    }
    public void RemoveOneBrick()
    {
        if (listBricks.Count > 0)
        {
            Destroy(listBricks.Peek().gameObject);
            listBricks.Pop();
        }
    }
    protected void ClearBrick()
    {
        while (listBricks.Count > 0)
        {
            Destroy(listBricks.Peek().gameObject);
            listBricks.Pop();
        }
    }

    private void DropBrick()
    {
        for (int i = 0; i < listBricks.Count; i++)
        {
            GameObject gb = Instantiate(dropBrickPrefab, spawn.transform.position + new Vector3(0, 1f, 0) * i, Quaternion.identity);
            Debug.Log(gb);
        }

    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    public void Victory()
    {
        ChangeAnim(Const.ANIM_DANCE);
    }
    public virtual void Falling()
    {
        DropBrick();
        ClearBrick();
        ChangeAnim(Const.ANIM_FALL);
    }

}
