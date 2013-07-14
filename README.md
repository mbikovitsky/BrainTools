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
    bftools <operation> <args>

This utility __saves__ images in PNG format, so watch your extensions.
This utility __reads__ images in all formats supported by the .NET framework.

__Note: By default, this utility outputs to `stdout`. *Piping is supported!*__

__Note: The `-` character stands for `stdin`.__

### Encoding
    bftools encode brainfuck   <file | -> [--output <output file>]
    bftools encode brainloller <file | -> --width <width> [--output <output image>]
    bftools encode braincopter <file | -> --original <original image> [--output <output image>]

### Decoding
    bftools decode brainloller <image | -> [--output <output image>]
    bftools decode braincopter <image | -> [--output <output image>]

### Running
    bftools run <program | ->

### Image operations
    bftools enlarge <image | -> <factor> [--output <output image>]
    bftools reduce  <image | -> <factor> [--output <output image>]

### Getting help
    bftools help

Further reading
----------------
- [Brainfuck](http://esolangs.org/wiki/Brainfuck)
- [Brainloller](http://esolangs.org/wiki/Brainloller)
- [Braincopter](http://esolangs.org/wiki/Braincopter)
