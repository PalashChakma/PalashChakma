using System;
using PalashChakma.Base;

namespace PalashChakma.Controller
{
    public class ToyRobotController
    {
        public ToyRobotController(ToyRobot robot)
        {
            this.Robot = robot;
        }

        public ToyRobot Robot { get; set; }

        public string ExecCommand(string command)
        {
            string message = "";
            ParameterCommands args = null;
            var commobj = GetCommand(command, ref args);

            switch (commobj)
            {
                case Command.Place:
                    var placeParams = (ExecParameterCommands)args;
                    if (Robot.Place(placeParams.X, placeParams.Y, placeParams.Direction))
                    {
                        message = "Placed.";
                    }
                    else
                    {
                        message = Robot.ErrorMsg;
                    }
                    break;
                case Command.Move:
                    if (Robot.Move())
                    {
                        message = "Moved.";
                    }
                    else
                    {
                        message = Robot.ErrorMsg;
                    }
                    break;
                case Command.Left:
                    if (Robot.Left())
                    {
                        message = "Turned Left.";
                    }
                    else
                    {
                        message = Robot.ErrorMsg;
                    }
                    break;
                case Command.Right:
                    if (Robot.Right())
                    {
                        message = "Turned Right.";
                    }
                    else
                    {
                        message = Robot.ErrorMsg;
                    }
                    break;
                case Command.Report:
                    message = Robot.Report();
                    break;
                default:
                    message = "Not a valid Command.";
                    break;
            }
            return message;
        }

        private Command GetCommand(string command, ref ParameterCommands args)
        {
            Command result;
            string argString = "";

            int spacePos = command.IndexOf(" ");
            if (spacePos > 0)
            {
                argString = command.Substring(spacePos + 1);
                command = command.Substring(0, spacePos);
            }
            command = command.ToUpper();

            if (Enum.TryParse<Command>(command, true, out result))
            {
                if (result == Command.Place)
                {
                    if (!ParseParameterCommands(argString, ref args))
                    {
                        result = Command.Invalid;
                    }
                }
            }
            else
            {
                result = Command.Invalid;
            }
            return result;
        }

        private bool ParseParameterCommands(string argString, ref ParameterCommands args)
        {
            var argParts = argString.Split(',');
            int x, y;
            Direction facing;

            if (argParts.Length == 3 &&
                TryGetCoordinate(argParts[0], out x) &&
                TryGetCoordinate(argParts[1], out y) &&
                TryGetFacingDirection(argParts[2], out facing))
            {
                args = new ExecParameterCommands
                {
                    X = x,
                    Y = y,
                    Direction = facing,
                };
                return true;
            }
            return false;
        }

        private bool TryGetCoordinate(string coordinate, out int coordinateValue)
        {
            return int.TryParse(coordinate, out coordinateValue);
        }

        private bool TryGetFacingDirection(string direction, out Direction facing)
        {
            return Enum.TryParse<Direction>(direction, true, out facing);
        }
    }
}
