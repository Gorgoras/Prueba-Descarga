using Google;
using Google.Apis.Auth.OAuth2;
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

            //Estos 2 te los dan al registrar la app en la pagina de google.
            var clientId = "958470112648-f26et699num6vs6ma1d7iha8gjpofngl.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "cTq0zQ309fG_6O5MU-eSJKl9";          // From https://console.developers.google.com
                                                                    // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%

            FileDataStore fds = new FileDataStore(@"e:\Pruebas\" + $"{usuario}", true);
            /*Ejemplo de ruta personalizada para cada usuario, para algo mas generico usar la definicion
             de abajo.*/


            //FileDataStore fds = new FileDataStore("PruebaDescarga.GoogleDrive.Auth.Store" + $"{usuario}", false);
            /*con esto guarda los tokens en AppData/Roaming pero no es recomendable para una
            aplicacion web, al crear FileDataStore se puede especificar la ruta poniendo
            el segundo parametro en true.*/

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
            scopes,
            usuario,
            CancellationToken.None,
            fds).Result;
            /*con esto guarda los tokens en AppData/Roaming pero no es recomendable para una
            aplicacion web, al crear FileDataStore se puede especificar la ruta poniendo
            el segundo parametro en true.*/

            return credential;
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


                list.Corpus = FilesResource.ListRequest.CorpusEnum.Domain;
                list.PageSize = 150; //Archivos a traer por pagina, por defecto 100.

                FileList filesFeed = list.Execute(); //trae una pagina de archivos
                File archivoActual;


                while (filesFeed != null)
                {
                    for (int i = 0; i < filesFeed.Files.Count; i++) //paso cada archivo de la pagina actual a la lista
                    {
                        archivoActual = filesFeed.Files.ElementAt<File>(i);
                        
                        /*no muestro los archivos creados con google docs porque no se pueden
                        descargar con la api*/
                        if (!archivoActual.MimeType.StartsWith("application/vnd.google"))
                        {
                            Files.Add(archivoActual);
                        }
                    }

                    /*  Para acelerar la carga de los archivos, voy a trabajar con los 100 primeros
                     *  nada mas, descomentar lo que esta abajo y eliminar el break al final para 
                     *  traer todos los archivos. 
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
