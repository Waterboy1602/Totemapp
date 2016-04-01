package md5b1d67ac94551c8753fec80729d65095f;


public class TinderEigenschappenActivity
	extends md5b1d67ac94551c8753fec80729d65095f.BaseActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Totem.TinderEigenschappenActivity, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TinderEigenschappenActivity.class, __md_methods);
	}


	public TinderEigenschappenActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TinderEigenschappenActivity.class)
			mono.android.TypeManager.Activate ("Totem.TinderEigenschappenActivity, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
