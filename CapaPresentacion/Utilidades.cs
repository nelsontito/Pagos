using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace CapaPresentacion
{
    public class Utilidades
    {
        #region "PATRON SINGLETON"
        private static Utilidades conexion = null;

        private Utilidades() { }

        public static Utilidades GetInstance()
        {
            if (conexion == null)
            {
                conexion = new Utilidades();
            }
            return conexion;
        }
        #endregion}

        public string UploadPhoto(MemoryStream stream, string folder)
        {
            string rutaa = "";

            try
            {
                stream.Position = 0;

                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                var fullPath = $"{folder}{file}";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), file);

                // Guardar la imagen en el sistema de archivos
                File.WriteAllBytes(path, stream.ToArray());

                // Verificar si el archivo fue guardado correctamente
                if (File.Exists(path))
                {
                    rutaa = fullPath;
                }
            }
            catch (IOException)
            {
                rutaa = "";
            }
            catch (Exception)
            {
                rutaa = "";
            }
            return rutaa;
        }

        public string Hash(string password)
        {
            // Validamos que no nos envíen contraseñas vacías
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            // Encripta la contraseña. BCrypt genera y aplica el "Salt" automáticamente
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            // Validamos que ninguno de los dos sea nulo o vacío
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            // Verifica si la contraseña en texto plano coincide con el hash de la BD
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string GenerarQr(string contenido)
        {
            string rutaRelativa = "";
            try
            {
                if (string.IsNullOrWhiteSpace(contenido))
                    return "";

                string fileName = $"{Guid.NewGuid()}.png";
                string relativePath = "/imgqr/";
                string folder = HttpContext.Current.Server.MapPath(relativePath);
                string fullPath = Path.Combine(folder, fileName);

                // Asegurar que la carpeta existe
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // Generar QR
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    File.WriteAllBytes(fullPath, ms.ToArray());
                }

                // Confirmar existencia y devolver ruta relativa
                if (File.Exists(fullPath))
                    rutaRelativa = $"{relativePath}{fileName}";
            }
            catch (Exception)
            {
                // Log opcional
                // Logger.LogError("Error al generar QR: " + ex.Message);
                rutaRelativa = "";
            }

            return rutaRelativa;
        }

    }
}