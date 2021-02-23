using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




[System.Serializable]
public class Cell
{
    public bool visited;
    public GameObject north;//1
    public GameObject east;//2
    public GameObject west;//3
    public GameObject south;//4

}

public class Generator : MonoBehaviour
{
    //[SerializeField] Camera camera;
    //[SerializeField] int RowInput;
    //[SerializeField] int ColumnInput;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject wallHolder;
    GameObject tempWall;
    [SerializeField] [Range(2, 10)] int row, column;

    int previousRow = 0, previousColumn = 0;
    private float wallLength = 1.0f;
    private Vector3 startPosition;
    private int currentCell = 0;
    public Cell[] cells;
    private int totalCells;
    private int currentNeighbour = 0;
    private int backingUp = 0;
    List<int> cellList;

    public float scale = 5f;
    public GameObject startDummie;
    public GameObject endDummie;
    int destruyendo;
    [SerializeField] Vector3 initialPosition;
    public GameObject groundObject;
    public bool easyEnd;
    public bool moveStartEnd = false;
    private void Start()
    {

        initialPosition = transform.position;
    }

    public bool CheckPreviousSize()
    {
        return (previousColumn != column || previousRow != row);
    }

    public void ChangeGroundSize()
    {
        float xScale = (float)row * scale;
        float zScale = (float)column * scale;
        Vector3 newPosition = new Vector3(0f, -0.8f, -2.5f);

        if (row % 2 != 0)
        {
            newPosition.z = 0f;
        }
        if (column % 2 != 0)
        {
            newPosition.x = 2.5f;
            //groundObject.transform.localPosition = new Vector3(2.5f, -0.8f, -2.5f);
        }

        groundObject.transform.localPosition = newPosition;
        groundObject.transform.localScale = new Vector3(xScale, groundObject.transform.localScale.y, zScale);
    }

