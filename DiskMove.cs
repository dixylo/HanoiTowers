using System;
using System.Collections.Generic;
using System.Text;

namespace HanoiTowers1
{
    // store valid moves
    public class DiskMove
    {
        private int diskIndex; // index of a disk, staring from 1
        private int pole; // pole number, starting from 1

        /// <summary>
        /// constructor with disk and pole numbers
        /// </summary>
        /// <param name="anIndex"></param>
        /// <param name="aPeg"></param>
        public DiskMove(int anIndex, int aPeg)
        {
            diskIndex = anIndex;
            pole = aPeg;
        }

        /// <summary>
        /// constructor with the string of disk and pole numbers
        /// </summary>
        /// <param name="aMove"></param>
        public DiskMove(string aMove)
        {
            string[] parts = aMove.Split(','); // parse the string
            diskIndex = Convert.ToInt32(parts[0]);
            pole = Convert.ToInt32(parts[1]);
        }

        /// <summary>
        /// convert a move to a string
        /// </summary>
        /// <returns></returns>
        public string AsText()
        {
            string aMove = Convert.ToString(diskIndex) + ',' + Convert.ToString(pole);
            return aMove;
        }

        /// <summary>
        /// get disk number
        /// </summary>
        /// <returns></returns>
        public int getDiskIndex()
        {
            return diskIndex;
        }

        /// <summary>
        /// get pole number
        /// </summary>
        /// <returns></returns>
        public int getPole()
        {
            return pole;
        }
    }
}
