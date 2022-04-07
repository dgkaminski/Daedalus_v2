using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum WallState
{
    // 0000 means no walls
    // 1111 means walls in all four directions

    EMPTY = 0, //        0000 0000 0000

    LEFT  = 1, //        0000 0000 0001
    RIGHT = 2, //        0000 0000 0010
    UP    = 4, //        0000 0000 0100
    DOWN  = 8, //        0000 0000 1000

    LIGHTLEFT = 16, //   0000 0001 0000
    LIGHTRIGHT = 32, //  0000 0010 0000
    LIGHTUP = 64, //     0000 0100 0000
    LIGHTDOWN = 128, //  0000 1000 0000

    VISITED = 256, //    0001 0000 0000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbor
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{

    private static WallState GetOppositeWall(WallState wall) {
        switch (wall) {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private static WallState GetOppositeWall2(WallState wall)
    {
        if (wall.HasFlag(WallState.RIGHT))
        {
            return WallState.LEFT | WallState.LIGHTLEFT;
        }
        else if (wall.HasFlag(WallState.LEFT))
        {
            return WallState.RIGHT | WallState.LIGHTRIGHT;
        }
        else if (wall.HasFlag(WallState.UP))
        {
            return WallState.DOWN | WallState.LIGHTDOWN;
        }
        else if (wall.HasFlag(WallState.DOWN))
        {
            return WallState.UP | WallState.LIGHTUP;
        } else
        {
            return WallState.EMPTY;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height) {
        var rng = new System.Random();
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height)};

        maze[position.X, position.Y] |= WallState.VISITED;
        positionStack.Push(position);

        while (positionStack.Count > 0) {
            var current = positionStack.Pop();
            var neighbors = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbors.Count > 0) {
                positionStack.Push(current);
                var randIndex = rng.Next(0, neighbors.Count);
                var randomNeighbor = neighbors[randIndex];

                var nPosition = randomNeighbor.Position;
                maze[current.X, current.Y] &= ~randomNeighbor.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall2(randomNeighbor.SharedWall);

                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private static List<Neighbor> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height) {
        var list = new List<Neighbor>();

        if (p.X > 0) { //left
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED)) {
                list.Add(new Neighbor
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT | WallState.LIGHTLEFT
                });
            }
        }

        if (p.Y > 0) { //bottom
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED)) {
                list.Add(new Neighbor {
                    Position = new Position {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN | WallState.LIGHTDOWN
                });
            }
        }

        if (p.Y < height - 1) { //up
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED)) {
                list.Add(new Neighbor {
                    Position = new Position {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP | WallState.LIGHTUP
                });
            }
        }

        if (p.X < width - 1) { //right
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED)) {
                list.Add(new Neighbor {
                    Position = new Position {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT | WallState.LIGHTRIGHT
                });
            }
        }

        return list;
    }

    public static WallState[,] Generate(int width, int height, float lightChance) {
        WallState[,] maze = new WallState[width, height];

        var rng = new System.Random();

        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j) {
                maze[i,j] = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;

                if (rng.NextDouble() < lightChance) {
                    int rand = rng.Next(0, 4);
                    if (rand == 0)
                    {
                        maze[i, j] |= WallState.LIGHTRIGHT;
                    }
                    else if (rand == 1)
                    {
                        maze[i, j] |= WallState.LIGHTLEFT;
                    }
                    else if (rand == 2) {
                        maze[i, j] |= WallState.LIGHTUP;
                    }
                    else if (rand == 3)
                    {
                        maze[i, j] |= WallState.LIGHTDOWN;
                    }
                }
            }
        }

        return ApplyRecursiveBacktracker(maze, width, height);
    }
}
