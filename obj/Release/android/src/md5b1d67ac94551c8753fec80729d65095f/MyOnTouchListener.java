package md5b1d67ac94551c8753fec80729d65095f;


public class MyOnTouchListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnTouchListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Totem.MyOnTouchListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyOnTouchListener.class, __md_methods);
	}


	public MyOnTouchListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyOnTouchListener.class)
			mono.android.TypeManager.Activate ("Totem.MyOnTouchListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MyOnTouchListener (android.content.Context p0, android.widget.EditText p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyOnTouchListener.class)
			mono.android.TypeManager.Activate ("Totem.MyOnTouchListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Widget.EditText, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onTouch (android.view.View p0, android.view.MotionEvent p1)
	{
		return n_onTouch (p0, p1);
	}

	private native boolean n_onTouch (android.view.View p0, android.view.MotionEvent p1);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
