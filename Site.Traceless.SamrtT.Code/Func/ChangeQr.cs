using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ZXing;

namespace Site.Traceless.SamrtT.Code.Func
{
    public class ChangeQr
    {
        public static string changeQr(string path)
        {
            byte[] _ImageBytes = File.ReadAllBytes(path);
            Image image = Image.FromStream(new MemoryStream(_ImageBytes));

            Bitmap bitmap = new Bitmap(image);
            BarcodeReader reader = new BarcodeReader();
            reader.AutoRotate = true;
            Result result = reader.Decode(bitmap);
            if (null == result)
            {
                return "";
            }

            ResultPoint[] points;
            points = result.ResultPoints;
            int margin = 1;
            float xMin = points.Select(p => p.X).Min() - margin;
            float xMax = points.Select(p => p.X).Max() + margin;
            float yMin = points.Select(p => p.Y).Min() - margin;
            float yMax = points.Select(p => p.Y).Max() + margin;

            string qrPath = Common.CqApi.AppDirectory + "changeqr.jpg";
            if (!File.Exists(qrPath))
            {
                return "[换码] 无需替换二维码";
            }
            byte[] _ImageBytesQr = File.ReadAllBytes(qrPath);
            Image imageQr = Image.FromStream(new MemoryStream(_ImageBytesQr));
            using (Graphics g = Graphics.FromImage(image))
            {
                string pathDic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "image");
                string fileName = DateTime.Now.Ticks + ".jpg";
                string name = Path.Combine(pathDic, fileName);
                g.DrawImage(imageQr, new Rectangle(new Point((int)xMin, (int)yMin), new Size((int)(xMax - xMin), (int)(yMax - yMin))), 0, 0, imageQr.Width, imageQr.Height, GraphicsUnit.Pixel);
                image.Save(name, ImageFormat.Jpeg);
                return new CQCode(CQFunction.Image, new KeyValuePair<string, string>("file", fileName)).ToSendString();
            }
        }
    }
}