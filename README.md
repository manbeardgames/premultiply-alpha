# Premultiply Alpha
This is a command line (CLI) application that will preform alpha premultiplication on the color data in an image file.  One use for this, which is why I made it, is to premultiply the color data of the [BMFont]() exported PNG file.

## Usage
Download the current build of the application from the releases page and extract the exe, or clone the source of this repository with `git clone https://github.com/manbeardgames/premultiply-alpha.git` and build from source.

The following is the syntax for using the exe from a CLI.

```sh
premultiplyalpha.exe [input] [output]
```

Both `[input]` and `[output]` must be absolute paths, relative paths are not accepted and will throw an error.

You can use a directory path for `[input]` to process multiple files in a directory as a batch, but when doing this `[output]` must also be a directory.

**Single file process example**
```
premultiplyalpha.exe "C:\Users\UserName\Desktop\image.png" "C:\Users\UserName\Desktop\premultiplied_image.png"
```

**Directory/batch process example**
```
premultiplyalpha.exe "C:\Users\UserName\Desktop\Images\" "C:\Users\UserName\Desktop\ProcessedImages\"
```

## Third Party Libraries
This application uses [StbImageSharp]() and [StbImageWriteSharp]() to process the images and write them back to disk.

## License
```
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
```