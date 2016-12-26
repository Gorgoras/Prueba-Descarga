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

namespace Prueba_Descarga
{
    public partial class Form1 : Form
    {
        private string linkDocumento;
        private string usuario;
        private string pass;
        private UserCredential userGoogle;
       

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            linkDocumento = txtLink.Text;
            usuario = txtUsuario.Text;
            pass = txtPass.Text;

            if (radioMicrosoft.Checked)
            {
                descargarMicrosoftOneDrive();
            }

            if (radioGoogle.Checked)
            {
                descargarGoogleDrive(linkDocumento, userGoogle);
            }

        }

        private UserCredential loginToGoogleDriveAPI(string usuario)
        {
            //Scopes for use with the Google Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive,
                                 DriveService.Scope.DriveFile};
            var clientId = "958470112648-f26et699num6vs6ma1d7iha8gjpofngl.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "cTq0zQ309fG_6O5MU-eSJKl9";          // From https://console.developers.google.com
                                               // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
            scopes,
            Environment.UserName,
            CancellationToken.None,
            new FileDataStore("Daimto.GoogleDrive.Auth.Store")).Result;
            return credential;
        }

        private async void descargarGoogleDrive(string link, UserCredential user)
        {

            DriveService service = new DriveService();


            link = "https://www.googleapis.com/drive/v3/files";
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
            userGoogle = loginToGoogleDriveAPI(usuario);
            if(userGoogle != null)
            {
                lblLoginSuccess.Text = "Exito!";
                lblLoginSuccess.ForeColor = Color.Green;
            }
            else
            {
                lblLoginSuccess.Text = "Fracaso =(";
                lblLoginSuccess.ForeColor = Color.Red;
            }
        }
    }
}
