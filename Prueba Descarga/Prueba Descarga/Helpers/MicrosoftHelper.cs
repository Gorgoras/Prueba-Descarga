using System;
using System.Collections.Generic;
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
            string clientSecret = "dDV933Xq3y0UyUbKBzckgjf";
            string returnUrl = "https://login.live.com/oauth20_desktop.srf";
            string[] scopes = { "onedrive.readwrite", "wl.signin", "wl.offline_access" };

            MsaAuthenticationProvider msaAuthProvider;

            CredentialCache cred = new CredentialCache();
            byte[] credBlob = cargarCredentialCacheBlob();
            cred.InitializeCacheFromBlob(credBlob);

            if (credBlob != null)
            {
                msaAuthProvider = new MsaAuthenticationProvider(
                    clientId, clientSecret, returnUrl, scopes, cred, new CredentialVault(clientId));
            }
            else
            {
                msaAuthProvider = new MsaAuthenticationProvider(
                    clientId, returnUrl, scopes, new CredentialVault(clientId));
            }



            try
            {
                await msaAuthProvider.RestoreMostRecentFromCacheAsync(clientId);
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

            if (!msaAuthProvider.IsAuthenticated)
            {
                try
                {
                    msaAuthProvider = new MsaAuthenticationProvider(
       clientId, returnUrl, scopes, new CredentialVault(clientId));
                    await msaAuthProvider.AuthenticateUserAsync(clientId);
                }
                catch
                { }
            }
            if (msaAuthProvider.IsAuthenticated)
            {
                credential = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthProvider);
                guardarCredentialCacheBlob(msaAuthProvider.CredentialCache.GetCacheBlob());

            }
            else
            {
                credential = null;
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

            var outputStream = new FileStream("E:\\Pruebas\\" + fileName, FileMode.Create);

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
            catch (Exception e)
            {
                e.Message.ToString();
                respuesta = "Ocurrio un error";
            }

            return respuesta;
        }

        private void guardarCredentialCacheBlob(byte[] refresh)
        {
            string path = @"e:\Pruebas\MyTest.txt";

            System.IO.File.WriteAllBytes(path, refresh);

        }

        private byte[] cargarCredentialCacheBlob()
        {
            string path = @"e:\Pruebas\MyTest.txt";
            byte[] readText = null;

            try
            {
                readText = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                e.ToString();
            }

            if (readText.Length < 2)
            {
                return null;
            }
            return readText;
        }
    }
}
