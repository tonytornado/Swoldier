using OCFX.DataModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.Data.DataModels.SocialModels
{
	public class Friend
	{
		[Key]
		public int ProfileId { get; set; }
		[ForeignKey("ProfileId")]
		public Profile Following { get; set; }

		[Key]
		public int FriendId { get; set; }
		[ForeignKey("FriendId")]
		public Profile Follower { get; set; }

		[Display(Name = "Confirm Friends")]
		public Confirmer FriendshipConfirmer { get; set; }

		[Display(Name = "Date Added")]
		public DateTime? FriendshipStart { get; set; }

		// Action id for the user that initiated the action
		public int ActionUserId { get; set; }

		public enum Confirmer
		{
			Pending = 0,
			Confirmed = 1,
			Declined = 2,
			Blocked = 3
		}
	}
}
