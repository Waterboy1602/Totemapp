## Installing

**Coming soon to a Xamarin Component Store near you!**

If Xamarin Components aren't your thing, then you can just drag the `CRSAlertView.cs` class into your project by hand. CRSAlertView is built using the Unified iOS API.

## Using

CRSAlertView has three main parts that are worth mentioning - the alert view itself, alert actions, and an alert input. The alert view is self-explanatory. Alert actions are little bits of functionality that are associated to button presses on an alert view. The alert input is a case that we needed for Curse Voice Mobile that we decided to extend through this library as well. It allows a user's input of text, and for you as the developer to grab that during an alert action if you so wish. Just add this to the top of any class you want to use the alert in:

```c#
using Curse;
```

#### Alert Actions

We'll start with alert actions. These are pretty self-evident as to what they do, though there are a few properties that you can utilize to offer a cleaner UX through the alert view. First we'll start by creating an action:

```c#
var action = new CRSAlertAction {
	Text = "Cancel",
	Highlighted = true,
	TintColor = UIColor.Cyan,
	DidSelect = (alert) => {
		// Do something here on press
	}
};
```

The first part of an alert action, `Text`, is the button text that is displayed on the CRSAlertView. `Highlighted` is used in conjunction with `TintColor` to denote whether this action should appear bolded and a different color. TintColor is not used if Highlighted == false. Using both of these properties smartly can help aid your users to find the actions that are important much easier than just a set of buttons that all appear with the same weight. The final property, `DidSelect`, is an `Action<CRSAlertView>` that is called whenever that button is selected. By passing the alert view back you can look at and use various properties on it to do anything necessary on selection. **`Text` is the only mandatory property, the others are optional.**

#### Alert View

Now that you've made a couple actions for your alert view, let's make the alert view:

```c#
var action = new CRSAlertAction {
	Text = "Cancel",
	Highlighted = true,
	TintColor = UIColor.Cyan,
	DidSelect = (alert) => {
		// Do something here on press
	}
};
var alertView = new CRSAlertView {
	Title = "Hello World!",
	Message = "This alert actually works :)",
	Image = UIImage.FromBundle("someIcon"),
	Actions = new CRSAlertAction[] { action }
};
alertView.Show();
```

Alright I know it looks easy, but let's walk through everything. `Title` and `Message` are both strings that reference the bolded title and standard message for an alert view. You can pass a `UIImage` into the `Image` property and it will put this image above the title in the resulting CRSAlertView. The UIImageView that is behind the scenes has a TintColor property on it that's set to match the text color, so if you pass in an image that is templated, it will match that color - a nice UI touch for your alert views. `Actions` is just an array of all the actions you want to use for this alert view.

#### Alert Input

Besides the actions, an alert view can also hold an input field for adding a message. Here's how you'd add an input:

```c#
var input = new CRSAlertInput {
	Image = UIImage.FromBundle("someIcon"),
	Placeholder = "Type a Message",
	Text = "Prefill a Message",
	TintColor = UIColor.Cyan,
	OpenAutomatically = true
};
alertView.Input = input;
alertView.Show();
```

The `Image` property is for a small icon next to the text view inside the alert - this also coincides with the `TintColor` property to use a templated image that is a certain color. `Placeholder` and `Text` correspond to the placeholder and prefilled text of the text view. `OpenAutomatically` is a boolean value that determines whether the keyboard opens automatically after the alert is shown or not.


#### Customizing

There are several defaults set statically on the CRSAlertView class that will determine fonts and colors of an alert view. If you don't set these, a neutral UI is shown. But what fun is a component without a little customization? You have control of these properties - and if you set them, we recommend setting these in your app delegate to have app wide functionality.

```c#
// Colors
public static UIColor Tint = UIColor.FromRGB (3, 127, 241);
public static UIColor Background = UIColor.FromRGB(0xf3, 0xf3, 0xf3);
public static UIColor TitleTextColor = UIColor.Black;
public static UIColor MessageTextColor = UIColor.Black;
public static UIColor InputTextColor = UIColor.Black;
public static UIColor ButtonBackground = UIColor.FromRGB (228, 228, 228);
public static UIColor ButtonHighlighted = UIColor.FromRGB (210, 210, 210);
public static UIColor SeparatorColor = UIColor.FromRGB( 212, 212, 212 );

// Fonts
public static UIFont TitleFont = UIFont.BoldSystemFontOfSize (18f);
public static UIFont MessageFont = UIFont.SystemFontOfSize (14f);
public static UIFont InputFont = UIFont.SystemFontOfSize(14f);
public static UIFont AlertButtonHighlightedFont = UIFont.BoldSystemFontOfSize(16f);
public static UIFont AlertButtonNormalFont = UIFont.SystemFontOfSize(16f);
```

So to change something like the title text color of the alert view to red, just do something like this:

```c#
CRSAlertView.TitleTextColor = UIColor.Red;
```

And that should propagate to all alert views that are created until that property is set again.

#### Showing/Hiding

After you have created an alert view, you can show it like so:

```c#
alertView.Show();
```

Hiding an alert view can be done by either calling `Hide()` on the instance, or by letting the alert actions call it themselves. Any time a user clicks on an alert action the hide function is called as well. **You don't have to manually call Hide() unless you want to do some from another event.**