    [ContextMenu("Generar Laberinto")]
    public void GenerateNewMaze()
    {

      //  Debug.Log(wallHolder.transform.childCount);

        //Finding the parent object of the walls if exists
        destruyendo = 0;
        initialPosition = transform.position;

        try
        {
            //row = int.Parse(RowInput.text);
            //column = int.Parse(ColumnInput.text);
            totalCells = row * column;
            //Transform cameraPosition = camera.transform;
            ChangeGroundSize();

            //int cameraHeight; 
            //if (row >= column)
            //	cameraHeight = row;
            //else
            //	cameraHeight = column;

            //cameraPosition.position = new Vector3(0,cameraHeight + 1 ,0);
            //Hidding previous maze if exist
            if (wallHolder)
            {
                int childCount = 0;
                //Debug.Log(wallHolder.transform.childCount);
                while (childCount > wallHolder.transform.childCount)
                {
                    //DestroyImmediate(wallHolder.transform.GetChild(0).gameObject);
                    wallHolder.transform.GetChild(childCount).gameObject.SetActive(false);
                }
            }
            CreateWall();
        }
        catch (System.FormatException e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// Creating wall gameobject based on rows and columns given
    /// </summary>
    public void CreateWall()
    {
        wallHolder.transform.localScale = Vector3.one;


        startPosition = new Vector3((-column / 2) + wallLength / 2, 0.0f, (-row / 2) + wallLength / 2) + initialPosition;
        Vector3 myPos = startPosition;


        if (wallHolder.transform.childCount == 0 || CheckPreviousSize())
        {

            //Destruimos lo anterior
            while (wallHolder.transform.childCount > 0)
            {
                DestroyImmediate(wallHolder.transform.GetChild(0).gameObject);
                //wallHolder.transform.GetChild(0).gameObject.SetActive(false);
            }


            Debug.Log("Creando nuevo laberinto");

            //for creating columns	
            for (int a = 0; a < row; a++)
            {
                for (int b = 0; b <= column; b++)
                {

                    myPos = new Vector3(startPosition.x + (b * wallLength) - wallLength / 2, 0.0f, startPosition.z + (a * wallLength) - wallLength / 2);
                    tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                    tempWall.name = "column " + a + "," + b;
                    tempWall.transform.parent = wallHolder.transform;

                }
            }

            //for creating rows
            for (int a = 0; a <= row; a++)
            {
                for (int b = 0; b < column; b++)
                {
                    myPos = new Vector3(startPosition.x + (b * wallLength), 0.0f, startPosition.z + (a * wallLength) - wallLength);
                    tempWall = Instantiate(wall, myPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                    tempWall.name = "row " + a + "," + b;
                    tempWall.transform.parent = wallHolder.transform;

                }
            }
        }
        CreateCells();

        wallHolder.transform.localScale = Vector3.one * scale;
        previousRow = row;
        previousColumn = column;
    }

    /// <summary>
    /// Assigning created walls to the cells direction (north,east,west,south)
    /// </summary>
    public void CreateCells()
    {
        cellList = new List<int>();
        int children = wallHolder.transform.childCount;
        GameObject[] allWalls = new GameObject[children];
        cells = new Cell[totalCells];

        int eastWestProccess = 0;
        int childProcess = 0;
        int termCount = 0;
        int cellProccess = 0;

        //Assigning all the walls to the allwalls array
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
            allWalls[i].SetActive(true);
        }

        //Assigning walls to the cells
        for (int j = 0; j < column; j++)
        {
            cells[cellProccess] = new Cell();

            cells[cellProccess].west = allWalls[eastWestProccess];
            cells[cellProccess].south = allWalls[childProcess + (column + 1) * row];
            termCount++;
            childProcess++;
            cells[cellProccess].north = allWalls[(childProcess + (column + 1) * row) + column - 1];
            eastWestProccess++;
            cells[cellProccess].east = allWalls[eastWestProccess];

            cellProccess++;
            if (termCount == column && cellProccess < cells.Length)
            {
                eastWestProccess++;
                termCount = 0;
                j = -1;
            }

        }
        CreateMaze();
    }

    /// <summary>
    /// Getting a random neighbour if not visited and wall between them
    /// </summary>
    void GiveMeNeighbour()
    {
        int length = 0;
        int[] neighbour = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;
        check = (currentCell + 1) / column;
        check -= 1;
        check *= column;
        check += column;
        //north
        if (currentCell + column < totalCells)
        {
            if (cells[currentCell + column].visited == false)
            {
                neighbour[length] = currentCell + column;
                connectingWall[length] = 1;
                length++;
            }
        }
        //east
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbour[length] = currentCell + 1;
                connectingWall[length] = 2;
                length++;
            }
        }
        //west
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbour[length] = currentCell - 1;
                connectingWall[length] = 3;
                length++;
            }
        }
        //south
        if (currentCell - column >= 0)
        {
            if (cells[currentCell - column].visited == false)
            {
                neighbour[length] = currentCell - column;
                connectingWall[length] = 4;
                length++;
            }
        }

        //Getting random neighbour and destroying the wall
        if (length != 0)
        {
            int randomNeighbour = Random.Range(0, length);
            currentNeighbour = neighbour[randomNeighbour];
            DestroyWall(connectingWall[randomNeighbour]);


        }
        else if (backingUp > 0)
        {
            currentCell = cellList[backingUp];
            backingUp--;
        }
    }
    int visitedCells;
    void CreateMaze()
    {
        bool startedBuilding = false;
        visitedCells = 0;
        while (visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighbour();
                if (!cells[currentNeighbour].visited && cells[currentCell].visited)
                {
                    int randomNeighbour = Random.Range(0, 5);
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    cellList.Add(currentCell);
                    currentCell = currentNeighbour;

                    if (cellList.Count > 0)
                        backingUp = cellList.Count - 1;
                }
            }
            else
            {
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;

            }
        }


    }



    void DestroyWall(int neighbour)
    {
        Vector3 position = Vector3.zero;

        destruyendo++;
        switch (neighbour)
        {
            //case 1 means north wall
            case 1:
                if (easyEnd)
                    position = cells[currentCell].north.transform.localPosition;
                else
                    position = cells[currentCell].north.transform.localPosition - (Vector3.back / 2);
                cells[currentCell].north.SetActive(false);
                //Destroy(cells[currentCell].north);
                break;

            //case 2 means east wall
            case 2:
                if ( easyEnd)
                    position = cells[currentCell].east.transform.localPosition;
                else
                    position = cells[currentCell].east.transform.localPosition - (Vector3.left / 2);
                cells[currentCell].east.SetActive(false);
                //Destroy(cells[currentCell].east);
                break;

            //case 3 means west wall
            case 3:
                if (easyEnd)
                    position = cells[currentCell].west.transform.localPosition;
                else
                    position = cells[currentCell].west.transform.localPosition - (Vector3.right / 2);
                cells[currentCell].west.SetActive(false);
                //Destroy(cells[currentCell].west);
                break;

            //case 4 means south wall
            case 4:
                if (easyEnd)
                    position = cells[currentCell].south.transform.localPosition;
                else
                    position = cells[currentCell].south.transform.localPosition - (Vector3.forward / 2);
                cells[currentCell].south.SetActive(false);
                //Destroy(cells[currentCell].south);	
                break;

            default:
                break;
        }


        if (moveStartEnd)
        {
            if (destruyendo == 1)
            {
                startDummie.transform.localPosition = (position * scale);
            }

            //if(destruyendo == 2)
            if (destruyendo == totalCells - 1)
            {
                //Debug.Log("Valor "+ destruyendo);
                endDummie.transform.localPosition = position * scale;
            }

        }
    }
}





