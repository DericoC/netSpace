using System;
using System.Collections.Generic;

namespace NetSpace.Util
{
	public interface ICRUD<E>
	{
		bool insert(E item);
		bool update(E item);
		bool delete(E item);
		List<E> read();
	}
}

