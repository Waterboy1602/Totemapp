//
// Main.cs
//
// Author:
//       Scouts en Gidsen Vlaanderen <informatica@scoutsengidsenvlaanderen.be>
//
// Copyright (c) 2016 SGV
//
using UIKit;

namespace TotemAppIos
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
