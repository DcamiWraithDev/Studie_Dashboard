namespace Studie_Dasboard
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
            this.cBox = new System.Windows.Forms.ComboBox();
            this.dGrid = new System.Windows.Forms.DataGridView();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.blokBox = new System.Windows.Forms.ComboBox();
            this.pBarFormatief = new System.Windows.Forms.ProgressBar();
            this.pBarSummatief = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pLabelobjectief = new System.Windows.Forms.Label();
            this.plabelSubjectief = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // cBox
            // 
            this.cBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox.FormattingEnabled = true;
            this.cBox.Location = new System.Drawing.Point(128, 9);
            this.cBox.Name = "cBox";
            this.cBox.Size = new System.Drawing.Size(121, 24);
            this.cBox.TabIndex = 0;
            // 
            // dGrid
            // 
            this.dGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid.Location = new System.Drawing.Point(12, 53);
            this.dGrid.Name = "dGrid";
            this.dGrid.ReadOnly = true;
            this.dGrid.RowHeadersWidth = 51;
            this.dGrid.RowTemplate.Height = 24;
            this.dGrid.Size = new System.Drawing.Size(1878, 850);
            this.dGrid.TabIndex = 1;
            // 
            // typeBox
            // 
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(354, 9);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(121, 24);
            this.typeBox.TabIndex = 2;
            // 
            // blokBox
            // 
            this.blokBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blokBox.FormattingEnabled = true;
            this.blokBox.Location = new System.Drawing.Point(584, 8);
            this.blokBox.Name = "blokBox";
            this.blokBox.Size = new System.Drawing.Size(121, 24);
            this.blokBox.TabIndex = 3;
            // 
            // pBarFormatief
            // 
            this.pBarFormatief.Location = new System.Drawing.Point(901, 8);
            this.pBarFormatief.Name = "pBarFormatief";
            this.pBarFormatief.Size = new System.Drawing.Size(100, 23);
            this.pBarFormatief.TabIndex = 4;
            this.pBarFormatief.Value = 10;
            // 
            // pBarSummatief
            // 
            this.pBarSummatief.Location = new System.Drawing.Point(1194, 8);
            this.pBarSummatief.Name = "pBarSummatief";
            this.pBarSummatief.Size = new System.Drawing.Size(100, 23);
            this.pBarSummatief.TabIndex = 5;
            this.pBarSummatief.Value = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Filter op Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Filter op Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Filter op Blok:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(720, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Studie Punten (Objectief):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1013, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Studie Punten (Subjectief):";
            // 
            // pLabelobjectief
            // 
            this.pLabelobjectief.AutoSize = true;
            this.pLabelobjectief.BackColor = System.Drawing.Color.Transparent;
            this.pLabelobjectief.Location = new System.Drawing.Point(938, 34);
            this.pLabelobjectief.Name = "pLabelobjectief";
            this.pLabelobjectief.Size = new System.Drawing.Size(30, 16);
            this.pLabelobjectief.TabIndex = 11;
            this.pLabelobjectief.Text = "N/A";
            // 
            // plabelSubjectief
            // 
            this.plabelSubjectief.AutoSize = true;
            this.plabelSubjectief.BackColor = System.Drawing.Color.Transparent;
            this.plabelSubjectief.Location = new System.Drawing.Point(1231, 34);
            this.plabelSubjectief.Name = "plabelSubjectief";
            this.plabelSubjectief.Size = new System.Drawing.Size(30, 16);
            this.plabelSubjectief.TabIndex = 12;
            this.plabelSubjectief.Text = "N/A";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1153);
            this.Controls.Add(this.plabelSubjectief);
            this.Controls.Add(this.pLabelobjectief);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pBarSummatief);
            this.Controls.Add(this.pBarFormatief);
            this.Controls.Add(this.blokBox);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.dGrid);
            this.Controls.Add(this.cBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBox;
        private System.Windows.Forms.DataGridView dGrid;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.ComboBox blokBox;
        private System.Windows.Forms.ProgressBar pBarFormatief;
        private System.Windows.Forms.ProgressBar pBarSummatief;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label pLabelobjectief;
        private System.Windows.Forms.Label plabelSubjectief;
    }
}

