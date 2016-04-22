using System;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreAnimation;

namespace TotemAppIos
{
	public class CRSAlertView : UIView
	{
		#region Properties
		const float AnimationDuration = 0.15f;
		const float pad = 20.0f;
		bool _displayOverAlert = true;

		// UI
		UIView _alertContainer;
		UIView _bottomSeparator;
		UIImageView _image;
		UILabel _title;
		UILabel _message;

		// Colors
		public static UIColor Tint = UIColor.FromRGB (3, 127, 241);
		public static UIColor Background = UIColor.FromRGB(0xf3, 0xf3, 0xf3);
		public static UIColor TitleTextColor = UIColor.FromRGB (0, 92, 157);
		public static UIColor MessageTextColor = UIColor.Black;
		public static UIColor InputTextColor = UIColor.Black;
		public static UIColor ButtonBackground = UIColor.FromRGB (228, 228, 228);
		public static UIColor ButtonHighlighted = UIColor.FromRGB (210, 210, 210);
		public static UIColor SeparatorColor = UIColor.FromRGB( 212, 212, 212 );

		// Fonts
		public static UIFont TitleFont = UIFont.FromName ("VerveineW01-Regular", 30f);
		public static UIFont MessageFont = UIFont.SystemFontOfSize (14f);
		public static UIFont InputFont = UIFont.SystemFontOfSize(14f);
		public static UIFont AlertButtonHighlightedFont = UIFont.BoldSystemFontOfSize(16f);
		public static UIFont AlertButtonNormalFont = UIFont.SystemFontOfSize(16f);

		// Input
		UILabel _inputLabel;
		UITextField _inputTextField;
		UIImageView _inputImage;
		UIButton _inputButton;

		string _t;
		public string Title { 
			get { return _t; }
			set {
				_t = value;
				if (_title != null) {
					_title.Text = value;
				}
			}
		}

		string _m;
		public string Message {
			get { return _m; }
			set {
				_m = value;
				if (_message != null) {
					_message.Text = value;
				}
			}
		}

		public UIImage Image { 
			get;
			set;
		}
		public CRSAlertInput Input { get; set; }
		public CRSAlertAction[] Actions { get; set; }
		public bool IsShowing {
			get {
				return Superview != null;
			}
		}

		public static UIWindow AlertWindow;
		static UIWindow PreviousKeyWindow;
		static int AlertsDisplayed;
		#endregion


