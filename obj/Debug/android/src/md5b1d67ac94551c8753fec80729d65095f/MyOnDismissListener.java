package md5b1d67ac94551c8753fec80729d65095f;


public class MyOnDismissListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.DialogInterface.OnDismissListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDismiss:(Landroid/content/DialogInterface;)V:GetOnDismiss_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnDismissListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Totem.MyOnDismissListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyOnDismissListener.class, __md_methods);
	}


	public MyOnDismissListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyOnDismissListener.class)
			mono.android.TypeManager.Activate ("Totem.MyOnDismissListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MyOnDismissListener (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyOnDismissListener.class)
			mono.android.TypeManager.Activate ("Totem.MyOnDismissListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onDismiss (android.content.DialogInterface p0)
	{
		n_onDismiss (p0);
	}

	private native void n_onDismiss (android.content.DialogInterface p0);

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
