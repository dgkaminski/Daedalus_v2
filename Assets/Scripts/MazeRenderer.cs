using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    [Range(1, 100)]
    private int width = 10;

    [SerializeField]
    [Range(1, 100)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform lightPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    /*[SerializeField]
    private double factor = 1.0;*/

    // Start is called before the first frame update
    void Start()
    {
        DrawFloor();

        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    private void DrawFloor() {
        var floor = Instantiate(floorPrefab, transform) as Transform;
        floor.position = new Vector3(0, 0, 0);
        floor.localScale = new Vector3(width, 1, height);
    }

    private void Draw(WallState[,] maze) {

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var cell = maze[i,j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP)) {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LIGHTUP))
                {
                    var topLight = Instantiate(lightPrefab, transform) as Transform;
                    topLight.position = position + new Vector3(0, 0, size / 2);
                }

                if (cell.HasFlag(WallState.LEFT)) {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (cell.HasFlag(WallState.LIGHTLEFT))
                {
                    var leftLight = Instantiate(lightPrefab, transform) as Transform;
                    leftLight.position = position + new Vector3(-size / 2, 0, 0);
                    leftLight.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1) {
                    if (cell.HasFlag(WallState.RIGHT)) {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if (cell.HasFlag(WallState.LIGHTRIGHT))
                    {
                        var rightLight = Instantiate(lightPrefab, transform) as Transform;
                        rightLight.position = position + new Vector3(+size / 2, 0, 0);
                        rightLight.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0) {
                    if (cell.HasFlag(WallState.DOWN)) {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }

                    if (cell.HasFlag(WallState.LIGHTDOWN))
                    {
                        var bottomLight = Instantiate(lightPrefab, transform) as Transform;
                        bottomLight.position = position + new Vector3(0, 0, -size / 2);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private int Round(double d) {
        return (int) (d - (d % 1)) + Eval(d % 1 >= 0.5);
    }

    private int Eval(bool b) {
        if (b) {
            return 1;
        } else {
            return 0;
        }
    }*/
}
