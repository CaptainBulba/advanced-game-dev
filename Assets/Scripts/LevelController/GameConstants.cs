
public class GameConstants
{
    // Basic actions

    public const string actionNextRoom = "next_room";

    // Puzzles 

    public const string puzzleRotation = "rotate_puzzle";
    public const string puzzleGuessNum = "number_puzzle";
    public const string puzzleLock = "lock_puzzle";
}

    public enum LevelOneActions
    {
        RotatePuzzle,
        LockPuzzle,
        GuessNumberPuzzle,
        NextRoom
    }