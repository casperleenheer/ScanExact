<link href="http://kevinburke.bitbucket.org/markdowncss/markdown.css" rel="stylesheet"></link>

iCopy 1.6
=========

iCopy is a free and open source software that lets you combine your scanner and printer into a powerful, 
but easy to use photocopier by only pressing a button.Its simple user interface lets you manage scanner 
and printer options, like brightness, contrast, number of copies in a couple of seconds. Automatic Document Feeders (ADF)
and duplex scanners are supported. You can also save the acquired image to file or to PDF.
As it's small and no installation is required, iCopy is also suitable for USB pen drives.

Website: <http://icopy.sourceforge.net>

1. Features
2. Requirements
3. Command line parameters
4. Known bugs & solutions
5. Support
6. Please help!

1. Features
---------------------------

* Simple and quick interface
* Scanning mode selection
* Run by pressing scanner button
* Resolution, brightness and contrast, scaling settings
* Scan Multiple Pages before printing, including ADF support
* Scan duplex (with compatible scanners)
* Scan to file function
* Scan to PDF with no external software
* Preview function
* Command-line parameters
* No installation needed and little hard disk space required
* Compatible with all WIA scanners and all printers

2. System requirements
--------------------------
* Windows XP SP1/Vista/7/8
* Microsoft .NET Framework 2.0 or higher
* A WIA (Windows Image Acquisition) compatible scanner

> NOTE: Some older scanners are not compatible with WIA. If you have troubles with your scanner, please report it 
> to iCopy bug tracker.
> A list of the known compatible scanners can be found at <http://icopy.sourceforge.net/?page_id=174>

3. Command line parameters
--------------------------
iCopy can be run as a Windows application, but its functionality can entirely be controlled from the command line.

* No parameters: standard mode with windows

		icopy.exe [/copy /file /multiplePages ..] [params]

* [params] are the parameters to be used. If a parameter is not specified, iCopy uses the values stored in settings

		/copy /c				Directly copy from scanner to printer, using settings provided or default settings
		/file /ScanToFile /f	Scan to a file. If file path is not provided a dialog will let you choose where to save the acquired image
		/copyMultiplePages		Scan a multipage document. Also /copymultiplepages
		/pdf					Scans to PDF. Output path can be specified with the /path parameter
	
* Parameters:

		/adf							Enable ADF support
		/duplex							Enable duplex ADF
		/resolution -or- /r	[value]		Specify a valid scanning resolution in DPI (eg /resolution 100, /r 500)
		/color -or- /col				Color acquisition
		/grayscale -or- /gray			Grayscale acquisition
		/text -or- /bw					Black and white (text) acquisition
		/copies -or- /nc [value]		The number of copies to be printed (default one copy)
		/scaling -or- /s				The scaling percentage (eg /s 150) default value: 100
		/brightness	-or- /b [value]		Value from -100 to 100 for brightness
		/contrast -or -	/cnt [value]	Value from -100 to 100 for constrast
		/preview -or- /p				Enables preview mode
		/path "path"					Specify the path for file acquisition. Paths containing spaces must be put between inverted commas 
										(eg. /path "C:\my folder\file.jpg"). Valid file estensions are .jpg, .bmp, .gif, .png, or .pdf for PDF docs
		/printer "printer name"			Specify the name of the printer between inverted commas. If not provided, the system default printer is used

> NOTE: Parameters are not case sensitive

Examples:

	icopy.exe /copy /r 200 /text
		Copy with 200 DPI resolution in text mode, with default brightness and contrast
	icopy.exe /file /brightness 0 /contrast 10 /path "C:\my folder\file.jpg"
		Saves to file "C:\my folder\file.jpg" with brighness 0 and contrast 10
	icopy.exe /copy /printer "Adobe PDF"
		Prints the file to PDF using Adobe Acrobat (if installed)

* Advanced parameters

		/silent				No message boxes.
		/wiareg /wr			Registers WIA components. Use if WIA errors are thrown during execution.
		/register /reg		Registers iCopy to the scanner buttons.
		/unregister /unreg	Unregisters iCopy from the scanner button applications.

4. Known errors & solutions 
-------------------------
### Wiaaut.dll not registered error	
iCopy asks if you want to register WIA Automation Layer. If it fails in doing so, you can register it manually:

* Copy the file wiaaut.dll that you find in iCopy directory to C:\Windows\system32\
* Open a Prompt command with administrator rights
* Enter commands:		

		cd C:\Windows\system32\		
		regsvr32 wiaaut.dll
		
* You should receive a confirmation of wiaaut.dll registration	
* Now iCopy should run as expected.

### Scanner isn't recognized by iCopy
If you think that your scanner is WIA compatible but it isn't recognized by iCopy, please [file a bug report](https://sourceforge.net/tracker/?func=add&group_id=201245&atid=976783).
Please attach the .log file that is created either in C:\Users\Your Username\AppData\Local\iCopy\ or in iCopy executable folder.

A list of the known compatible scanners can be found at <http://icopy.sourceforge.net/?page_id=174>

5. Support
-------------------------
Please read the F.A.Q.:
<http://icopy.sourceforge.net/?page_id=10>

If you want to report an exception or a bug, to suggest a new feature or a change, or ask for support, 
please use Sourceforge Bug tracker:

<https://sourceforge.net/tracker/?func=add&group_id=201245&atid=976783>

6. Please Help!
------------------------
iCopy is free software, so it is supported only by your generous donations and by advertisements on the website.
If you like iCopy, please help me to make it a better software! You can:

* Report problems, tell me your ideas or let me know what you like or don't like in iCopy.
* Tell your friends about iCopy, on your blog, on Facebook, Twitter or wherever you like, so that it gets 
more and more famous.
* Surf the website, leave comments, and maybe give a look (and a click) at the advertisements :-).
* Donate a small amount (even a few $) with Paypal: <http://sourceforge.net/donate/index.php?group_id=201245>.