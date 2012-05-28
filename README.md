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
bftools \<operation\> \<language\> \<params\>

This utility __saves__ images in PNG format, so watch your extensions.
This utility __reads__ images in all formats supported by the .NET framework.

### Encoding
bftools encode brainloller \<input file\> \<image width\> \<output image\> <br>
bftools encode braincopter \<input file\> \<original image\> \<output image\>

### Decoding
bftools decode brainloller \<input image\> \<output file\> <br>
bftools decode braincopter \<input image\> \<output file\>

### Running
bftools run \<input file\>

### Image operations
bftools enlarge \<input image\> \<factor\> \<output image\> <br>
bftools reduce \<input image\> \<factor\> \<output image\>

### Getting help
bftools help

Further reading
----------------
- [Brainfuck](http://esolangs.org/wiki/Brainfuck)
- [Brainloller](http://esolangs.org/wiki/Brainloller)
- [Braincopter](http://esolangs.org/wiki/Braincopter)
