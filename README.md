BrainTools
===========

What is it?
------------
An utility for working with Brainfuck and related languages. It can encode and decode programs in various
formats.

What works?
------------
EVERYTHING! You can run Brainfuck programs and encode/decode Brainfuck code using Brainloller
and Braincopter. Plus, given that Brainloller produces very small images, the utility features
an image resizer.

Usage
------
bftools \<operation\> \<args\>

This utility __saves__ images in PNG format, so watch your extensions.
This utility __reads__ images in all formats supported by the .NET framework.

__Note: By default, this utility outputs to stdout.__

### Encoding
bftools encode --lang brainfuck   --file \<file\> [--output \<output file\>] <br>
bftools encode --lang brainloller --file \<file\> --width \<width\> [--output \<output image\>] <br>
bftools encode --lang braincopter --file \<file\> --original \<original image> [--output \<output image\>]

### Decoding
bftools decode --lang brainloller --image \<input image\> [--output \<output image\>] <br>
bftools decode --lang braincopter --image \<input image\> [--output \<output image\>]

### Running
bftools run --file \<input file\>

### Image operations
bftools enlarge --image \<input image\> --factor \<factor\> [--output \<output image\>] <br>
bftools reduce  --image \<input image\> --factor \<factor\> [--output \<output image\>]

### Getting help
bftools help

Further reading
----------------
- [Brainfuck](http://esolangs.org/wiki/Brainfuck)
- [Brainloller](http://esolangs.org/wiki/Brainloller)
- [Braincopter](http://esolangs.org/wiki/Braincopter)
