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

namespace Prueba_Descarga
{
    public partial class Form1 : Form
    {
        private string linkDocumento;
        private string usuario;
        private string pass;
        private UserCredential userGoogle;
        private GoogleHelper google;
        private MicrosoftHelper microsft;

        public Form1()
        {
            InitializeComponent();
            google = new GoogleHelper();
            microsft = new MicrosoftHelper();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
           
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

        private void button1_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuario.Text;
            userGoogle = google.loginToGoogleDriveAPI(usuario);
            if(userGoogle != null)
            {
                lblLoginSuccess.Text = "Exito!";
                lblLoginSuccess.ForeColor = Color.Green;
                btnList.Enabled = true;
            }
            else
            {
                lblLoginSuccess.Text = "Fracaso =(";
                lblLoginSuccess.ForeColor = Color.Red;
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            google.GetFiles(userGoogle);
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
