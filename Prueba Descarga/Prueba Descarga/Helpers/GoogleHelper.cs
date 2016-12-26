using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba_Descarga.Helpers
{
    class GoogleHelper
    {

        public UserCredential loginToGoogleDriveAPI(string usuario)
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
            usuario,
            CancellationToken.None,
            new FileDataStore("Daimto.GoogleDrive.Auth.Store")).Result;
            //con esto guarda los tokens en %AppData% pero no es recomendable para una aplicacion web
            return credential;
            //Environment.UserName
        }

        public IList GetFiles(UserCredential userCred)
        {
            BaseClientService.Initializer init = new BaseClientService.Initializer();
            init.ApplicationName = "Prueba Descarga";
            init.HttpClientInitializer = userCred;
            DriveService service = new DriveService(init);

            IList Files = new List<File>();

            try
            {
                //List all of the files and directories for the current user.  
                // Documentation: https://developers.google.com/drive/v2/reference/files/list
                FilesResource.ListRequest list = service.Files.List();

                FileList filesFeed = list.Execute(); //trae 100 archivos
                File archivoActual;


                while (filesFeed != null)
                {
                    for (int i = 0; i < filesFeed.Files.Count; i++)
                    {
                        archivoActual = filesFeed.Files.ElementAt<File>(i);
                        Files.Add(archivoActual);

                    }
                    if (filesFeed.NextPageToken == null)
                    {
                        break;
                    }
                    list.PageToken = filesFeed.NextPageToken;
                    filesFeed = list.Execute();
                }
            }
            catch (Exception ex)
            {
                // In the event there is an error with the request.
                Console.WriteLine(ex.Message);
            }
            return Files;
        }
    }
   
}
