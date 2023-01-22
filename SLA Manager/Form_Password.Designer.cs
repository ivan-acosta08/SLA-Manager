namespace SLA_Manager
{
    partial class Form_Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Password));
            this.txb_fp_password = new CustomControls.RJControls.RJTextBox();
            this.btn_fp_close = new System.Windows.Forms.Button();
            this.btn_pf_aceptar = new CustomControls.RJControls.RJButton();
            this.btn_pf_cancelar = new CustomControls.RJControls.RJButton();
            this.lbl_pf_label = new System.Windows.Forms.Label();
            this.pic_fp_visible = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_fp_visible)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_fp_password
            // 
            this.txb_fp_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.txb_fp_password.BorderColor = System.Drawing.Color.Transparent;
            this.txb_fp_password.BorderFocusColor = System.Drawing.Color.Transparent;
            this.txb_fp_password.BorderRadius = 5;
            this.txb_fp_password.BorderSize = 1;
            this.txb_fp_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txb_fp_password.ForeColor = System.Drawing.Color.White;
            this.txb_fp_password.Location = new System.Drawing.Point(13, 61);
            this.txb_fp_password.Margin = new System.Windows.Forms.Padding(4);
            this.txb_fp_password.MaxLength = 27;
            this.txb_fp_password.Multiline = false;
            this.txb_fp_password.Name = "txb_fp_password";
            this.txb_fp_password.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txb_fp_password.PasswordChar = true;
            this.txb_fp_password.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txb_fp_password.PlaceholderText = "";
            this.txb_fp_password.ReadOnly = false;
            this.txb_fp_password.Size = new System.Drawing.Size(340, 31);
            this.txb_fp_password.TabIndex = 302;
            this.txb_fp_password.Texts = "";
            this.txb_fp_password.UnderlinedStyle = false;
            this.txb_fp_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_fp_password_KeyPress);
            this.txb_fp_password.MouseEnter += new System.EventHandler(this.txb_fp_password_MouseEnter);
            this.txb_fp_password.MouseLeave += new System.EventHandler(this.txb_fp_password_MouseLeave);
            // 
            // btn_fp_close
            // 
            this.btn_fp_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.btn_fp_close.BackgroundImage = global::SLA_Manager.Properties.Resources.close_leave;
            this.btn_fp_close.FlatAppearance.BorderSize = 0;
            this.btn_fp_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btn_fp_close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btn_fp_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fp_close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_fp_close.ForeColor = System.Drawing.Color.Black;
            this.btn_fp_close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_fp_close.Location = new System.Drawing.Point(367, -1);
            this.btn_fp_close.Name = "btn_fp_close";
            this.btn_fp_close.Size = new System.Drawing.Size(32, 32);
            this.btn_fp_close.TabIndex = 303;
            this.btn_fp_close.TabStop = false;
            this.btn_fp_close.UseVisualStyleBackColor = false;
            this.btn_fp_close.Click += new System.EventHandler(this.btn_fp_close_Click);
            this.btn_fp_close.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btn_fp_close.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btn_pf_aceptar
            // 
            this.btn_pf_aceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(217)))));
            this.btn_pf_aceptar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(217)))));
            this.btn_pf_aceptar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_pf_aceptar.BorderRadius = 0;
            this.btn_pf_aceptar.BorderSize = 0;
            this.btn_pf_aceptar.FlatAppearance.BorderSize = 0;
            this.btn_pf_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pf_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_pf_aceptar.Location = new System.Drawing.Point(211, 108);
            this.btn_pf_aceptar.Name = "btn_pf_aceptar";
            this.btn_pf_aceptar.Size = new System.Drawing.Size(82, 40);
            this.btn_pf_aceptar.TabIndex = 304;
            this.btn_pf_aceptar.Text = "Aceptar";
            this.btn_pf_aceptar.TextColor = System.Drawing.Color.White;
            this.btn_pf_aceptar.UseVisualStyleBackColor = false;
            this.btn_pf_aceptar.Click += new System.EventHandler(this.btn_pf_aceptar_Click);
            // 
            // btn_pf_cancelar
            // 
            this.btn_pf_cancelar.BackColor = System.Drawing.Color.LightSlateGray;
            this.btn_pf_cancelar.BackgroundColor = System.Drawing.Color.LightSlateGray;
            this.btn_pf_cancelar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_pf_cancelar.BorderRadius = 0;
            this.btn_pf_cancelar.BorderSize = 0;
            this.btn_pf_cancelar.FlatAppearance.BorderSize = 0;
            this.btn_pf_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pf_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_pf_cancelar.Location = new System.Drawing.Point(305, 108);
            this.btn_pf_cancelar.Name = "btn_pf_cancelar";
            this.btn_pf_cancelar.Size = new System.Drawing.Size(82, 40);
            this.btn_pf_cancelar.TabIndex = 305;
            this.btn_pf_cancelar.Text = "Cancelar";
            this.btn_pf_cancelar.TextColor = System.Drawing.Color.White;
            this.btn_pf_cancelar.UseVisualStyleBackColor = false;
            this.btn_pf_cancelar.Click += new System.EventHandler(this.btn_fp_close_Click);
            // 
            // lbl_pf_label
            // 
            this.lbl_pf_label.AutoSize = true;
            this.lbl_pf_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_pf_label.ForeColor = System.Drawing.Color.White;
            this.lbl_pf_label.Location = new System.Drawing.Point(13, 27);
            this.lbl_pf_label.Name = "lbl_pf_label";
            this.lbl_pf_label.Size = new System.Drawing.Size(261, 17);
            this.lbl_pf_label.TabIndex = 306;
            this.lbl_pf_label.Text = "Ingresa la contraseña general para seguir...";
            // 
            // pic_fp_visible
            // 
            this.pic_fp_visible.BackgroundImage = global::SLA_Manager.Properties.Resources.visible;
            this.pic_fp_visible.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pic_fp_visible.ErrorImage = null;
            this.pic_fp_visible.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pic_fp_visible.InitialImage = null;
            this.pic_fp_visible.Location = new System.Drawing.Point(360, 65);
            this.pic_fp_visible.Name = "pic_fp_visible";
            this.pic_fp_visible.Size = new System.Drawing.Size(24, 24);
            this.pic_fp_visible.TabIndex = 307;
            this.pic_fp_visible.TabStop = false;
            this.pic_fp_visible.Click += new System.EventHandler(this.pic_fp_visible_Click);
            // 
            // Form_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(400, 160);
            this.Controls.Add(this.pic_fp_visible);
            this.Controls.Add(this.lbl_pf_label);
            this.Controls.Add(this.btn_pf_cancelar);
            this.Controls.Add(this.btn_pf_aceptar);
            this.Controls.Add(this.btn_fp_close);
            this.Controls.Add(this.txb_fp_password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Form_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SLA Manager";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pic_fp_visible)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.RJControls.RJTextBox txb_fp_password;
        private Button btn_fp_close;
        private CustomControls.RJControls.RJButton btn_pf_aceptar;
        private CustomControls.RJControls.RJButton btn_pf_cancelar;
        private Label lbl_pf_label;
        private PictureBox pic_fp_visible;
    }
}