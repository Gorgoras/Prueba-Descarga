using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using Microsoft.Graph;
using System.IO;

namespace Prueba_Descarga.Helpers
{
    class MicrosoftHelper
    {
        public async Task<OneDriveClient> loginToOneDriveAPICuentaComun(string usuario)
        {
            OneDriveClient credential = null;
            string clientId = "21cbe2f1-86dd-4e67-9258-dfbfa2a6a41c";
            //string clientSecret = "dDV933Xq3y0UyUbKBzckgjf";
            string returnUrl = "https://login.live.com/oauth20_desktop.srf";
            string[] scopes = { "onedrive.readwrite", "wl.signin", "wl.offline_access" };


            var msaAuthProvider = new MsaAuthenticationProvider(
                clientId,
                //clientSecret,
                returnUrl,
                scopes,
                //null,
                new CredentialVault(clientId));


            credential = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthProvider);

            Task authTask = msaAuthProvider.RestoreMostRecentFromCacheOrAuthenticateUserAsync();

            try
            {
                await authTask;
            }
            catch (ServiceException exception)
            {
                if (OAuthConstants.ErrorCodes.AuthenticationFailure == exception.Error.Code)
                {
                    //problema de autenticacion
                    return null;
                }
                else
                {
                    //algo mas
                    return null;
                }
            }
            return credential;
        }

        public async Task<OneDriveClient> loginToOneDriveAPICuentaBusiness(string usuario)
        {
            OneDriveClient credential = null;
            string clientId = "21cbe2f1-86dd-4e67-9258-dfbfa2a6a41c";
            string clientSecret = "dDV933Xq3y0UyUbKBzckgjf";
            string returnUrl = "urn:ietf:wg:oauth:2.0:oob";
            string[] scopes = { "onedrive.readwrite", "wl.signin" };


            var msaAuthProvider = new MsaAuthenticationProvider(
                clientId,
                clientSecret,
                returnUrl,
                scopes,
                null,
                new CredentialVault(clientId));


            credential = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthProvider);

            Task authTask = msaAuthProvider.RestoreMostRecentFromCacheOrAuthenticateUserAsync();

            try
            {
                await authTask;
            }
            catch (ServiceException exception)
            {
                if (OAuthConstants.ErrorCodes.AuthenticationFailure == exception.Error.Code)
                {
                    //problema de autenticacion
                }
                else
                {
                    //algo mas
                }
            }
            return credential;
        }

        public async Task<List<Item>> getFiles(OneDriveClient usuario)
        {
            List<Item> listaArchivos = new List<Item>();

            //primero traemos la carpeta
            Item folder;
            folder = await usuario.Drive.Root.Request().Expand("thumbnails,children").GetAsync();

            //despues los archivos
            if (folder.Folder != null && folder.Children != null && folder.Children.CurrentPage != null)
            {
                var items = folder.Children.CurrentPage;

                foreach (Item obj in items)
                {
                    listaArchivos.Add(obj);
                }
            }
            return listaArchivos;
        }


        public async Task<string> downloadFile(string fileId, string fileName, OneDriveClient usuario)
        {
            string respuesta = "";
            Stream stream = await usuario.Drive.Items[fileId].Content.Request().GetAsync();

            var outputStream = new FileStream("E:\\Pruebas\\"+fileName, FileMode.Create);

            try
            {
                await stream.CopyToAsync(outputStream);

                //Sin lo que sigue no anda nada!! No olvidar
                stream.Dispose();
                stream.Close();
                outputStream.Dispose();
                outputStream.Close();
                respuesta = "Completo";

            }
            catch(Exception e)
            {
                e.Message.ToString();
                respuesta = "Ocurrio un error";
            }

            return respuesta;
        }
    }
}
