package md52001c78d29a6163797046cd159e44dcb;


public class clsGlobal
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AbasScanningApp.clsGlobal, AbasScanningApp", clsGlobal.class, __md_methods);
	}


	public clsGlobal ()
	{
		super ();
		if (getClass () == clsGlobal.class)
			mono.android.TypeManager.Activate ("AbasScanningApp.clsGlobal, AbasScanningApp", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
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
