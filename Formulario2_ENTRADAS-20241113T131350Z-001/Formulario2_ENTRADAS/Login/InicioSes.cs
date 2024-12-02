using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formulario_ENTRADAS.Login
{
    public partial class InicioSes : Form
    {
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCancel;
        public InicioSes()
        {
            InitializeComponent();
            
            this.Text = "Inicio de Sesión";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            
            Label lblEmail = new Label()
            {
                Text = "Correo (solo pan.ni):",
                Location = new System.Drawing.Point(200, 100),
                AutoSize = true
            };
            txtEmail = new TextBox()
            {
                Location = new System.Drawing.Point(200, 130),
                Width = 200
            };

            Label lblPassword = new Label()
            {
                Text = "Contraseña:",
                Location = new System.Drawing.Point(200, 180),
                AutoSize = true
            };
            txtPassword = new TextBox()
            {
                Location = new System.Drawing.Point(200, 210),
                Width = 200,
                PasswordChar = '*'
            };

            btnLogin = new Button()
            {
                Text = "Iniciar sesión",
                Location = new System.Drawing.Point(200, 260),
                TabIndex = 2
            };
            btnCancel = new Button()
            {
                Text = "Cancelar",
                Location = new System.Drawing.Point(310, 260),
                TabIndex = 3
            };

            
            txtEmail.TabIndex = 0;
            txtPassword.TabIndex = 1;

            
            btnLogin.Click += new EventHandler(btnLogin_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            this.KeyDown += new KeyEventHandler(Form_KeyDown); 

            
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnCancel);
        }

        private bool ValidateEmail(string email)
        {
            
            string pattern = @"^[a-zA-ZñÑ]+@pan\.ni$"; 
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, pattern))
            {
                MessageBox.Show("El correo debe contener solo letras (incluida la ñ) y terminar con '@pan.ni'.");
                return false;
            }
            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                return false;
            }
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (ValidateEmail(email) && ValidatePassword(password))
            {
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnLogin.Focused)
                {
                    btnLogin.PerformClick(); 
                }
                else if (btnCancel.Focused)
                {
                    btnCancel.PerformClick(); 
                }
            }
        }

    }
}
