namespace NetMazeServer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMaze = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.StartServerbutton = new System.Windows.Forms.Button();
            this.listPlayers = new System.Windows.Forms.ListBox();
            this.repaintBtn = new System.Windows.Forms.Button();
            this.adresaServeru = new System.Windows.Forms.Label();
            this.destinationX = new System.Windows.Forms.NumericUpDown();
            this.destinationY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.setDestinationPointBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.destinationX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationY)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMaze
            // 
            this.panelMaze.BackColor = System.Drawing.Color.Transparent;
            this.panelMaze.Location = new System.Drawing.Point(56, 42);
            this.panelMaze.Name = "panelMaze";
            this.panelMaze.Size = new System.Drawing.Size(640, 702);
            this.panelMaze.TabIndex = 0;
            this.panelMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            this.panelMaze.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(59, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(155, 9);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // StartServerbutton
            // 
            this.StartServerbutton.Location = new System.Drawing.Point(608, 9);
            this.StartServerbutton.Name = "StartServerbutton";
            this.StartServerbutton.Size = new System.Drawing.Size(75, 23);
            this.StartServerbutton.TabIndex = 3;
            this.StartServerbutton.Text = "Start Server";
            this.StartServerbutton.UseVisualStyleBackColor = true;
            this.StartServerbutton.Click += new System.EventHandler(this.StartServerbutton_Click);
            // 
            // listPlayers
            // 
            this.listPlayers.FormattingEnabled = true;
            this.listPlayers.Location = new System.Drawing.Point(724, 45);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(141, 407);
            this.listPlayers.TabIndex = 4;
            // 
            // repaintBtn
            // 
            this.repaintBtn.Location = new System.Drawing.Point(409, 9);
            this.repaintBtn.Name = "repaintBtn";
            this.repaintBtn.Size = new System.Drawing.Size(75, 23);
            this.repaintBtn.TabIndex = 5;
            this.repaintBtn.Text = "Repaint";
            this.repaintBtn.UseVisualStyleBackColor = true;
            this.repaintBtn.Click += new System.EventHandler(this.repaintBtn_Click);
            // 
            // adresaServeru
            // 
            this.adresaServeru.AutoSize = true;
            this.adresaServeru.Location = new System.Drawing.Point(690, 13);
            this.adresaServeru.Name = "adresaServeru";
            this.adresaServeru.Size = new System.Drawing.Size(39, 13);
            this.adresaServeru.TabIndex = 6;
            this.adresaServeru.Text = "adresa";
            // 
            // destinationX
            // 
            this.destinationX.Location = new System.Drawing.Point(797, 487);
            this.destinationX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.destinationX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.destinationX.Name = "destinationX";
            this.destinationX.Size = new System.Drawing.Size(46, 20);
            this.destinationX.TabIndex = 7;
            this.destinationX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // destinationY
            // 
            this.destinationY.Location = new System.Drawing.Point(797, 513);
            this.destinationY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.destinationY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.destinationY.Name = "destinationY";
            this.destinationY.Size = new System.Drawing.Size(46, 20);
            this.destinationY.TabIndex = 7;
            this.destinationY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 504);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Destination point :";
            // 
            // setDestinationPointBtn
            // 
            this.setDestinationPointBtn.Location = new System.Drawing.Point(724, 549);
            this.setDestinationPointBtn.Name = "setDestinationPointBtn";
            this.setDestinationPointBtn.Size = new System.Drawing.Size(119, 23);
            this.setDestinationPointBtn.TabIndex = 9;
            this.setDestinationPointBtn.Text = "Set destination point";
            this.setDestinationPointBtn.UseVisualStyleBackColor = true;
            this.setDestinationPointBtn.Click += new System.EventHandler(this.setDestinationPointBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 756);
            this.Controls.Add(this.setDestinationPointBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destinationY);
            this.Controls.Add(this.destinationX);
            this.Controls.Add(this.adresaServeru);
            this.Controls.Add(this.repaintBtn);
            this.Controls.Add(this.listPlayers);
            this.Controls.Add(this.StartServerbutton);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelMaze);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.onClosed);
            ((System.ComponentModel.ISupportInitialize)(this.destinationX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMaze;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button StartServerbutton;
        private System.Windows.Forms.ListBox listPlayers;
        private System.Windows.Forms.Button repaintBtn;
        private System.Windows.Forms.Label adresaServeru;
        private System.Windows.Forms.NumericUpDown destinationX;
        private System.Windows.Forms.NumericUpDown destinationY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button setDestinationPointBtn;
    }
}

