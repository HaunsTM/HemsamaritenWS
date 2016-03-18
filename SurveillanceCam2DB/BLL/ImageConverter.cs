namespace Tellstick.BLL
{
    using System;

    using BLL.Interfaces;

    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using ImageProcessor;
    using ImageProcessor.Common.Exceptions;
    using ImageProcessor.Imaging;

    using log4net;

    public class ImageConverter : IImageConverter
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constructors

        public ImageConverter()
        {
        }

        #endregion
        

        /// <summary>
        /// Returns the image as byte[] in the specified format.
        /// </summary>
        /// <returns></returns>
        public byte[] ImageToByteArray(Image imageIn, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Returns the image as byte[] in the specified format.
        /// </summary>
        /// <returns></returns>
        public byte[] ImageToByteArray(Image imageIn, ImageFormat format, int imageQualityPercent)
        {
            log.Debug(String.Format("Going to convert Image to byte[]. Original data for image: img.Width = {0}; img.Height = {1}; img.HorizontalResolution = {2}; img.VerticalResolution ={3}", imageIn.Width, imageIn.Height, imageIn.HorizontalResolution, imageIn.VerticalResolution));
            if (!Equals(format, ImageFormat.Jpeg))
            {
                throw new ImageFormatException("Image must derive from JPEG!");
            }
           
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, format);
                var imageBytes = ms.ToArray();

                using (MemoryStream inStream = new MemoryStream(imageBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Quality(imageQualityPercent)
                                        .Save(outStream);
                        }

                        // Do something with the stream.
                        return outStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Returns the image as byte[] in the specified format.
        /// </summary>
        /// <returns></returns>
        public byte[] ImageToByteArray(Image imageIn, ImageFormat format, int imageQualityPercent, Size newImageSize, bool preserveImageAspectRatio = true)
        {
            int newWidth;
            int newHeight;

            log.Debug(String.Format("Going to convert Image to byte[]. Original data for image: img.Width = {0}; img.Height = {1}; img.HorizontalResolution = {2}; img.VerticalResolution ={3}", imageIn.Width, imageIn.Height, imageIn.HorizontalResolution, imageIn.VerticalResolution));

            if (!Equals(format, ImageFormat.Jpeg))
            {
                throw new ImageFormatException("Image must derive from JPEG!");
            }


            if (preserveImageAspectRatio)
            {
                int originalWidth = imageIn.Width;
                int originalHeight = imageIn.Height;
                float percentWidth = (float)newImageSize.Width / (float)originalWidth;
                float percentHeight = (float)newImageSize.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = newImageSize.Width;
                newHeight = newImageSize.Height;
            }

            newImageSize = new Size(newWidth, newHeight);

            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, format);
                var imageBytes = ms.ToArray();

                using (MemoryStream inStream = new MemoryStream(imageBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Quality(imageQualityPercent)
                                        .Resize(newImageSize)
                                        .Save(outStream);
                        }

                        // Do something with the stream.
                        return outStream.ToArray();
                    }
                }
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        #region Compressed

        public byte[] ImageToCompressedByteArray(Image imageIn, ImageFormat format)
        {
            var uncompressedByteArray = this.ImageToByteArray(imageIn, format);
            var byteArrayDealer = new ByteArrayDealer();
            var compressedByteArray = byteArrayDealer.Compress(uncompressedByteArray);
            return compressedByteArray;
        }

        public Image CompressedByteArrayToImage(byte[] compressedByteArrayIn)
        {
            var byteArrayDealer = new ByteArrayDealer();
            var decompressedByteArray = byteArrayDealer.Decompress(compressedByteArrayIn);
            return this.ByteArrayToImage(decompressedByteArray);
        }

        #endregion

    }
}
