package crc64afd14e0b086eb550;


public class Movimentacoes
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("APPHotel.Movimentacoes, AppHotel", Movimentacoes.class, __md_methods);
	}


	public Movimentacoes ()
	{
		super ();
		if (getClass () == Movimentacoes.class)
			mono.android.TypeManager.Activate ("APPHotel.Movimentacoes, AppHotel", "", this, new java.lang.Object[] {  });
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
