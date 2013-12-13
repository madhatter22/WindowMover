namespace WindowMover.Views
{
    partial class ConfigView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigView));
            this.lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstAvailableKeyModifiers = new System.Windows.Forms.ListBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstCurrentKeyModifiers = new System.Windows.Forms.ListBox();
            this.lblIsSaving = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(306, 23);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(25, 13);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available Key Modifiers";
            // 
            // lstAvailableKeyModifiers
            // 
            this.lstAvailableKeyModifiers.FormattingEnabled = true;
            this.lstAvailableKeyModifiers.Location = new System.Drawing.Point(15, 39);
            this.lstAvailableKeyModifiers.Name = "lstAvailableKeyModifiers";
            this.lstAvailableKeyModifiers.Size = new System.Drawing.Size(120, 82);
            this.lstAvailableKeyModifiers.TabIndex = 2;
            this.lstAvailableKeyModifiers.DoubleClick += new System.EventHandler(this.LstAvailableKeyModifiersOnDoubleClick);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(309, 39);
            this.txtKey.MaxLength = 1;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(100, 20);
            this.txtKey.TabIndex = 4;
            this.txtKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtKeyOnKeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Current Key Modifiers";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(265, 162);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveOnClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(358, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Done";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelOnClick);
            // 
            // lstCurrentKeyModifiers
            // 
            this.lstCurrentKeyModifiers.FormattingEnabled = true;
            this.lstCurrentKeyModifiers.Location = new System.Drawing.Point(162, 39);
            this.lstCurrentKeyModifiers.Name = "lstCurrentKeyModifiers";
            this.lstCurrentKeyModifiers.Size = new System.Drawing.Size(120, 82);
            this.lstCurrentKeyModifiers.TabIndex = 8;
            this.lstCurrentKeyModifiers.DoubleClick += new System.EventHandler(this.LstCurrentKeyModifiersOnDoubleClick);
            // 
            // lblIsSaving
            // 
            this.lblIsSaving.AutoSize = true;
            this.lblIsSaving.Location = new System.Drawing.Point(123, 167);
            this.lblIsSaving.Name = "lblIsSaving";
            this.lblIsSaving.Size = new System.Drawing.Size(108, 13);
            this.lblIsSaving.TabIndex = 9;
            this.lblIsSaving.Text = "Saving, please wait...";
            this.lblIsSaving.Visible = false;
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 200);
            this.Controls.Add(this.lblIsSaving);
            this.Controls.Add(this.lstCurrentKeyModifiers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lstAvailableKeyModifiers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Window Mover Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstAvailableKeyModifiers;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstCurrentKeyModifiers;
        private System.Windows.Forms.Label lblIsSaving;
    }
}