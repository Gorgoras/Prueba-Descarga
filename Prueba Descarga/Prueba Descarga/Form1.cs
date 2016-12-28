using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Logging;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Collections;
using Google.Apis.Drive.v3.Data;
using Prueba_Descarga.Helpers;
using Microsoft.OneDrive.Sdk.Authentication;
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
            }
        }



        private async void descargarGoogleDrive(string link, UserCredential user)
        {
            BaseClientService.Initializer init = new BaseClientService.Initializer();
            init.ApplicationName = "Prueba Descarga";
            init.HttpClientInitializer = userGoogle;
            DriveService service = new DriveService(init);


            var stream = new System.IO.MemoryStream();
            var request = service.Files.Get(link);
            var fileStream = System.IO.File.Create("E:\\Pruebas\\test");
            request.MediaDownloader.ProgressChanged +=
            (IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            await request.DownloadAsync(stream);
            stream.CopyTo(System.IO.File.Create("E:\\Pruebas\\test"));
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
                userMicrosoft = await microsoft.loginToOneDriveAPICuentaBusiness(usuario);

            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            dgvFiles.AutoGenerateColumns = false;
            dgvFiles.MultiSelect = false;
            dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (radioGoogle.Checked)
            {
                //Creo las columnas a mostrar
                dgvFiles.Columns.Add("Name", "Name");
                dgvFiles.Columns.Add("Id", "Id");
                dgvFiles.Columns.Add("Kind", "Kind");
                dgvFiles.Columns.Add("MimeType", "MimeType");
           
                List<Google.Apis.Drive.v3.Data.File> listaArchivos = google.GetFiles(userGoogle);
                Google.Apis.Drive.v3.Data.File archivoActual;
                string[] fila;
                
                //Cargo las filas
                for(int i = 0; i < listaArchivos.Count; i++)
                {
                    archivoActual = listaArchivos.ElementAt(i);
                    fila = new string[] { archivoActual.Name, archivoActual.Id, archivoActual.Kind, archivoActual.MimeType };
                    dgvFiles.Rows.Add(fila);
                }

                //Habilito boton descargar para que se pueda seleccionar un archivo de la lista y descargarlo
                btnDescargar.Enabled = true;
                
            }else
            {
                //logica para One Drive
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
