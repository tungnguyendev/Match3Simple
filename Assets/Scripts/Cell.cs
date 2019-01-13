using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private bool _isReload = false;

    public bool IsReload
    {
        get
        {
            return _isReload;
        }
        set
        {
            _isReload = value;
        }
    }

    private int _row;

    public int Row
    {
        get
        {
            return _row;
        }
        set
        {
            _row = value;
        }
    }

    private int _column;

    public int Column
    {
        get
        {
            return _column;
        }
        set
        {
            _column = value;
        }
    }


    private int _cellType;

    public int CellType
    {
        set
        {
            _cellType = value;
            if(_cellType == 0)
            {
                isDisable = true;
                Utils.Board[_row, _column] = -2;
                this.transform.GetComponent<Image>().color = new Color(185 / 255f, 185 / 255f, 185 / 255f);
                image.gameObject.SetActive(false);
            }
            else
            {
                //Random cell
                var rnd = Random.Range(0, 5);

                switch (rnd)
                {
                    case 0:
                        image.sprite = sprites[0];
                        break;
                    case 1:
                        image.sprite = sprites[1];
                        break;
                    case 2:
                        image.sprite = sprites[2];
                        break;
                    case 3:
                        image.sprite = sprites[3];
                        break;
                    case 4:
                        image.sprite = sprites[4];
                        break;
                    default:
                        break;
                }

                Utils.Board[_row, _column] = rnd;
            }
        }
    }

    public Sprite[] sprites;
    public Image image;

    //if isDisable == true, the cell will be can't select.
    private bool isDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetValue()
    {
        Utils.Board[_row, _column] = -1;
    }

    public void Reload(int num)
    {
        
        switch (num)
        {
            case 0:
                image.sprite = sprites[0];
                break;
            case 1:
                image.sprite = sprites[1];
                break;
            case 2:
                image.sprite = sprites[2];
                break;
            case 3:
                image.sprite = sprites[3];
                break;
            case 4:
                image.sprite = sprites[4];
                break;
            default:
                break;
        }
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
    }

    public void Btn_Click()
    {
        //In the case game handle after moving
        if(!Utils.isCanSelect)
        {
            return;
        }

        if(Utils.isSelected)
        {
            StartCoroutine(WaitToHandle());
        }
        else
        {
            Utils.RowMove = Row;
            Utils.ColMove = Column;
            Utils.isSelected = true;
        }
    }

    IEnumerator WaitToHandle()
    {
        //Check if is moving possible
        Utils.Swap(Row, Column, Utils.RowMove, Utils.ColMove);
        yield return new WaitForSeconds(1);
        if (!Utils.CheckMatch())
        {
            Utils.Swap(Row, Column, Utils.RowMove, Utils.ColMove);
        }
        else
        {
            StartCoroutine(WaitToReload());
        }

        Utils.isSelected = false;
    }
}
