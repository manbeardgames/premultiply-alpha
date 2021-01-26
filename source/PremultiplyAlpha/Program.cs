/* ----------------------------------------------------------------------------
    MIT License

    Copyright (c) 2020 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
---------------------------------------------------------------------------- */

using StbImageSharp;
using StbImageWriteSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace PremultiplyAlpha
{
    class Program
    {
        //  string value of the search patterns to use for the acceptable file
        //  extensions, each sepearted by a pipe '|' character.
        private const string ACCEPTED_EXTENSIONS = "*.png|*.bmp|*.jpg|*.tga";

        //  The input path given by the user in the cli arguments.
        private static string _inputPath;

        //  The output path given by the user in the cli arguments.
        private static string _outputPath;

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                WriteHelpText();
                Environment.Exit(0);
            }

            //  The first argument should be the input path.
            _inputPath = args[0];

            //  The second argument should be the output path. 
            _outputPath = args[1];

            //  Validate that both paths are absolute paths and not relative
            if (!Path.IsPathRooted(_inputPath))
            {
                throw new Exception("[input] path must be an abolute path. Relative paths are not accepted.");
            }

            if (!Path.IsPathRooted(_outputPath))
            {
                throw new Exception("[output] path must be an absolute path. Relative paths are not accepted.");
            }

            //  Determine if the input path is a directory
            FileAttributes inputAttributes = File.GetAttributes(_inputPath);
            bool isDirectory = (inputAttributes & FileAttributes.Directory) == FileAttributes.Directory;


            if (isDirectory)
            {
                //  If the input path is a directory, then the output path must also be a directory.
                FileAttributes outputAttributes = File.GetAttributes(_outputPath);
                if ((outputAttributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    throw new Exception("If [input] path is a directory, then [output] must also be a directory");
                }

                //  Since Directory.GetFiles doesn't accept multiple file type search patterns,
                //  we need to do a search using each of the acceptable extensions and build a 
                //  list of the files to process.
                string[] formats = ACCEPTED_EXTENSIONS.Split('|');
                List<string> files = new List<string>();
                for (int i = 0; i < formats.Length; i++)
                {
                    files.AddRange(Directory.GetFiles(_inputPath, formats[i]));
                }

                //  Process each of the files
                for (int i = 0; i < files.Count; i++)
                {
                    string outputFile = Path.Combine(_outputPath, Path.GetFileName(files[i]));
                    PremultiplyAlpha(files[i], outputFile);
                }
            }
            else
            {
                //  Since input was a file, then the output path must also be a file
                FileAttributes outputAttributes = File.GetAttributes(_outputPath);
                if ((outputAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    throw new Exception("If [input] is a file, then [output] path must also be a file");
                }

                //  Process the file
                PremultiplyAlpha(_inputPath, _outputPath);
            }
        }

        /// <summary>
        ///     Premultiplies the alpha of hte given <paramref name="input"/> file and saves it
        ///     to disc at the <paramref name="output"/> location.
        /// </summary>
        /// <param name="input">
        ///     A <see cref="string"/> value containing the fully qualified absolute path
        ///     to the image file to premultiply the alpha of.
        /// </param>
        /// <param name="output">
        ///     A <see cref="string"/> value containing the fully qualified absolute path
        ///     to the destination to save the processed image file to.
        /// </param>
        private static void PremultiplyAlpha(string input, string output)
        {
            //  Load the image using StbImageSharp
            ImageResult image = null;
            using (var stream = File.OpenRead(input))
            {
                image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);
            }

            //  Get the pixel data of the image. Every 4bytes contains the RGBA values.
            byte[] data = image.Data;
            int pixels = image.Width * image.Height;

            //  Premultiply the alpha
            for (int i = 0; i < pixels; i++)
            {
                int r = data[i * 4];
                int g = data[(i * 4) + 1];
                int b = data[(i * 4) + 2];
                int a = data[(i * 4) + 3];


                r = r * a / 255;
                g = g * a / 255;
                b = b * a / 255;

                data[i * 4] = (byte)r;
                data[(i * 4) + 1] = (byte)g;
                data[(i * 4) + 2] = (byte)b;
            }


            string extension = Path.GetExtension(output);

            //  Save the image to disc
            using (Stream stream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                ImageWriter writer = new ImageWriter();
                switch (extension)
                {
                    case ".png":
                        writer.WritePng(data, image.Width, image.Height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
                        break;
                    case ".bmp":
                        writer.WriteBmp(data, image.Width, image.Height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
                        break;
                    case ".jpg":
                        writer.WriteJpg(data, image.Width, image.Height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream, 0);
                        break;
                    case ".tga":
                        writer.WriteTga(data, image.Width, image.Height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
                        break;
                    default:
                        throw new Exception("Unrecognized filetype");
                }
            }
        }

        /// <summary>
        ///     Outputs the help text to the console window.
        /// </summary>
        private static void WriteHelpText()
        {
            Console.WriteLine("Performs alpha premultiply on the color data of an image" + Environment.NewLine);
            Console.WriteLine("premultiplyalpha [input] [output]" + Environment.NewLine);
            Console.WriteLine("[input]\t\tThe absolute path to the image file to process");
            Console.WriteLine("[output]\tThe absolute path to where to save the image after processing." + Environment.NewLine);
            Console.WriteLine("You can use a directory path for the [input] to process multiple images in a batch.");
            Console.WriteLine("However when doing this the [output] value must also be a directory");
        }
    }
}