		#region Constructors
		public CRSAlertView()
		{
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public static CRSAlertView Error(string title, string message, UIImage image = null, string buttonTitle = "Ok")
		{
			var action = new CRSAlertAction {
				Text = buttonTitle,
				Highlighted = true,
				TintColor = Tint
			};
			return new CRSAlertView {
				Title = title,
				Message = message,
				Image = image != null ? image : UIImage.FromBundle(""),
				Actions = new CRSAlertAction[] { action },
			};
		}
		#endregion


		#region UI
		void CreateAlertWindow()
		{
			if (CRSAlertView.AlertWindow == null) {
				CRSAlertView.AlertWindow = new UIWindow (UIScreen.MainScreen.Bounds) {
					BackgroundColor = UIColor.Clear
				};
			}
		}

		private void MakeUI()
		{
			CreateAlertWindow ();

			// Needs these parameters
			if (CRSAlertView.AlertWindow == null || Title == null || Actions == null || Actions.Length == 0) {
				throw new ModelNotImplementedException();
			}

			// Build Main View
			Alpha = 0f;
			BackgroundColor = UIColor.Black.ColorWithAlpha (0.75f);
			Frame = CRSAlertView.AlertWindow.Bounds;

			// Build Container
			nfloat imageWidth = 40f;
			nfloat buttonWidth = (CRSAlertView.AlertWindow.Frame.Width - 2 * pad) / Actions.Length;
			nfloat buttonHeight = 60f;

			_alertContainer = new UIView {
				Frame = new CGRect (pad, 0, CRSAlertView.AlertWindow.Frame.Width - 2 * pad, pad),
				BackgroundColor = Background,
				Alpha = 0f
			};
			_alertContainer.Layer.CornerRadius = 10.0f;
			_alertContainer.Layer.MasksToBounds = true;

			_image = new UIImageView {
				Frame = new CGRect (_alertContainer.Frame.Width/2 - imageWidth/2 - 20 , pad, Image != null ? imageWidth : 0, Image != null ? imageWidth : 0),
				BackgroundColor = UIColor.Clear,
				TintColor = TitleTextColor,
				Image = Image ?? new UIImage(),
				ContentMode = UIViewContentMode.ScaleAspectFit
			};

			_title = new UILabel {
				Frame = new CGRect( _alertContainer.Frame.Width/2 - imageWidth/2 + 20 , pad, Image != null ? imageWidth : 0, Image != null ? imageWidth : 0),
				Text = Title,
				TextColor = TitleTextColor,
				Font = TitleFont,
				Lines = 1,
				AdjustsFontSizeToFitWidth = true,
				MinimumScaleFactor = 0.5f,
				TextAlignment = UITextAlignment.Center
			};

			_message = new UILabel {
				Frame = new CGRect( pad/2, _title.Frame.Bottom + 10, _alertContainer.Frame.Width - pad , 20.0f),
				Text = Message,
				TextColor = MessageTextColor,
				Font = MessageFont,
				Lines = 0,
				TextAlignment = UITextAlignment.Center
			};
			_message.SizeToFit ();
			_message.Center = new CGPoint (_title.Center.X -20 , _message.Center.Y);

			if (Input != null) {
				_inputImage = new UIImageView {
					Frame = new CGRect(pad/2, _message.Frame.Bottom + pad/2, Input.Image != null ? 20 : 0, Input.Image != null ? 20 : 0),
					Image = Input.Image ?? new UIImage(),
					TintColor = Input.TintColor != null ? Input.TintColor : Tint
				};

				var startX = Input.Image != null ? pad / 2 + 30 : pad / 2;
				_inputTextField = new UITextField {
					Frame = new CGRect(startX, _message.Frame.Bottom + pad/2, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
					BackgroundColor = UIColor.White,
					Placeholder = Input.Placeholder != null ? Input.Placeholder : "",
					Text = Input.Text != null ? Input.Text : "",
					TextColor = InputTextColor,
					Font = InputFont,
					BorderStyle = UITextBorderStyle.None,
					Alpha = 0f,
					Delegate = new InputSource(),
					ReturnKeyType = UIReturnKeyType.Done,
					KeyboardAppearance = UIKeyboardAppearance.Dark
				};
				_inputTextField.Layer.SublayerTransform = CATransform3D.MakeTranslation (5.0f, 0.0f, 0.0f);

				_inputLabel = new UILabel {
					Frame = new CGRect(startX, _message.Frame.Bottom + pad/2, _alertContainer.Frame.Width - _inputImage.Frame.Right + 20, 30),
					TextColor = Input.TintColor != null ? Input.TintColor : Tint,
					Text = Input.Placeholder != null ? Input.Placeholder : "",
					Alpha = 0,
					Font = InputFont
				};

				_inputButton = new UIButton {
					BackgroundColor = UIColor.Clear
				};
				_inputButton.TouchUpInside += (sender, e) => {
					ShowInputTextField();
				};

				_alertContainer.AddSubviews (new UIView[] { _inputImage, _inputLabel, _inputButton, _inputTextField });

				if (Input.OpenAutomatically) {
					_inputButton.Hidden = true;
					_inputTextField.Alpha = 1f;
					_inputImage.Center = new CGPoint (_inputImage.Center.X, _inputTextField.Center.Y);
				} else {
					_inputLabel.SizeToFit ();
					_inputLabel.Alpha = 1f;
					nfloat width = _inputImage.Frame.Width + 10 + _inputLabel.Frame.Width;
					_inputImage.Frame = new CGRect (_alertContainer.Frame.Width / 2 - width / 2, _inputImage.Frame.Top, _inputImage.Frame.Width, _inputImage.Frame.Height);
					_inputLabel.Frame = new CGRect (_inputImage.Frame.Right + 10, _inputLabel.Frame.Top, _inputLabel.Frame.Width, _inputLabel.Frame.Height);
					_inputImage.Center = new CGPoint (_inputImage.Center.X, _inputLabel.Center.Y);
					_inputButton.Frame = new CGRect (_inputImage.Frame.Left, _inputLabel.Frame.Top, width, 44);
					_inputButton.Center = new CGPoint (_inputButton.Center.X, _inputLabel.Center.Y);
				}
			}

			_bottomSeparator = new UIView {
				Frame = new CGRect(0, Input == null ? _message.Frame.Bottom + pad : _inputLabel.Frame.Bottom + pad, _alertContainer.Frame.Width, 1),
				BackgroundColor = SeparatorColor
			};
			_alertContainer.AddSubviews (new UIView[] { _image, _title, _message, _bottomSeparator });

			for (int i = 0; i < Actions.Length; i++) {
				CRSAlertAction action = Actions [i];
				var btn = new UIButton {
					Frame = new CGRect(buttonWidth*i, _bottomSeparator.Frame.Bottom, buttonWidth, buttonHeight),
					BackgroundColor = ButtonBackground,
					Font = action.Highlighted ? AlertButtonHighlightedFont : AlertButtonNormalFont
				};
				btn.ContentMode = UIViewContentMode.ScaleAspectFit;
				btn.SetTitle (string.IsNullOrEmpty (action.Text) ? "" : action.Text, UIControlState.Normal);
				btn.Tag = i;
				btn.TouchDown += (sender, e) => {
					btn.BackgroundColor = ButtonHighlighted;
				};
				btn.TouchUpOutside += (sender, e) => {
					btn.BackgroundColor = ButtonBackground;
				};
				btn.TouchUpInside += (sender, e) => { 
					DidSelectAction((int)btn.Tag);
				};
				btn.SetTitleColor (action.Highlighted ? (action.TintColor ?? Tint) : TitleTextColor, UIControlState.Normal);
				_alertContainer.Add (btn);
				if (i < Actions.Length - 1) {
					var s = new UIView {
						Frame = new CGRect(btn.Frame.Right - 1, btn.Frame.Top, 1, buttonHeight),
						BackgroundColor = SeparatorColor
					};
					_alertContainer.Add (s);
				}
			}
			nfloat alertEnd = (_alertContainer.Subviews[_alertContainer.Subviews.Length - 1] as UIView).Frame.Bottom;
			_alertContainer.Frame = new CGRect (_alertContainer.Frame.Left, _alertContainer.Frame.Top, _alertContainer.Frame.Width, alertEnd);
			_alertContainer.Center = new CGPoint (CRSAlertView.AlertWindow.Frame.Width / 2, CRSAlertView.AlertWindow.Frame.Height / 2);
			Add (_alertContainer);
		}
		#endregion


		#region Showing/Hiding
		public void Show(float duration = AnimationDuration*2)
		{
			if( !_displayOverAlert && AlertsDisplayed > 0 )
			{
				return;
			}

			if (_alertContainer == null) {
				MakeUI ();
			}

			CRSAlertView.PreviousKeyWindow = UIApplication.SharedApplication.KeyWindow;
			CRSAlertView.PreviousKeyWindow.EndEditing (true);

			CRSAlertView.AlertWindow.Alpha = 0f;
			Alpha = 0;
			_alertContainer.Alpha = 0f;
			CRSAlertView.AlertWindow.RootViewController = new AlertViewController (this);
			CRSAlertView.AlertWindow.MakeKeyAndVisible ();
			UIView.Animate (duration/2, () => {
				CRSAlertView.AlertWindow.Alpha = 1f;
				Alpha = 1;
			}, () => {
				if (UIApplication.SharedApplication.KeyWindow != CRSAlertView.AlertWindow) {
					CRSAlertView.AlertWindow.MakeKeyAndVisible();
				}
				UIView.Animate(duration/2, () => {
					_alertContainer.Alpha = 1f;
				}, () => {
					if (Input != null && Input.OpenAutomatically) {
						_inputTextField.BecomeFirstResponder ();
					}
				});
			});
		}

		public void Hide(Action<CRSAlertView> didHide = null, float duration = AnimationDuration, UIWindow window = null)
		{
			--AlertsDisplayed;

			if (CRSAlertView.AlertWindow == null) {
				return;
			}

			if (_inputTextField != null && _inputTextField.IsFirstResponder) {
				_alertContainer.EndEditing (true);
			}
			UIView.Animate (AnimationDuration, () => {
				Alpha = 0f;
				CRSAlertView.AlertWindow.Alpha = 0f;
			}, () => {
				CRSAlertView.AlertWindow.RootViewController = new UIViewController();
				if( CRSAlertView.PreviousKeyWindow == null || CRSAlertView.PreviousKeyWindow.Hidden ) {
					window = window ?? UIApplication.SharedApplication.KeyWindow;
					window.MakeKeyAndVisible();
				}
				else {
					CRSAlertView.PreviousKeyWindow.MakeKeyAndVisible();
				}

				if( didHide != null ) didHide( this );
			});
		}

		private void ShowInputTextField()
		{
			_inputButton.Hidden = true;
			UIView.Animate (AnimationDuration, () => {
				_inputLabel.Alpha = 0f;
				_inputImage.Frame = new CGRect (pad/2, _inputImage.Frame.Top, _inputImage.Frame.Width, _inputImage.Frame.Height);
				_inputImage.Center = new CGPoint (_inputImage.Center.X, _inputTextField.Center.Y);
			}, () => {
				_inputTextField.Alpha = 1f;
				_inputTextField.BecomeFirstResponder();
			});
		}
		#endregion


		#region Did Select
		public void DidSelectAction(int i)
		{
			if (Input != null) {
				Input.Text = _inputTextField.Text;
			}

			Hide (Actions[i].DidSelect);
		}
		#endregion


		#region TextFieldSource
		class InputSource : UITextFieldDelegate
		{
			public InputSource(){}

			public override bool ShouldReturn(UITextField textField)
			{
				textField.ResignFirstResponder ();
				return true;
			}
		}
		#endregion


		#region Keyboard Notifications
		private void OnKeyboardNotification (NSNotification notification)
		{
			//Check if the keyboard is becoming visible
			bool visible = notification.Name == UIKeyboard.WillShowNotification;

			//Start an animation, using values from the keyboard
			UIView.BeginAnimations ("AnimateForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (UIKeyboard.AnimationDurationFromNotification (notification));
			UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (notification));

			//Pass the notification, calculating keyboard height, etc.
			if (visible) {
				var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);
				OnKeyboardChanged (visible, keyboardFrame.Height);
			} else {
				var keyboardFrame = UIKeyboard.FrameBeginFromNotification (notification);
				OnKeyboardChanged (visible, keyboardFrame.Height);
			}

			//Commit the animation
			UIView.CommitAnimations ();
		}

		protected void OnKeyboardChanged (bool visible, nfloat height)
		{
			CreateAlertWindow ();

			if( _alertContainer == null ) return;

			if (visible) {
				_alertContainer.Frame = new CGRect (_alertContainer.Frame.Left, CRSAlertView.AlertWindow.Frame.Height - height - pad - _alertContainer.Frame.Height, _alertContainer.Frame.Width, _alertContainer.Frame.Height);
			} else {
				_alertContainer.Center = new CGPoint (CRSAlertView.AlertWindow.Frame.Width / 2, CRSAlertView.AlertWindow.Frame.Height / 2);
			}
		}
		#endregion


		#region View Controller
		class AlertViewController : UIViewController
		{
			readonly CRSAlertView _alert;

			public AlertViewController(CRSAlertView alert)
			{
				_alert = alert;
			}

			public override void ViewDidLoad()
			{
				base.ViewDidLoad ();
				View.AddSubview(_alert);
			}

			public override UIStatusBarStyle PreferredStatusBarStyle () {
				return UIStatusBarStyle.LightContent;
			}
		}
		#endregion
	}


	#region Alert Action
	public class CRSAlertAction 
	{
		public string Text { get; set; }
		public bool Highlighted { get; set; }
		public UIColor TintColor { get; set; }
		public Action<CRSAlertView> DidSelect { get; set; }
	}
	#endregion


	#region Alert Input
	public class CRSAlertInput
	{
		public UIImage Image { get; set; }
		public string Placeholder { get; set; }
		public string Text { get; set; }
		public UIColor TintColor { get; set; }
		public bool OpenAutomatically { get; set; }
	}
	#endregion
}