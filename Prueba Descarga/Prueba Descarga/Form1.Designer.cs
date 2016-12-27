namespace Prueba_Descarga
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.radioMicrosoft = new System.Windows.Forms.RadioButton();
            this.radioGoogle = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblLoginSuccess = new System.Windows.Forms.Label();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.btnList = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link documento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(368, 38);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(355, 20);
            this.txtLink.TabIndex = 3;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(368, 12);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtUsuario.TabIndex = 5;
            this.txtUsuario.Text = "martinzurita1";
            // 
            // radioMicrosoft
            // 
            this.radioMicrosoft.AutoSize = true;
            this.radioMicrosoft.Location = new System.Drawing.Point(9, 12);
            this.radioMicrosoft.Name = "radioMicrosoft";
            this.radioMicrosoft.Size = new System.Drawing.Size(73, 17);
            this.radioMicrosoft.TabIndex = 6;
            this.radioMicrosoft.Text = "One Drive";
            this.radioMicrosoft.UseVisualStyleBackColor = true;
            // 
            // radioGoogle
            // 
            this.radioGoogle.AutoSize = true;
            this.radioGoogle.Checked = true;
            this.radioGoogle.Location = new System.Drawing.Point(9, 48);
            this.radioGoogle.Name = "radioGoogle";
            this.radioGoogle.Size = new System.Drawing.Size(87, 17);
            this.radioGoogle.TabIndex = 7;
            this.radioGoogle.TabStop = true;
            this.radioGoogle.Text = "Google Drive";
            this.radioGoogle.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioGoogle);
            this.panel1.Controls.Add(this.radioMicrosoft);
            this.panel1.Location = new System.Drawing.Point(89, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 75);
            this.panel1.TabIndex = 8;
            // 
            // btnDescargar
            // 
            this.btnDescargar.Enabled = false;
            this.btnDescargar.Location = new System.Drawing.Point(89, 253);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(198, 26);
            this.btnDescargar.TabIndex = 9;
            this.btnDescargar.Text = "Download";
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblLoginSuccess
            // 
            this.lblLoginSuccess.AutoSize = true;
            this.lblLoginSuccess.Location = new System.Drawing.Point(293, 200);
            this.lblLoginSuccess.Name = "lblLoginSuccess";
            this.lblLoginSuccess.Size = new System.Drawing.Size(0, 13);
            this.lblLoginSuccess.TabIndex = 11;
            // 
            // dgvFiles
            // 
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Location = new System.Drawing.Point(368, 79);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.Size = new System.Drawing.Size(724, 461);
            this.dgvFiles.TabIndex = 12;
            // 
            // btnList
            // 
            this.btnList.Enabled = false;
            this.btnList.Location = new System.Drawing.Point(89, 224);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(198, 23);
            this.btnList.TabIndex = 13;
            this.btnList.Text = "Get list of files";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 552);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.lblLoginSuccess);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDescargar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.RadioButton radioMicrosoft;
        private System.Windows.Forms.RadioButton radioGoogle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblLoginSuccess;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.Button btnList;
    }
}

