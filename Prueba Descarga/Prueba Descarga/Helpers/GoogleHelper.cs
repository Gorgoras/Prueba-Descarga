using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
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
            new FileDataStore("PruebaDescarga.GoogleDrive.Auth.Store")).Result;
            //con esto guarda los tokens en %AppData% pero no es recomendable para una aplicacion web
            return credential;
            //Environment.UserName
        }

        public List<File> GetFiles(UserCredential userCred)
        {
            BaseClientService.Initializer init = new BaseClientService.Initializer();
            init.ApplicationName = "Prueba Descarga";
            init.HttpClientInitializer = userCred;
            DriveService service = new DriveService(init);

            List<File> Files = new List<File>();

            try
            {
                //Trae todos los archivos y carpetas del usuario, organizado en varias paginas.
                FilesResource.ListRequest list = service.Files.List();

                FileList filesFeed = list.Execute(); //trae 100 archivos (el tamaño estandar de una pagina de archivos)
                File archivoActual;


                while (filesFeed != null)
                {
                    for (int i = 0; i < filesFeed.Files.Count; i++) //paso cada archivo a la lista
                    {
                        archivoActual = filesFeed.Files.ElementAt<File>(i);
                        Files.Add(archivoActual);

                    }

                    /*  Para acelerar la carga de los archivos, voy a trabajar con los 100 primeros
                     *  nada mas, descomentar lo que esta abajo y eliminar el break al final para 
                     *  traer todos los archivos. 
                     * 
                     * 
                    if (filesFeed.NextPageToken == null) //si ya no hay mas paginas siguientes, corto el while
                    {
                        break;
                    }


                    list.PageToken = filesFeed.NextPageToken; //cambiamos el page token a la pagina que sigue
                    filesFeed = list.Execute(); //trae los archivos que estan en la pagina indicada anteriormente
                    */
                    break;
                }
            }
            catch (Exception ex)
            {
                // In the event there is an error with the request.
                Console.WriteLine(ex.Message);
            }
            return Files;
        }

        public async Task<string> downloadFile(string fileId, string fileName, UserCredential userGoogle)
        {
            BaseClientService.Initializer init = new BaseClientService.Initializer();
            init.ApplicationName = "Prueba Descarga";
            init.HttpClientInitializer = userGoogle;
            DriveService service = new DriveService(init);
            string respuesta = "";

            try
            {
                // var pList = service.Permissions.List(fileId);
                // pList.OauthToken = userGoogle.Token.AccessToken;
                // var perm = pList.Execute();
                // Permission perm = service.Permissions.Get(fileId, pList.Permissions[0].Id).Execute();

                var fileStream = System.IO.File.Create("E:\\Pruebas\\" + fileName);

                await service.Files.Get(fileId).DownloadAsync(fileStream);

                respuesta = "Completo";

                fileStream.Dispose();
                fileStream.Close();
            }
            catch (GoogleApiException e)
            {
                respuesta = e.Message.ToString();

                if (e.Error.Code == 403) respuesta = "No tiene permisos suficientes";
            }
            return respuesta;
        }
    }

}
