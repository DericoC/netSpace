using System;
using NetSpace.Model;

namespace NetSpace.Util
{
	public sealed class UserSession
	{
		private static UserSession _singleton;
		private User user;

		public UserSession()
		{
			this.user = new User();
		}

		public UserSession(User loggedUser)
		{
			this.user = loggedUser;
		}

		public static UserSession getSession()
		{
			if (_singleton == null)
			{
				_singleton = new UserSession();
			}
			return _singleton;
		}

		public void reset()
		{
			_singleton = null;
			this.user = null;
		}

		public User getUser()
		{
			return user;
		}

		public void setUser(User u)
		{
			this.user = u;
		}

	}
}

