using System;
using Android.Views.InputMethods;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Animation;
using System.Collections.Generic;
using Android.Views.Animations;
using System.Threading.Tasks;
using System.Threading;

namespace Totem {
	public class MyOnCheckBoxClickListener {
		
		//WeakReference to context to avoid memory leak
		private WeakReference<Context> mContext;

		public MyOnCheckBoxClickListener(Context context) {
			mContext = new WeakReference<Context>(context);
		}

		//hides keyboard when checkbox is clicked
		public void OnCheckboxClicked() {
			Context context = null;
			mContext.TryGetTarget(out context);
			KeyboardHelper.HideKeyboard (context);
		}

		public void UpdateCounter(List<Eigenschap> eigenschapList)  {
			Context context = null;
			mContext.TryGetTarget(out context);
			TextView count = ((Activity)context).FindViewById<TextView> (Resource.Id.selected);
			RelativeLayout bottomBar = ((Activity)context).FindViewById<RelativeLayout> (Resource.Id.bottomBar);
			int counter = CountCheckedItems(eigenschapList);
			if (counter > 0 && eigenschapList.Count == 324) {
				bottomBar.Visibility = ViewStates.Visible;

				//animation
				/*if(bottomBar.Visibility == ViewStates.Gone) {
					TranslateAnimation animate = new TranslateAnimation (0, 0, bottomBar.Height, 0);
					animate.Duration = 250;
					bottomBar.StartAnimation (animate);
					Task.Factory.StartNew(() => Thread.Sleep(250)).ContinueWith((t) => {
						bottomBar.Visibility = ViewStates.Visible;
					}, TaskScheduler.FromCurrentSynchronizationContext());

				}*/
			} else {
				bottomBar.Visibility = ViewStates.Gone;

				//animation
				/*TranslateAnimation animate = new TranslateAnimation (0, 0, 0, bottomBar.Height);
				animate.Duration = 250;
				bottomBar.StartAnimation (animate);
				bottomBar.Visibility = ViewStates.Gone;*/
			}
			count.Text = counter + " geselecteerd";
		}

		private int CountCheckedItems(List<Eigenschap> eigenschapList) {
			int result = 0;
			foreach (Eigenschap e in eigenschapList) {
				if (e.selected)
					result++;
			}
			return result;
		}
		
		private class BottomBarAnimationListener : AnimatorListenerAdapter {
			private View view;

			public BottomBarAnimationListener(View view) {
				this.view = view;
			}

			public override void OnAnimationEnd(Animator animation) {
				base.OnAnimationEnd (animation);
				view.Visibility = ViewStates.Gone;
			}
		}
	}
}