using System;
using System.Windows.Forms;

namespace HanoiTowers1
{
    //the Disk class stores the pole and level of a label representing the disk
    //as well as an object refrence to the label
    //It also holds the constants needed to show a "disk" label correctly
    //in position on a given level and pole and uses these constants
    //in its Move(int newPole, int newLevel) method.
    public class Disk
    {
        const int maxPoles = 3; // number of poles
        const int poleStart = 228; // the left coordinate of the leftest pole
        const int poleGap = 180; // distance between the left lines of neighoring poles
        const int deckHeight = 240; // the top coordinate of the base
        const int diskHeight = 24; // the thickness of a disk

        private int pole; // the pole number, starting from 1
        private int level; // the level number, starting from 1
        private int width; // the width of a disk
        public Label thisDisk; // a public variable referring to a disk

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="aLabel"></param>
        /// <param name="aPole"></param>
        /// <param name="aLevel"></param>
        public Disk(Label aLabel, int aPole, int aLevel)
        {
            width = aLabel.Width;
            pole = aPole;
            level = aLevel;
            thisDisk = aLabel;
        }

        /// <summary>
        /// direct setting of the position of a disk
        /// </summary>
        /// <param name="aPole"></param>
        /// <param name="aLevel"></param>
        public void setPosition(int aPole, int aLevel)
        {
            pole = aPole;
            level = aLevel;
            thisDisk.Hide(); // hide the disk when resetting its position
            thisDisk.Left = poleStart + ((pole - 1) * poleGap) - (width / 2); // reset the position
            thisDisk.Top = deckHeight - (level * diskHeight); // reset the position
            thisDisk.Show(); // show the disk when resetting is complete
        }

        /// <summary>
        /// get the pole of a disk
        /// </summary>
        /// <returns></returns>
        public int getPole()
        {
            return pole;
        }

        /// <summary>
        /// get the level of a disk
        /// </summary>
        /// <returns></returns>
        public int getLevel()
        {
            return level;
        }

        /// <summary>
        /// move a disk to a valid position
        /// </summary>
        /// <param name="newPole"></param>
        /// <param name="newLevel"></param>
        public void Move(int newPole, int newLevel)
        {
            pole = newPole;
            level = newLevel;
            thisDisk.Hide(); // hide the disk when resetting its position
            thisDisk.Left = poleStart + ((pole - 1) * poleGap) - (width / 2); // reset the position
            thisDisk.Top = deckHeight - (level * diskHeight); // reset the position
            thisDisk.Show(); // show the disk when resetting is complete
        }

    }
}
