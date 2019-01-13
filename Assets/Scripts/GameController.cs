using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int[,] _board = new int[,]
    {
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
        { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
        { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
        { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0},
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0}
    };

    private List<int> lstDisappear = new List<int>();

    public RectTransform Board;

    public GameObject Cell;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        for (int i = 0; i < _board.Length; i++)
        {
            var a = _board[i / 10, i % 10];
            GameObject cell = Instantiate(Cell);
            cell.transform.SetParent(Board.transform, false);

            Cell cellScript = cell.GetComponent<Cell>();
            cellScript.Row = i / 10;
            cellScript.Column = i % 10;
            cellScript.CellType = _board[i / 10, i % 10];

            Utils.arrCellScripts[i / 10, i % 10] = cellScript;
        }

        StartCoroutine(WaitToReload());
    }


    IEnumerator WaitToReload()
    {
        Utils.isCanSelect = false;
        while (Utils.CheckMatch())
        {
            Utils.Reload();
            yield return new WaitForSeconds(2);
        }

        Utils.isCanSelect = true;
        Debug.Log("Handle Success!");
    }
}
