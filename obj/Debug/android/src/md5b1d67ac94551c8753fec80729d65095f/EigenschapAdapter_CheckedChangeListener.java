package md5b1d67ac94551c8753fec80729d65095f;


public class EigenschapAdapter_CheckedChangeListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.widget.CompoundButton.OnCheckedChangeListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCheckedChanged:(Landroid/widget/CompoundButton;Z)V:GetOnCheckedChanged_Landroid_widget_CompoundButton_ZHandler:Android.Widget.CompoundButton/IOnCheckedChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Totem.EigenschapAdapter+CheckedChangeListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EigenschapAdapter_CheckedChangeListener.class, __md_methods);
	}


	public EigenschapAdapter_CheckedChangeListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == EigenschapAdapter_CheckedChangeListener.class)
			mono.android.TypeManager.Activate ("Totem.EigenschapAdapter+CheckedChangeListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public EigenschapAdapter_CheckedChangeListener (android.app.Activity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == EigenschapAdapter_CheckedChangeListener.class)
			mono.android.TypeManager.Activate ("Totem.EigenschapAdapter+CheckedChangeListener, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onCheckedChanged (android.widget.CompoundButton p0, boolean p1)
	{
		n_onCheckedChanged (p0, p1);
	}

	private native void n_onCheckedChanged (android.widget.CompoundButton p0, boolean p1);

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
