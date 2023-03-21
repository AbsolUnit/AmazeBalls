using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MazeGen : MonoBehaviour
{
    private GameObject gm;
    public GameObject wall;
    public GameObject floor;
    public GameObject ball;
    public GameObject start;
    public GameObject end;
    public GameObject scaleCollect;
    public Material floorMat;
    public Transform parent;

    public bool scaling;
    public bool[,] points;
    public bool[,] hwalls;
    public bool[,] vwalls;
    public int size;
    public bool generateWallMaze = false;
    private int visitedCount = 0;
	[Range(4f,15f)]
    public float scale = 4;
    [HideInInspector] public float posOffset;
    private Stack<int[]> mazeStack;
    

    int rand(int size)
    {
        return (int)Random.Range(0.0f, (float)size - 0.001f);
    }
    int[] pick(List<int[]> neighbors)
    {
        int n = rand(neighbors.Count);
        return neighbors[n];
    }
    bool checkValid(int[] point)
    {
        bool ival = 0 <= point[0] && point[0] < size;
        bool jval = 0 <= point[1] && point[1] < size;
        return ival && jval;
    }

    int[] pickNeighbor(int[] p)
    {
        return pick(validAvailableNeighbors(p));
    }
    List<int[]> validNeighbors(int[] p)
	{
        var i = p[0];
        var j = p[1];
        List<int[]> neighbors = new List<int[]> { };
        if (checkValid(new int[] { i - 1, j })){
            neighbors.Add(new int[] { i - 1, j });
		}
        if (checkValid(new int[] { i + 1, j }))
        {
            neighbors.Add(new int[] { i + 1, j });
        }
        if (checkValid(new int[] { i , j + 1}))
        {
            neighbors.Add(new int[] { i, j + 1 });
        }
        if (checkValid(new int[] { i, j - 1 }))
        {
            neighbors.Add(new int[] { i, j - 1 });
        }
        return neighbors;
    }
	bool neighborsAvailable(int[] p)
	{
        bool available = false;
        foreach (int[] neighbor in validNeighbors(p))
		{
			if (!isvisited(neighbor))
			{
                available = true;
			}
		}
        return available;
	}
	List<int[]> validAvailableNeighbors(int[] p)
	{
        List<int[]> neighbors = new List<int[]> { };
        foreach (int[] neighbor in validNeighbors(p))
		{
			if (!isvisited(neighbor))
			{
                neighbors.Add(neighbor);
			}
		}
        return neighbors;
	}
    void visit(int[] p)
    {
        points[p[0], p[1]] = true;
        visitedCount++;
        //printCurr(p);
    }
    string pointString(int[] p)
    {
        return p[0].ToString() + "," + p[1].ToString();
    }
    void printCurr(int[] p)
    {
        string s = pointString(p);
        // Debug.Log("visiting: " + unvisited.ToString() + " : " + s);
    }
    bool isvisited(int[] p)
    {
        return points[p[0], p[1]];
    }
    void removeWall(int[] p1, int[] p2)
    {
        int dx = p2[0] - p1[0];
        int dy = p2[1] - p1[1];
        if (dx == -1)
        {
            var i = p1[0];
            var j = p1[1];
            vwalls[i, j] = false;
            // Debug.Log("Removing vwall " + i.ToString() + "," + j.ToString());
        }
        if (dy == -1)
        {
            var i = p1[0];
            var j = p1[1];
            hwalls[i, j] = false;
            // Debug.Log("Removing hwall " + i.ToString() + "," + j.ToString());
        }
        if (dx == 1)
        {
            var i = p1[0] + dx;
            var j = p1[1];
            vwalls[i, j] = false;
            // Debug.Log("Removing vwall " + i.ToString() + "," + j.ToString());
        }
        if (dy == 1)
        {
            var i = p1[0];
            var j = p1[1] + dy;
            hwalls[i, j] = false;
            // Debug.Log("Removing hwall " + i.ToString() + "," + j.ToString());
        }
    }
    void BackTrack()
    {
        // start by picking a random cell.
        // int[] p = {rand(size), rand(size)};
        int[] p = { 0, 0 };
        visit(p);
		mazeStack.Push(p);
        while (visitedCount < size * size)
        {
            if (neighborsAvailable(p))
			{
                var next = pickNeighbor(p);
                visit(next);
                removeWall(p, next);
                p = next;
                mazeStack.Push(p);
			}
			else
			{
                mazeStack.Pop();
                p = mazeStack.Peek();
			}
        }
    }
    void fill(bool[,] M, bool b)
    {
        for (int i = 0; i < size + 1; i++)
        {
            for (int j = 0; j < size + 1; j++)
            {
                M[i, j] = b;
            }
        }
    }
    void instantiateWallMaze()
    {
        for (int i = 0; i < size + 1; i++)
        {
            for (int j = 0; j < size + 1; j++)
            {
                if (hwalls[i, j] && i < size)
                {
                    var prefab = Instantiate(wall, new Vector3((i * scale) - posOffset, 0.5f, (j * scale - scale / 2) - posOffset), Quaternion.identity, parent);
                    prefab.transform.localScale = new Vector3(scale * 1.25f, scale / 2, scale / 4);
                    prefab.GetComponent<MeshRenderer>().material.renderQueue = 3000;
                    prefab.tag = "hWall";
                    prefab.GetComponent<WallScale>().maze = gameObject;
                    prefab.GetComponent<WallScale>().pos = new int[] {i , j};
                    prefab.name = "hwall_{" + i.ToString() + "," + j.ToString() + "}";
                }
                if (vwalls[i, j] && j < size)
                {
                    var prefab = Instantiate(wall, new Vector3((i * scale - scale / 2) - posOffset, 0.5f, (j * scale) - posOffset), Quaternion.identity, parent);
                    prefab.transform.localScale = new Vector3(scale * 1.25f, scale / 2, scale / 4);
                    prefab.transform.Rotate(new Vector3(0, 90, 0));
                    prefab.tag = "vWall";
                    prefab.GetComponent<WallScale>().maze = gameObject;
                    prefab.GetComponent<WallScale>().pos = new int[] { i, j };
                    prefab.name = "vwall_{" + i.ToString() + "," + j.ToString() + "}";
                }
            }
        }
    }
    void Start()
    {
        //Setup
        gm = GameObject.FindWithTag("GameManager");
        gm.GetComponent<ScaleCoolDown>().maze = gameObject.GetComponent<MazeGen>();
        mazeStack = new Stack<int[]> { };
        posOffset = ((scale * size) / 2) - scale / 2;
        points = new bool[size, size];
        hwalls = new bool[size + 1, size + 1];
        vwalls = new bool[size + 1, size + 1];
        fill(hwalls, true);
        fill(vwalls, true);
        //Maze Algorithm
        BackTrack();
        //Make Floor
        var prefab = Instantiate(floor, new Vector3(0, -1, 0), Quaternion.identity, parent);
        prefab.transform.localScale = new Vector3(size * scale, 1, size * scale);
        prefab.name = "MazeFloor";
        //Make Ball
        prefab.GetComponent<WallScale>().maze = gameObject;
        prefab = Instantiate(ball, new Vector3(-posOffset, 1, -posOffset), Quaternion.identity, parent);
        prefab.name = "Ball";
        gm.GetComponent<GameManager>().ball = prefab.GetComponent<BallMove>();
        prefab.GetComponent<WallScale>().maze = gameObject;
        GameObject.FindWithTag("VirtCam").GetComponent<CinemachineVirtualCamera>().Follow = prefab.transform;
        //Make Start pad
        prefab = Instantiate(start, new Vector3(-posOffset, -0.4f, -posOffset), Quaternion.identity, parent);
        prefab.transform.localScale = new Vector3(scale - (scale / 4) - 0.2f, 0.1f, scale - (scale / 4) - 0.2f);
        prefab.name = "Start";
        prefab.GetComponent<WallScale>().maze = gameObject;
        //Make End pad
        prefab = Instantiate(end, new Vector3(posOffset, -0.4f, posOffset), Quaternion.identity, parent);
        prefab.transform.localScale = new Vector3(scale - (scale / 4) - 0.2f, 0.1f, scale - (scale / 4) - 0.2f);
        prefab.name = "End";
        prefab.GetComponent<WallScale>().maze = gameObject;
        //Make Collectables

        var collectionPoints = new bool[size, size];
        for (int i = 0; i < size + 1; i++)
		{
            for (int j = 0; j < size + 1; j++)
            {
                if (rand(10) == 5 && i <size && j < size)
				{
                    prefab = Instantiate(scaleCollect, new Vector3((i * scale) - posOffset, 1, (j * scale) - posOffset), Quaternion.identity, parent);
                    prefab.GetComponent<ScaleCollect>().maze = gameObject.GetComponent<MazeGen>();
                    prefab.GetComponent<ScaleCollect>().timer = gm.GetComponent<Timer>();
                    prefab.GetComponent<ScaleCollect>().coolDown = gm.GetComponent<ScaleCoolDown>();
                    prefab.GetComponent<ScaleCollect>().gm = gm.GetComponent<GameManager>();
                    prefab.GetComponent<WallScale>().maze = gameObject;
                    prefab.name = "ScaleCollect_{" + i.ToString() + "," + j.ToString() + "}";
                }
            }
        }

        
        //Make Walls
        if (generateWallMaze) instantiateWallMaze();
    }
}