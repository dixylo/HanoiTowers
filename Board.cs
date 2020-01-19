using System;
using System.Windows.Forms;
using System.Collections;

namespace HanoiTowers1
{
    //The board class stores the constants needed to handle the display of disks
    //It has a two dimensional array of Disk references that represents the board
    //It has an ArrayList for storing the moves made
    //It has a one dimensional array of 4 Disk references for the Disk objects used in the game.

    public class Board
    {
        Disk[] disks;
        Disk[,] positions;
        int[,] savedPositions; // store the position of the disks of a saved game
        string savedMoves; // store the moves of a saved game

        public Label thisDisk;
        private ArrayList moves; // store valid moves

        /// <summary>
        /// constructor with four disk objects
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        public Board(Disk d1, Disk d2, Disk d3, Disk d4)
        {
            // assign values to disks
            disks = new Disk[4];
            disks[0] = d1;
            disks[1] = d2;
            disks[2] = d3;
            disks[3] = d4;
            // assign disks to positions
            positions = new Disk[3, 4];
            positions[0, 3] = d1;
            positions[0, 2] = d2;
            positions[0, 1] = d3;
            positions[0, 0] = d4;

            savedPositions = new int[4, 2]; // initialize the array for saving positions

            moves = new ArrayList(); // initialize the arraylist for storing valid moves
        }

        /// <summary>
        /// restart the game
        /// </summary>
        public void Reset()
        {
            // set the disks to the initial positions
            disks[0].setPosition(1, 4);
            disks[1].setPosition(1, 3);
            disks[2].setPosition(1, 2);
            disks[3].setPosition(1, 1);
            // set the positions to the initial values
            Array.Clear(positions, 0, positions.Length);
            for (int i = 0; i < 4; i++)
            {
                positions[0, i] = disks[3 - i]; // set disks to the initial positions
            }

            moves.Clear(); // clear all moves
        }

        /// <summary>
        /// check the disk is allowed to be moved
        /// </summary>
        /// <param name="aDisk"></param>
        /// <returns></returns>
        public bool CanStartMove(Disk aDisk)
        {
            int poleIndex = aDisk.getPole() - 1;
            int levelIndex = aDisk.getLevel() - 1;

            if (levelIndex < 3 && positions[poleIndex, levelIndex + 1] != null) // if a disk is not on the fourth level and has a disk above itself, then it's not allow to be moved
            {
                return false; // then it's invalid
            }
            return true;
        }

        /// <summary>
        /// check the drop is valid
        /// </summary>
        /// <param name="aDisk"></param>
        /// <param name="aPeg"></param>
        /// <returns></returns>
        public bool CanDrop(Disk aDisk, int aPeg)
        {
            int newLevel = 3;
            int newPole = aPeg - 1;
            for (int i = 0; i < 4; i++)
            {
                if (positions[newPole, i] == null) // find the highest vacant level
                {
                    newLevel = i;
                    break;
                }
            }

            if (newLevel > 0) // if the vacant level is not the base level
            {
                if (aDisk.thisDisk.Width > positions[newPole, newLevel - 1].thisDisk.Width) // and if the dragged disk is larger than the one to be dropped on
                {
                    return false; // then it's invalid
                }
            }
            return true;
        }

        /// <summary>
        /// move a disk to a new valid position
        /// </summary>
        /// <param name="aDisk"></param>
        /// <param name="aPeg"></param>
        public void Move(Disk aDisk, int aPeg)
        {
            int oldPole = aDisk.getPole() - 1;
            int oldLevel = aDisk.getLevel() - 1;
            int newPole = aPeg - 1;
            int newLevel = 3;
            for (int i = 0; i < 4; i++)
            {
                if (positions[newPole, i] == null) // find the highest vacant level
                {
                    newLevel = i;
                    break;
                }
            }

            positions[newPole, newLevel] = positions[oldPole, oldLevel]; // transfer the disk reference
            positions[oldPole, oldLevel] = null; // set the old position vacant
            positions[newPole, newLevel].Move(newPole + 1, newLevel + 1); // actually move the disk

            int diskIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if (disks[i] == aDisk)
                {
                    diskIndex = i + 1; // find the number of disk, instead of the object, as the constructor of the diskmove class requires two integers
                    break;
                }
            }

            moves.Add(new DiskMove(diskIndex, aPeg)); // store this valid move 
        }

        /// <summary>
        /// show the string that combines all moves
        /// </summary>
        /// <returns></returns>
        public string AllMovesAsString()
        {
            string allMoves = "";
            foreach (DiskMove dm in moves)
            {
                allMoves += dm.AsText() + "\r\n";
            }
            return allMoves;
        }

        /// <summary>
        /// save a board
        /// </summary>
        public void Save()
        {
            for (int i = 0; i < 4; i++)
            {
                savedPositions[i, 0] = disks[i].getPole(); // save the pole number of a disk
                savedPositions[i, 1] = disks[i].getLevel(); // save the level number of a disk
            }
            savedMoves = this.AllMovesAsString(); // save the string of all moves
        }

        /// <summary>
        /// display the board of a saved game
        /// </summary>
        public void Display()
        {
            Array.Clear(positions, 0, positions.Length); // clear positions
            for (int i = 0; i < 4; i++)
            {
                var pole = savedPositions[i, 0];
                var level = savedPositions[i, 1];
                disks[i].setPosition(pole, level); // set a particular disk to its saved position
                positions[pole -1, level - 1] = disks[i]; // make a disk occupy its position
            }

            moves.Clear(); // clear the moves array
            string[] sep = new string[] {"\r\n"}; // define the separator to split the string of all moves of a saved game
            string[] parts = savedMoves.Split(sep, StringSplitOptions.RemoveEmptyEntries); // parse the moves
            foreach (string part in parts)
            {
                moves.Add(new DiskMove(part)); // load saved moves
            }

        }

        /// <summary>
        /// find the disk object via a label
        /// </summary>
        /// <param name="aLabel"></param>
        /// <returns></returns>
        public Disk FindDisk(Label aLabel)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                if (disks[i].thisDisk == aLabel)
                {
                    index = i; // find the index of the disk
                    break;
                }
            }
            return disks[index];
        }

        /// <summary>
        /// check the game is completed
        /// </summary>
        /// <returns></returns>
        public bool checkGoal()
        {
            if (positions[2, 3] != null) // if the fourth level of the third pole is occupied, the game is complete
            {
                return true;
            }
            return false;
        }
    }
}
