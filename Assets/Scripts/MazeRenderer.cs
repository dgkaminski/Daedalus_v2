using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class MazeRenderer : MonoBehaviour
{
    //[SerializeField]
    //[Range(1, 100)]
    public int width = 10;

    //[SerializeField]
    //[Range(1, 100)]
    public int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform lightPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    [Range(0, 1)]
    private float horizOffset = 0.0f;

    [SerializeField]
    [Range(0, 5)]
    private float vertOffset = 0.0f;

    [SerializeField]
    [Range(0, 1)]
    private float lightChance = 0.5f;

    [SerializeField]
    [Range(1, 50)]
    private int mapWallWidth = 0;

    [SerializeField]
    [Range(1, 100)]
    private int mapCellWidth = 0;

    [SerializeField]
    private Texture2D wallTexture = null;

    /*
    [SerializeField]
    private Color wallColor;

    [SerializeField]
    private Color cellColor;

    [SerializeField]
    private Color nodeColor;
    */

    
    private Color wallColor = Color.black;

    
    private Color cellColor = Color.white;

    
    private Color nodeColor = Color.green;

    public MazeRenderer(/*int width = defaultWidth, int height = defaultHeight, Color wall = new Color(0, 0, 0, 1), Color cell = Color.white, Color node = Color.green*/)
    {
        
    }

    /**
     * Generates the entire maze
     */
    public void StartMaze()
    {
        DrawFloor();

        var maze = MazeGenerator.Generate(width, height, lightChance);
        Draw(maze);

        GameObject.Find("Player").GetComponent<Transform>().position = new Vector3(-width / 2, 0.5f, -height / 2);
    }

    /**
     * Adds the floor of the maze to the scene
     */
    private void DrawFloor()
    {
        var floor = Object.Instantiate<Transform>(floorPrefab, transform) as Transform;
        floor.position = new Vector3(0, 0, 0);
        floor.localScale = new Vector3(width, 1, height);

        /*floor = Instantiate(floorPrefab, transform) as Transform;
        floor.position = new Vector3(0, -6.48f + 9, 0);
        floor.localScale = new Vector3(width, 1, height);*/
    }

    /**
     * Generates the maze in unity and draws and sends a map of it.
     */
    private void Draw(WallState[,] maze)
    {

        //Sets the dimensions of the map
        int pictureWidth = width * (mapCellWidth + mapWallWidth) + mapWallWidth;
        int pictureHeight = height * (mapCellWidth + mapWallWidth) + mapWallWidth;

        //Generates the map
        Texture2D map = new Texture2D(pictureWidth, pictureHeight);

        //Sets the default map
        for (int i = 0; i < pictureHeight; i++)
        {
            for (int j = 0; j < pictureWidth; j++)
            {
                int a = i % (mapCellWidth + mapWallWidth);
                int b = j % (mapCellWidth + mapWallWidth);

                if (a < mapWallWidth && b < mapWallWidth)
                {
                    map.SetPixel(j, i, nodeColor);
                }
                else if (a >= mapWallWidth && b >= mapWallWidth)
                {
                    map.SetPixel(j, i, cellColor);
                }
                else
                {
                    map.SetPixel(j, i, wallColor);
                }
            }
        }

        //wallTexture = Tesselate(wallTexture, 5, 5);

        //Draws the map and the maze in the engine
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = maze[i,j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                //Checks if the cell has the UP flag, then if it does it adds a wall in the up position
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                //Otherwise, it removes the wall from the map
                else
                {
                    for (int a = 0; a < mapCellWidth; a++)
                    {
                        for (int b = 0; b < mapWallWidth; b++)
                        {
                            map.SetPixel(a + (i * (mapCellWidth + mapWallWidth)) + mapWallWidth, b + ((j + 1) * (mapCellWidth + mapWallWidth)), cellColor);
                        }
                    }
                }

                //Checks if there should be a lantern in the up position
                if (cell.HasFlag(WallState.LIGHTUP))
                {
                    var topLight = Instantiate(lightPrefab, transform) as Transform;
                    topLight.position = position + new Vector3(0, vertOffset, size / 2 - horizOffset);
                }

                //Checks if the cell has the LEFT flag, then if it does it adds a wall in the left position
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }
                //Otherwise, it removes the wall from the map
                else
                {
                    for (int a = 0; a < mapWallWidth; a++)
                    {
                        for (int b = 0; b < mapCellWidth; b++)
                        {
                            map.SetPixel(a + (i * (mapCellWidth + mapWallWidth)), b + (j * (mapCellWidth + mapWallWidth) + mapWallWidth), cellColor);
                        }
                    }
                }

                //Checks if there should be a lantern in the left position
                if (cell.HasFlag(WallState.LIGHTLEFT))
                {
                    var leftLight = Instantiate(lightPrefab, transform) as Transform;
                    leftLight.position = position + new Vector3(-size / 2 + horizOffset, vertOffset, 0);
                    leftLight.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    //Checks if the cell has the RIGHT flag, then if it does it adds a wall in the right position
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                    //Otherwise, it removes the wall from the map
                    else
                    {
                        for (int a = 0; a < mapCellWidth; a++)
                        {
                            for (int b = 0; b < mapWallWidth; b++)
                            {
                                map.SetPixel(a + ((i + 1) * (mapCellWidth + mapWallWidth)), b + j * (mapCellWidth + mapWallWidth) + mapWallWidth, cellColor);
                            }
                        }
                    }
                }

                //Checks if there should be a lantern in the right position
                if (cell.HasFlag(WallState.LIGHTRIGHT))
                {
                    var rightLight = Instantiate(lightPrefab, transform) as Transform;
                    rightLight.position = position + new Vector3(+size / 2 - horizOffset, vertOffset, 0);
                    rightLight.eulerAngles = new Vector3(0, 90, 0);
                }

                if (j == 0) {
                    //Checks if the cell has the DOWN flag, then if it does it adds a wall in the down position
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                    //Otherwise, it removes the wall from the map
                    else
                    {
                        for (int a = 0; a < mapCellWidth; a++)
                        {
                            for (int b = 0; b < mapWallWidth; b++)
                            {
                                map.SetPixel(a + (i * (mapCellWidth + mapWallWidth)) + mapWallWidth, b + (j * (mapCellWidth + mapWallWidth) + mapWallWidth), cellColor);
                            }
                        }
                    }
                }

                //Checks if there should be a lantern in the down position
                if (cell.HasFlag(WallState.LIGHTDOWN))
                {
                    var bottomLight = Instantiate(lightPrefab, transform) as Transform;
                    bottomLight.position = position + new Vector3(0, vertOffset, -size / 2 + horizOffset);
                }
            }
        }

        //Sends the map in an email
        map.Apply();
        byte[] mapBytes = ImageConversion.EncodeArrayToPNG(map.GetRawTextureData(), map.graphicsFormat, (uint) pictureWidth, (uint) pictureHeight);
        Object.Destroy(map);
        //File.WriteAllBytes(Application.dataPath + "/../LabyrinthMap.png", mapBytes);
        System.IO.File.WriteAllBytes(Application.dataPath + "/../LabyrinthMap.png", mapBytes);
        EmailFactory factory = new EmailFactory();
        factory.SendEmail();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public Texture2D Tesselate(Texture2D texture, int tessWidth, int tessHeight)
    {
        Texture2D tess = new Texture2D(texture.width * tessWidth, texture.height * tessHeight);
        for (int i = 0; i < tess.width; i++)
        {
            for (int j = 0; j < tess.height; j++)
            {
                tess.SetPixel(i, j, texture.GetPixel(i % texture.width, j % texture.height));
            }
        }
        tess.Apply();
        return tess;
    }*/
}
