using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HanoiTowers1
{
	/// <summary>
	/// This form lets the user play a game of HanoiTowers.
	/// 4 labels representing disks are shown on the first of three poles. It is possible 
	/// to drag a disk from one pole to another. The rules for a valid move are that
	/// a bigger disk cannot be dropped on top of a smaller one. The aim of the game
	/// is to move the stack of disks to another pole one disk at a time.
	/// Moves made by Dragging are recorder as lines of text in a textBox
	/// It is possible to reset the disks to their original position
	/// It is also possible to replay the moves stored in the textbox
	/// either by stepping through them - the [Step] button
	/// or from a timer - started by the [Animate] button
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private Disk[,] disks = new Disk[3,4];
		//array of possible positions of disks over the 3 poles and four levels
		//the array keeps track of where the labels representing the disks are
		//the Disk class stores the pole and level of a label representing the disk
		//as well as an object refrence to the label

		private int targetPole = 0;
		//used to communicate between DragDrop which identifies the pole being dropped on
		//and the MouseDown method for the "disks" which will move a "disk" to a new
		//pole after DragDrop is completed

		private int animateLine = 0;
        //used to say which line in a list of moves is the current move

        private int MoveCount = 0; //count of moves made in a game

        private string tempMoves = null; // store the saved moves of an incompleted game for loading later

        private Board board;

        /// <summary>
        /// Components of the MainForm
        /// </summary>
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label Disk4;
		private System.Windows.Forms.Label Disk3;
		private System.Windows.Forms.Label Disk2;
		private System.Windows.Forms.Label Pole2;
		private System.Windows.Forms.Label Pole1;
		private System.Windows.Forms.Label Pole3;
		private System.Windows.Forms.Label Disk1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtMoves;
		private System.Timers.Timer timer1;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnAnimate;
		private System.Windows.Forms.Button btnStep;
		private System.Windows.Forms.Label lblMoves;
        private Label label2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button btnSave;
        private Button btnLoad;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.Disk4 = new System.Windows.Forms.Label();
            this.Disk3 = new System.Windows.Forms.Label();
            this.Disk2 = new System.Windows.Forms.Label();
            this.Disk1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.Pole2 = new System.Windows.Forms.Label();
            this.Pole1 = new System.Windows.Forms.Label();
            this.Pole3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMoves = new System.Windows.Forms.TextBox();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.timer1 = new System.Timers.Timer();
            this.btnStep = new System.Windows.Forms.Button();
            this.lblMoves = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Location = new System.Drawing.Point(120, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 48);
            this.panel1.TabIndex = 0;
            // 
            // Disk4
            // 
            this.Disk4.BackColor = System.Drawing.Color.Lime;
            this.Disk4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk4.Location = new System.Drawing.Point(157, 216);
            this.Disk4.Name = "Disk4";
            this.Disk4.Size = new System.Drawing.Size(144, 24);
            this.Disk4.TabIndex = 5;
            this.Disk4.Text = "4";
            this.Disk4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Disk4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk4_MouseDown);
            // 
            // Disk3
            // 
            this.Disk3.BackColor = System.Drawing.Color.Lime;
            this.Disk3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk3.Location = new System.Drawing.Point(173, 192);
            this.Disk3.Name = "Disk3";
            this.Disk3.Size = new System.Drawing.Size(112, 24);
            this.Disk3.TabIndex = 6;
            this.Disk3.Text = "3";
            this.Disk3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Disk3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk3_MouseDown);
            // 
            // Disk2
            // 
            this.Disk2.BackColor = System.Drawing.Color.Lime;
            this.Disk2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk2.Location = new System.Drawing.Point(189, 168);
            this.Disk2.Name = "Disk2";
            this.Disk2.Size = new System.Drawing.Size(80, 24);
            this.Disk2.TabIndex = 7;
            this.Disk2.Text = "2";
            this.Disk2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Disk2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk2_MouseDown);
            // 
            // Disk1
            // 
            this.Disk1.BackColor = System.Drawing.Color.Lime;
            this.Disk1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Disk1.Location = new System.Drawing.Point(205, 144);
            this.Disk1.Name = "Disk1";
            this.Disk1.Size = new System.Drawing.Size(48, 24);
            this.Disk1.TabIndex = 8;
            this.Disk1.Text = "1";
            this.Disk1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Disk1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Disk1_MouseDown);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(16, 16);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(104, 32);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Restart";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Pole2
            // 
            this.Pole2.AllowDrop = true;
            this.Pole2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole2.Location = new System.Drawing.Point(396, 112);
            this.Pole2.Name = "Pole2";
            this.Pole2.Size = new System.Drawing.Size(24, 144);
            this.Pole2.TabIndex = 10;
            this.Pole2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole2_DragDrop);
            this.Pole2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole2_DragEnter);
            // 
            // Pole1
            // 
            this.Pole1.AllowDrop = true;
            this.Pole1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole1.Location = new System.Drawing.Point(216, 112);
            this.Pole1.Name = "Pole1";
            this.Pole1.Size = new System.Drawing.Size(24, 144);
            this.Pole1.TabIndex = 11;
            this.Pole1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole1_DragDrop);
            this.Pole1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole1_DragEnter);
            // 
            // Pole3
            // 
            this.Pole3.AllowDrop = true;
            this.Pole3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Pole3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pole3.Location = new System.Drawing.Point(576, 112);
            this.Pole3.Name = "Pole3";
            this.Pole3.Size = new System.Drawing.Size(24, 144);
            this.Pole3.TabIndex = 13;
            this.Pole3.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pole3_DragDrop);
            this.Pole3.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pole3_DragEnter);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(350, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Moves:";
            // 
            // txtMoves
            // 
            this.txtMoves.Location = new System.Drawing.Point(733, 64);
            this.txtMoves.Multiline = true;
            this.txtMoves.Name = "txtMoves";
            this.txtMoves.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoves.Size = new System.Drawing.Size(100, 280);
            this.txtMoves.TabIndex = 15;
            // 
            // btnAnimate
            // 
            this.btnAnimate.Enabled = false;
            this.btnAnimate.Location = new System.Drawing.Point(16, 112);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(104, 32);
            this.btnAnimate.TabIndex = 16;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // btnStep
            // 
            this.btnStep.Enabled = false;
            this.btnStep.Location = new System.Drawing.Point(16, 64);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(104, 32);
            this.btnStep.TabIndex = 17;
            this.btnStep.Text = "Step";
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // lblMoves
            // 
            this.lblMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblMoves.Location = new System.Drawing.Point(418, 23);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(48, 24);
            this.lblMoves.TabIndex = 18;
            this.lblMoves.Text = "0";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(729, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 45);
            this.label2.TabIndex = 19;
            this.label2.Text = "Your Moves (Disk, Pole)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(218, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "1";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(398, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "2";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(578, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "3";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(16, 161);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 32);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 32);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(888, 365);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnAnimate);
            this.Controls.Add(this.txtMoves);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.Disk1);
            this.Controls.Add(this.Disk2);
            this.Controls.Add(this.Disk3);
            this.Controls.Add(this.Disk4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Pole1);
            this.Controls.Add(this.Pole2);
            this.Controls.Add(this.Pole3);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Hanoi Tower";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        /// <summary>
        /// put all four disks back in order on first pole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnReset_Click(object sender, System.EventArgs e)
		{
            board.Reset(); // board returns to the initial state
            MoveCount = 0; // zero the count of moves
            lblMoves.Text = "0"; // clear move count shown
            txtMoves.Text = ""; // clear all moves shown
            animateLine = 0; // make sure stepping and animation will start from the first line when game is over
            Pole1.AllowDrop = true; // make sure game can run normally
            Pole2.AllowDrop = true; // make sure game can run normally
            Pole3.AllowDrop = true; // make sure game can run normally
            btnStep.Enabled = false; // stepping and animation are only allowed when a game is completed
            btnAnimate.Enabled = false; // stepping and animation are only allowed when a game is completed
            btnSave.Enabled = true; // make sure game is savable
            timer1.Enabled = false; // make sure the timer will stop when restart occurs during animation
        }

        /// <summary>
        /// create Disk objects matching the "Disk" labels on the first pole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MainForm_Load(object sender, System.EventArgs e)
		{
            board = new Board(new Disk(Disk1, 1, 4), new Disk(Disk2, 1, 3), new Disk(Disk3, 1, 2), new Disk(Disk4, 1, 1));
		}

        /// <summary>
        /// MouseDown Event on Disk1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Disk1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Label alabel = (sender as Label);
            dragEvent(alabel);

            if (board.checkGoal()) // check game is completed
            {
                if (MoveCount == 15) // check game is completed within the least-possible moves
                {
                    // inform the user she's made it within the least-possible moves
                    MessageBox.Show("You have successfully completed the game with the minimum number of moves!\r\nClick the 'Step' button to review your moves stepwards, or the 'Animate' button to animate moves automatically, or the 'Reset' button to start a new game.", "Congratulations!");
                }
                else
                {
                    // inform the user she's not made it within the least-possible moves
                    MessageBox.Show("You have successfully completed the game but not with the minimum number of moves!\r\nClick the 'Step' button to review your moves stepwards, or the 'Animate' button to animate moves automatically, or the 'Reset' button to start a new game", "Congratulations!");
                }
                Pole1.AllowDrop = false; // no more moves can be made once the game is completed unless a new game is started or an incompleted game is loaded
                Pole2.AllowDrop = false; // no more moves can be made once the game is completed unless a new game is started or an incompleted game is loaded
                Pole3.AllowDrop = false; // no more moves can be made once the game is completed unless a new game is started or an incompleted game is loaded
                btnStep.Enabled = true; // stepping and animation are only allowed when a game is completed
                btnAnimate.Enabled = true; // stepping and animation are only allowed when a game is completed
                btnSave.Enabled = false; // only an incompleted game is allowed to be saved
            }
        }

        /// <summary>
        /// MouseDown Event on Disk2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disk2_MouseDown(object sender, MouseEventArgs e)
        {
            Label alabel = (sender as Label);
            dragEvent(alabel);
        }

        /// <summary>
        /// MouseDown Event on Disk3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disk3_MouseDown(object sender, MouseEventArgs e)
        {
            Label alabel = (sender as Label);
            dragEvent(alabel);
        }

        /// <summary>
        /// MouseDown Event on Disk4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disk4_MouseDown(object sender, MouseEventArgs e)
        {
            Label alabel = (sender as Label);
            dragEvent(alabel);
        }

        /// <summary>
        /// in order to save some code
        /// </summary>
        /// <param name="alabel"></param>
        private void dragEvent(Label alabel)
        {
            Disk aDisk = board.FindDisk(alabel);

            if (board.CanStartMove(aDisk)) // check the disk is allowed to be moved
            {
                DragDropEffects result = alabel.DoDragDrop(alabel, DragDropEffects.All);
                if (result != DragDropEffects.None)
                {
                    if (board.CanDrop(aDisk, targetPole)) // check the drop is allowed
                    {
                        board.Move(aDisk, targetPole); // move a disk
                        MoveCount++;
                        lblMoves.Text = Convert.ToString(MoveCount);
                        txtMoves.Text = board.AllMovesAsString(); // show moves
                    }
                    else
                    {
                        MessageBox.Show("Invalid Move - cannot drop bigger disk on smaller", "Warning!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid Move - can only move top disk", "Warning!");
            }
        }

        /// <summary>
        /// change the cursor to show dropping is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// change the cursor to show dropping is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole2_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		//
		{
			e.Effect = DragDropEffects.All;
		}

        /// <summary>
        /// change the cursor to show dropping is allowed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /// <summary>
        /// when a drop happens store the information about which pole was dropped on in the global variable targetPole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)		
		{
			Label alabel = (sender as Label);
            dropEvent(alabel);
        }

        /// <summary>
        /// when a drop happens store the information about which pole was dropped on in the global variable targetPole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole2_DragDrop(object sender, DragEventArgs e)
        {
            Label alabel = (sender as Label);
            dropEvent(alabel);
        }

        /// <summary>
        /// when a drop happens store the information about which pole was dropped on in the global variable targetPole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pole3_DragDrop(object sender, DragEventArgs e)
        {
            Label alabel = (sender as Label);
            dropEvent(alabel);
        }

        /// <summary>
        /// in order to save some code
        /// </summary>
        /// <param name="alabel"></param>
        private void dropEvent(Label alabel)
        // in order to save some code
        {
            if (alabel == Pole1) targetPole = 1;
            else if (alabel == Pole2) targetPole = 2;
            else if (alabel == Pole3) targetPole = 3;
        }

        /// <summary>
        /// given a string with the .Name property of a disk return a reference to that disk assuming that only valid names are passed
        /// </summary>
        /// <param name="DiskName"></param>
        /// <returns></returns>
        private Label getDisk(string DiskName)
        {
            if (DiskName == "Disk1")
            {
                return Disk1;
            }
            else if (DiskName == "Disk2")
            {
                return Disk2;
            }
            else if (DiskName == "Disk3")
            {
                return Disk3;
            }
            else if (DiskName == "Disk4")
            {
                return Disk4;
            }
            else return Disk4;
        }

        /// <summary>
        /// turn the timer on to begin animation of the moves stored in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnimate_Click(object sender, System.EventArgs e)
		{
            timer1.Enabled = true;
            btnLoad.Enabled = false; // loading is not allowed when animation is in progress
        }

        /// <summary>
        /// repeat of the moves stored in the textbox one move at a time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnStep_Click(object sender, System.EventArgs e)
		{ 
            MakeNextMove();
        }

        /// <summary>
        /// repeat one of the moves stored in the textbox each time the timer fires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
            MakeNextMove();
        }

        /// <summary>
        /// repeat one of the moves stored in the textbox
        /// </summary>
		private void MakeNextMove()
		{
            if (animateLine >= txtMoves.Lines.Length - 1)
            {
                timer1.Enabled = false; // make sure timer is stoped when animation is completed
                animateLine = 0; // make sure step and animate can start from the first move again, if the user wants to do step and animate again
                btnLoad.Enabled = true;
                MessageBox.Show("Last available move has been completed");
            }
            else
            {
                if (animateLine == 0)
                {
                    board.Reset();
                    MoveCount = 0;
                }

                string aMove = txtMoves.Lines[animateLine];
                string[] parts = aMove.Split(',');
                Label diskLabel = getDisk("Disk" + parts[0]);
                int newPole = Convert.ToInt32(parts[1]);
                Disk aDisk = board.FindDisk(diskLabel);
                board.Move(aDisk, newPole);
                animateLine++;
                MoveCount++;
                lblMoves.Text = Convert.ToString(MoveCount);
            }
        }

        /// <summary>
        /// load a saved game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (tempMoves == null) // if no game has been saved
            {
                MessageBox.Show("No Uncompleted Game has been Saved!", "Error!");
            }
            else
            {
                txtMoves.Text = tempMoves;
                MoveCount = txtMoves.Lines.Length -1;
                lblMoves.Text = Convert.ToString(MoveCount);
                Pole1.AllowDrop = true;
                Pole2.AllowDrop = true;
                Pole3.AllowDrop = true;
                btnStep.Enabled = false; // step and animate not allowed if game is incomplete
                btnAnimate.Enabled = false; // step and animate not allowed if game is incomplete
                animateLine = 0; // make step and animate start from the first move, when the incompleted game is completed
                board.Display(); // display saved board
                MessageBox.Show("Game Loaded!");
            }
        }

        /// <summary>
        /// save an uncompleted game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            board.Save(); // save current board
            tempMoves = txtMoves.Text; // save current moves and act like an indicator that an incompleted game has been saved when loading game
            MessageBox.Show("Game Saved!");
        }
    }
}
