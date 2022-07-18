using System.Diagnostics;

namespace MazePathfinder.MazeSolver
{
    /// <summary>
    /// 迷路探索アルゴリズム（深さ優先探索を使用）
    /// </summary>
    public class MazeSolverForDfs
    {
        /// <summary>迷路配列</summary>
        private readonly int[,] maze;

        /// <summary>迷路の横幅</summary>
        private readonly int mazeWidth;

        /// <summary>迷路の高さ</summary>
        private readonly int mazeHeight;

        /// <summary>探索済配列</summary>
        private int[] visitedMazeArray;

        /// <summary>スタート地点</summary>
        private Point startPoint;

        /// <summary>ゴール地点</summary>
        private Point goalPoint;

        /// <summary>探索の処理時間</summary>
        private readonly Stopwatch stopwatch;

        /// <summary>探索処理のタイムアウト時間（秒）</summary>
        const int MAZE_TIMEOUT = 15;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mazeArray">迷路配列</param>
        /// <param name="startX">スタート地点の位置X</param>
        /// <param name="startY">スタート地点の位置Y</param>
        /// <param name="gorlX">ゴール地点の位置X</param>
        /// <param name="gorlY">ゴール地点の位置Y</param>
        public MazeSolverForDfs(int[,] mazeArray, int startX, int startY, int gorlX, int gorlY)
        {
            maze = mazeArray;
            mazeWidth = (maze.GetLength(0) - 1);
            mazeHeight = (maze.GetLength(1) - 1);
            visitedMazeArray = new int[mazeWidth * mazeHeight];
            stopwatch = new Stopwatch();
            startPoint = new Point(startY, startX);
            goalPoint = new Point(gorlY, gorlX);
        }

        /// <summary>
        /// 探索処理（深さ優先探索を使用）
        /// https://algoful.com/Archive/Algorithm/DFS
        /// </summary>
        public void Search()
        {
            stopwatch.Start();

            MainForm.isGoaled = false;
            Stack<Point> queue = new Stack<Point>();
            queue.Push(startPoint);

            // 訪問済配列を -1 で初期化
            visitedMazeArray = Enumerable.Repeat(-1, visitedMazeArray.Length).ToArray();
            visitedMazeArray[ToIndex(startPoint)] = ToIndex(startPoint);

            while (queue.Count > 0 && MainForm.isGoaled == false)
            {
                var target = queue.Pop();

                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    Point nextPoint = new Point(target.X, target.Y);
                    switch (dir)
                    {
                        case Direction.Up:
                            nextPoint.Y -= 1;
                            break;
                        case Direction.Right:
                            nextPoint.X += 1;
                            break;
                        case Direction.Down:
                            nextPoint.Y += 1;
                            break;
                        case Direction.Left:
                            nextPoint.X -= 1;
                            break;
                        case Direction.UpRight:
                            nextPoint.Y -= 1;
                            nextPoint.X += 1;
                            break;
                        case Direction.UpLeft:
                            nextPoint.Y -= 1;
                            nextPoint.X -= 1;
                            break;
                        case Direction.DownRight:
                            nextPoint.Y += 1;
                            nextPoint.X += 1;
                            break;
                        case Direction.DownLeft:
                            nextPoint.Y += 1;
                            nextPoint.X -= 1;
                            break;
                    }
                    if (nextPoint.X >= 0 && nextPoint.Y >= 0 && nextPoint.X < mazeWidth && nextPoint.Y < mazeHeight)
                    {
                        if (visitedMazeArray[ToIndex(nextPoint)] < 0 && maze[nextPoint.X, nextPoint.Y] == 0)
                        {
                            SetVisited(target, nextPoint);
                            if (nextPoint.X == goalPoint.X && nextPoint.Y == goalPoint.Y)
                            {
                                queue.Clear();
                                queue.Push(nextPoint);
                                MainForm.isGoaled = true;
                                break;
                            }
                            else
                            {
                                queue.Push(nextPoint);
                            }
                        }
                    }
                }

                // 中止した場合、探索を中止する
                if (MainForm.isCalculationCancel == true)
                {
                    return;
                }

                // 探索処理のタイムアウト
                if (stopwatch.ElapsedMilliseconds > (MAZE_TIMEOUT * 1000))
                {
                    return;
                }
            }
            if (MainForm.isGoaled == true)
            {
                SetRoute();
            }
            return;
        }

        /// <summary>
        /// ゴールへのルートを二次元配列に設定する
        /// </summary>
        private void SetRoute()
        {
            // 探索済の配列からゴールまでのルートを設定する
            int startIndex = ToIndex(startPoint);
            int goalIndex = ToIndex(goalPoint);
            int beforeIndex = visitedMazeArray[goalIndex];
            List<int> route = new List<int>();

            // ゴールからスタートへのルートをたどる
            while (beforeIndex >= 0 && beforeIndex != startIndex)
            {
                route.Add(beforeIndex);
                beforeIndex = visitedMazeArray[beforeIndex];
            }

            // ゴールへのルートを設定
            foreach (var index in route)
            {
                Point point = new Point(index % mazeWidth, index / mazeWidth);
                maze[point.X, point.Y] = 2;
            }
            return;
        }

        /// <summary>
        /// 探索済データの設定を行う
        /// </summary>
        /// <param name="fromPoint">始点</param>
        /// <param name="toPoint">終点</param>
        private void SetVisited(Point fromPoint, Point toPoint)
        {
            int fromIndex = ToIndex(fromPoint);
            int toIndex = ToIndex(toPoint);
            visitedMazeArray[toIndex] = fromIndex;
            return;
        }

        /// <summary>
        /// Pointを一次元配列のインデックスに変換する
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>一次元配列のインデックス</returns>
        private int ToIndex(Point point)
        {
            return point.X + mazeWidth * point.Y;
        }

        // セル情報
        private struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        // 進行方向（上下左右斜め4つ）
        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
            UpRight = 4,
            UpLeft = 5,
            DownRight = 6,
            DownLeft = 7
        }
    }
}
