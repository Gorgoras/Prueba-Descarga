using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Prueba_Descarga.Helpers;
using Microsoft.OneDrive.Sdk;

namespace Prueba_Descarga
{
    public partial class Form1 : Form
    {
        private UserCredential userGoogle;
        private GoogleHelper google;

        private MicrosoftHelper microsoft;
        private OneDriveClient userMicrosoft;

        public Form1()
        {
            InitializeComponent();
            google = new GoogleHelper();
            microsoft = new MicrosoftHelper();
        }



        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            lblDownloadStatus.Text = "Downloading...";
            if (radioGoogle.Checked)
            {
                //dgvFiles
                int nroFila = Int32.Parse(dgvFiles.SelectedRows[0].Index.ToString());
                string fileId = dgvFiles.Rows[nroFila].Cells[1].Value.ToString();
                string fileName = dgvFiles.Rows[nroFila].Cells[0].Value.ToString();

                lblDownloadStatus.Text = await google.downloadFile(fileId, fileName, userGoogle);
            }
            else
            {
                //logica para One Drive
                int nroFila = Int32.Parse(dgvFiles.SelectedRows[0].Index.ToString());
                string fileId = dgvFiles.Rows[nroFila].Cells[1].Value.ToString();
                string fileName = dgvFiles.Rows[nroFila].Cells[0].Value.ToString();


                lblDownloadStatus.Text = await microsoft.downloadFile(fileId, fileName, userMicrosoft);



            }
        }



        private void descargarMicrosoftOneDrive()
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            if (radioGoogle.Checked)
            {
                //logica para logueo en Google Drive
                userGoogle = google.loginToGoogleDriveAPI(usuario);
                if (userGoogle != null)
                {
                    lblLoginSuccess.Text = "Exito!";
                    lblLoginSuccess.ForeColor = Color.Green;
                    btnList.Enabled = true;
                    radioGoogle.Enabled = false;
                    radioMicrosoft.Enabled = false;
                }
                else
                {
                    lblLoginSuccess.Text = "Fracaso =(";
                    lblLoginSuccess.ForeColor = Color.Red;
                }
            }
            else
            {
                //logica para logueo en One Drive

                userMicrosoft = await microsoft.loginToOneDriveAPICuentaComun(usuario);
                if (userMicrosoft != null)
                {
                    lblLoginSuccess.Text = "Exito!";
                    lblLoginSuccess.ForeColor = Color.Green;
                    btnList.Enabled = true;
                    radioGoogle.Enabled = false;
                    radioMicrosoft.Enabled = false;
                }
                else
                {
                    lblLoginSuccess.Text = "Fracaso =(";
                    lblLoginSuccess.ForeColor = Color.Red;
                }
            }
        }

        private async void btnList_Click(object sender, EventArgs e)
        {
            dgvFiles.AutoGenerateColumns = false;
            dgvFiles.MultiSelect = false;
            dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblGetList.Text = "Reading...";

            if (radioGoogle.Checked)
            {
                //Creo las columnas a mostrar
                dgvFiles.Columns.Add("Name", "Name");
                dgvFiles.Columns.Add("Id", "Id");
                dgvFiles.Columns.Add("MimeType", "MimeType");

                List<Google.Apis.Drive.v3.Data.File> listaArchivos = google.GetFiles(userGoogle);
                Google.Apis.Drive.v3.Data.File archivoActual;
                string[] fila;

                //Cargo las filas
                for (int i = 0; i < listaArchivos.Count; i++)
                {
                    archivoActual = listaArchivos.ElementAt(i);
                    fila = new string[] { archivoActual.Name, archivoActual.Id, archivoActual.MimeType };
                    dgvFiles.Rows.Add(fila);
                }

                //Habilito boton descargar para que se pueda seleccionar un archivo de la lista y descargarlo
                btnDescargar.Enabled = true;
                lblGetList.Text = "Done";

            }
            else
            {
                //Creo las columnas a mostrar
                dgvFiles.Columns.Add("Name", "Name");
                dgvFiles.Columns.Add("Id", "Id");
                dgvFiles.Columns.Add("Kind", "Kind");


                //logica para One Drive
                List<Microsoft.OneDrive.Sdk.Item> listaArchivos = await microsoft.getFiles(userMicrosoft);



                Microsoft.OneDrive.Sdk.Item archivoActual;
                string[] fila;

                //Cargo las filas
                for (int i = 0; i < listaArchivos.Count; i++)
                {
                    archivoActual = listaArchivos.ElementAt(i);
                    fila = new string[] { archivoActual.Name, archivoActual.Id, "Averiguar esto" };
                    dgvFiles.Rows.Add(fila);
                }

                //Habilito boton descargar para que se pueda seleccionar un archivo de la lista y descargarlo
                btnDescargar.Enabled = true;
                lblGetList.Text = "Done";
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
