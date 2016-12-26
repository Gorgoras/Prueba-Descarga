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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.radioMicrosoft = new System.Windows.Forms.RadioButton();
            this.radioGoogle = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblLoginSuccess = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link documento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(179, 94);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(355, 20);
            this.txtLink.TabIndex = 3;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(179, 146);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 20);
            this.txtPass.TabIndex = 4;
            this.txtPass.Text = "a";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(179, 120);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtUsuario.TabIndex = 5;
            // 
            // radioMicrosoft
            // 
            this.radioMicrosoft.AutoSize = true;
            this.radioMicrosoft.Location = new System.Drawing.Point(9, 12);
            this.radioMicrosoft.Name = "radioMicrosoft";
            this.radioMicrosoft.Size = new System.Drawing.Size(73, 17);
            this.radioMicrosoft.TabIndex = 6;
            this.radioMicrosoft.TabStop = true;
            this.radioMicrosoft.Text = "One Drive";
            this.radioMicrosoft.UseVisualStyleBackColor = true;
            // 
            // radioGoogle
            // 
            this.radioGoogle.AutoSize = true;
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
            this.btnDescargar.Location = new System.Drawing.Point(89, 235);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(198, 26);
            this.btnDescargar.TabIndex = 9;
            this.btnDescargar.Text = "Descargar";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 424);
            this.Controls.Add(this.lblLoginSuccess);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDescargar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.RadioButton radioMicrosoft;
        private System.Windows.Forms.RadioButton radioGoogle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblLoginSuccess;
    }
}

