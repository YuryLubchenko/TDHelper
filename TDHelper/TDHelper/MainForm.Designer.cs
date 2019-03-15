namespace TDHelper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.idleLabel = new System.Windows.Forms.Label();
            this.startedOnLabel = new System.Windows.Forms.Label();
            this.idleTextBox = new System.Windows.Forms.TextBox();
            this.startedOnTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // idleLabel
            // 
            this.idleLabel.AutoSize = true;
            this.idleLabel.Location = new System.Drawing.Point(12, 9);
            this.idleLabel.Name = "idleLabel";
            this.idleLabel.Size = new System.Drawing.Size(50, 13);
            this.idleLabel.TabIndex = 0;
            this.idleLabel.Text = "Idle Time";
            // 
            // startedOnLabel
            // 
            this.startedOnLabel.AutoSize = true;
            this.startedOnLabel.Location = new System.Drawing.Point(12, 35);
            this.startedOnLabel.Name = "startedOnLabel";
            this.startedOnLabel.Size = new System.Drawing.Size(58, 13);
            this.startedOnLabel.TabIndex = 1;
            this.startedOnLabel.Text = "Started On";
            // 
            // idleTextBox
            // 
            this.idleTextBox.Location = new System.Drawing.Point(111, 6);
            this.idleTextBox.Name = "idleTextBox";
            this.idleTextBox.ReadOnly = true;
            this.idleTextBox.Size = new System.Drawing.Size(119, 20);
            this.idleTextBox.TabIndex = 2;
            // 
            // startedOnTextBox
            // 
            this.startedOnTextBox.Location = new System.Drawing.Point(111, 32);
            this.startedOnTextBox.Name = "startedOnTextBox";
            this.startedOnTextBox.ReadOnly = true;
            this.startedOnTextBox.Size = new System.Drawing.Size(119, 20);
            this.startedOnTextBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 62);
            this.Controls.Add(this.startedOnTextBox);
            this.Controls.Add(this.idleTextBox);
            this.Controls.Add(this.startedOnLabel);
            this.Controls.Add(this.idleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "TD Helper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idleLabel;
        private System.Windows.Forms.Label startedOnLabel;
        private System.Windows.Forms.TextBox idleTextBox;
        private System.Windows.Forms.TextBox startedOnTextBox;
    }
}

