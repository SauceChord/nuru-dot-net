<img width="100%" src="nuru-logo.png" alt="nuru dot net"> 

A C# .NET implementation of [domsson's nuru file format](https://github.com/domsson/nuru).

# Current state

Loads ASCII and Unicode images. Supports ANSI4, ANSI8 and TrueColor palette lookups. Metadata too. However does not yet provide a clean way to access ANSI4 and ANSI8 raw values. If you want to use them, you could look them up using the NUIFile and NUPFile (in case the NUIFile has a color palette mode and the NUPFile is providing ANSI8 codes).

# Submodules

This repository uses submodules, so after you clone, use the following commands to get the submodules.

    git submodule init
    git submodule update