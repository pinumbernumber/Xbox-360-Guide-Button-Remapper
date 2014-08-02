#Xbox 360 Guide Button Remapper v 0.1

###By Jacob Coddaire

Xbox 360 Guide Button Remapper is a lightweight
Windows application that runs completely in the 
background. It stays out of your way and listens
for you to press the Xbox 360 Guide / Home button.

When you do press that button, the shortcut of your 
choice executes. This can be set by you, and can include
a single button (such as Escape) or a key combination
(such as ALT F4).

The minimal design is perfect for those who want to
simply "set it and forget it".

Please see the Known Issues section before downloading.


##System Requirements
Windows 7, 8, or 8.1. 

Some hard drive space.

Some RAM.

Some willpower.

##Setup Instructions
1. Download the .exe file. (or the source and publish it).
2. Run the installer.
3. Add the program file to your startup folder. For instructions, see this link: http://www.sevenforums.com/tutorials/1401-startup-programs-change.html
4. Run the application.
5. Set your keys.
6. Begin using the app!

##Known Issues
I'm not sure how to use the GitHub issue log, so I'll
keep a list here.

This program doesn't ACTUALLY map to the Guide button (I know, I lied).
It's very much a work in progress.

Microsoft likes to hide this information from its devs.
No idea why, they're normally pretty good about this sort
of stuff.

It currently maps to the Escape (ESC) key. Run it,
set your keys, and whenever you hit the ESC key, the shortcut
will execute.

The idea is to press the Xbox 360 Guide button to trigger
the code. But I have no idea how to detect that.

It is known to listen for the 0x0400 keypress event.

One brilliant sir figured it out and has a working solution.
I have included it in this repository. See the folder
button_on_360_guide_v5 in the root directory. 

The file you are interested in is /src/main.cpp. Line 191.

I would give credit to him / her, but all I know is their
Reddit username. They failed to comment the code with their
identity.

What would be helpful is a brilliant and willing programmer
that knows C / C++ and can figure out how to cleverly integrate
his listen code into my existing solution file.

That would be awesome indeed. 

##Feedback and Bugs
I appreciate any and all feedback! If you would like
a feature to be added, let me know. If you find a bug,
please let me know. I like to kill those as quickly as 
I can.

You can contact me via GitHub at this link:

www.github.com/jcoddaire/

Thank you for your interest!