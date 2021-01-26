# Premultiply Alpha
This is a command line (CLI) application that will preform alpha premultiplication on the color data in an image file.  One use for this, which is why I made it, is to premultiply the color data of the [BMFont](https://www.angelcode.com/products/bmfont/) exported PNG file.

## Download
You can download the current build of the application for your operating system from the [releases](https://github.com/manbeardgames/premultiply-alpha/releases/tag/v1.0.0) page.

Alternatively, if you wish to build from source, first clone the repository with the following command

```
git clone https://github.com/manbeardgames/premultiply-alpha.git
```

Then execute the **build.sh** file to build from source.  Once built, locate the new **/build** directory.  Inside this directory will be four directories. The executable you need is based on the operating system you are using.

## Usage
The following is the syntax for using the exe from a CLI.

```sh
premultiplyalpha.exe [input] [output]
```

Both `[input]` and `[output]` must be absolute paths, relative paths are not accepted and will throw an error.

You can use a directory path for `[input]` to process multiple files in a directory as a batch, but when doing this `[output]` must also be a directory.


### Windows Examples
**Single file process example**
```
premultiplyalpha.exe "C:\Users\UserName\Desktop\image.png" "C:\Users\UserName\Desktop\premultiplied_image.png"
```

**Directory/batch process example**
```
premultiplyalpha.exe "C:\Users\UserName\Desktop\Images\" "C:\Users\UserName\Desktop\ProcessedImages\"
```

### OSX/Linux Examples
**Single file process example**
```
premultiplyalpha.exe "/Users/UserName/Desktop/image.png" "Users/UserName/Desktop/premultiplied_image.png"
```

**Directory/batch process example**
```
premultiplyalpha.exe "/Users/UserName/Desktop/Images/" "/Users/UserName/Desktop/ProcessedImages/"
```

## Third Party Libraries
This application uses [StbImageSharp](https://github.com/StbSharp/StbImageSharp) and [StbImageWriteSharp](https://github.com/StbSharp/StbImageWriteSharp) to process the images and write them back to disk.

## Sponsor On GitHub
[![](https://raw.githubusercontent.com/manbeardgames/monogame-aseprite/gh-pages-develop/static/img/github_sponsor.png)](https://github.com/sponsors/manbeardgames)  
 Hi, my name is Christopher Whitley. I am an indie game developer and game development tool developer. I create tools primary for the MonoGame framework. All of the tools I develop are released as free and open-sourced software (FOSS), just like this tool.

 If you'd like to buy me a cup of coffee or just sponsor me and my projects in general, you can do so on [GitHub Sponsors](https://github.com/sponsors/manbeardgames).

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