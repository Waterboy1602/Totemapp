package md5b1d67ac94551c8753fec80729d65095f;


public class TipDialog
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Totem.TipDialog, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TipDialog.class, __md_methods);
	}


	public TipDialog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TipDialog.class)
			mono.android.TypeManager.Activate ("Totem.TipDialog, Totem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

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
