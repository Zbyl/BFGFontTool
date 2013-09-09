BFGFontTool
===========

Zbyl (zbychs@gmail.com)

Tool for manipulating Doom 3 and Doom 3 BFG Edition fonts.
If you want to create fonts for Doom 3 BFG Edition or Rage you'll probably be better off with id Software's idFont tool from Rage.

This program is meant to help me port Doom 3 BFG font code to The Dark Mod.
It works with cooperation with AngelCode's BMFont (http://www.angelcode.com/products/bmfont/).

At the moment (09-09-2013) Doom 3 BFG fonts created with this tool are mostly ok, but not 100% ok.
Decomposing Doom 3 fonts is still an experimental feature.

	Usage:
		BFGFontTool create-bfg --help
		BFGFontTool decompose-d3 --help
	
		BFGFontTool create-bfg options...
			converts AngelCode BMFont's .fnt plain-text font descriptor into Doom 3 BFG Edition .dat font file
			-h, --help                 		show this message and exit
			  --bm, --bmfont=VALUE   		file name of the input BMFont (i.e. bm=ArialNarrow.fnt)
			  --bfg, --bfgfont[=VALUE]		file name of the output BFG font (default: bfg=48.dat)
	
		BFGFontTool decompose-d3 options...
			decomposes Doom 3's .dat font into BMFont's character descriptions
			-h, --help                 		show this message and exit
			  --d3, --d3font=VALUE   		file name of the input Doom 3 .dat font (i.e. d3=fontimage_48.dat)
			  --d3texs=VALUE         		directory containing font's textures (files like: arial_0_48.dds)
			  --bmfc, --bmfconfig[=VALUE]	file to which to append character descriptions (in BMFont's configuration file format) (default: myConf.bmfc)
			  --imgout[=VALUE]       		directory to which to write character images (default: bmfc file's directory)
			  --lang, --language,    		comma separated list of The Dark Mod's languages to use during font's conversion (default: all except russian)
											available langs: czech,danish,dutch,english,french,german,hungarian,italian,polish,portuguese,slovak,spanish,russian
			  --remap, --remapdir[=VALUE]	directory in which The Dark Mod's remap tables are (like: polish.map)

BFGFontTool requires ImageMagick to decompose Doom 3 fonts.
As of this writing (09-09-2013) it uses Magick.NET-6.8.6.801-Q16-x64-net40-client and therefore you need to have corresponding
ImageMagick package installed (ImageMagick-6.8.6-8-Q16-x86-dll.exe; hmmm... now I think about it shouldn't I install the x64 version?).

