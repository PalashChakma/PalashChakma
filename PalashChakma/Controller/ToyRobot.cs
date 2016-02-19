using System;

namespace PalashChakma.Controller
{
    public class ToyRobot
    {
        private int? _coordx;
        private int? _coordy;
        private const int MAX_COORD = 5;
        public string ErrorMsg { get; set; }

        public ToyRobot()
        {
            ErrorMsg = "";
        }        
        private Direction _dir;

        public bool Place(int x, int y, Direction facing)
        {
            if (IsRobotOnTable(x, y, "placed"))
            {
                _coordx = x;
                _coordy = y;
                _dir = facing;
                return true;
            }
            return false;
        }

        public bool Move()
        {
            if (IsRobotPlaced("move"))
            {
                int newx = GetXPosCurrent();
                int newy = GetYPosCurrent();
                if (IsRobotOnTable(newx, newy, "moved"))
                {
                    _coordx = newx;
                    _coordy = newy;
                    return true;
                }
            }
            return false;
        }

        private int GetXPosCurrent()
        {
            if (_dir == Direction.East)
            {
                return _coordx.Value + 1;
            }
            else
            {
                if (_dir == Direction.West)
                {
                    return _coordx.Value - 1;
                }
            }
            return _coordx.Value;
        }

        private int GetYPosCurrent()
        {
            if (_dir == Direction.North)
            {
                return _coordy.Value + 1;
            }
            else
            {
                if (_dir == Direction.South)
                {
                    return _coordy.Value - 1;
                }
            }
            return _coordy.Value;
        }

        public bool Left()
        {
            return Turn(TurnDirection.Left);
        }

        public bool Right()
        {
            return Turn(TurnDirection.Right);
        }

        private bool Turn(TurnDirection direction)
        {
            if (IsRobotPlaced("turn"))
            {
                var directionValue = (int)_dir;
                if (direction == TurnDirection.Right)
                {
                    directionValue = directionValue + 1;
                }
                else
                {
                    directionValue = directionValue - 1;
                }                
                if (directionValue == 5) directionValue = 1;
                if (directionValue == 0) directionValue = 4;
                _dir = (Direction)directionValue;
                return true;
            }
            return false;
        }

        public string Report()
        {
            if (IsRobotPlaced("print current position"))
            {
                return String.Format("{0},{1},{2}", _coordx.Value, _coordy.Value, _dir.ToString().ToUpper());
            }
            return "";
        }

        private bool IsRobotPlaced(string action)
        {
            if (!_coordx.HasValue || !_coordy.HasValue)
            {
                ErrorMsg = String.Format("ToyRobot cannot {0}. Please placed on the table first.", action);
                return false;
            }
            return true;
        }

        private bool IsRobotOnTable(int x, int y, string action)
        {
            if (x < 0 || y < 0 || x > MAX_COORD || y > MAX_COORD)
            {
                ErrorMsg = String.Format("ToyRobot cannot be {0} there.", action);
                return false;
            }
            return true;
        }
    }

}
