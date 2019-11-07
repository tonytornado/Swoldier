using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Friend
    {
        public Friend()
        {
        }

        /// <summary>
        /// Standard implementation of a friend object using ID's
        /// </summary>
        /// <param name="actionUserId">The Id of user Initiating the action</param>
        /// <param name="userProfileId">The user's profile Id</param>
        /// <param name="friendId">The friend/connection's profile Id</param>
        /// <param name="friendshipConfirmer">Enum for the Status type</param>
        /// <param name="friendshipStart">Start date for the friendship</param>
        public Friend(int actionUserId, int userProfileId, int friendId, Confirmer friendshipConfirmer, DateTime? friendshipStart)
        {
            ProfileId = userProfileId;
            FriendId = friendId;
            FriendshipConfirmer = friendshipConfirmer;
            FriendshipStart = friendshipStart;
            ActionUserId = actionUserId;
        }

        [Key]
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public ProfileSheet Following { get; set; }

        [Key]
        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public ProfileSheet Follower { get; set; }

        [Display(Name = "Confirm Friends")]
        public Confirmer FriendshipConfirmer { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? FriendshipStart { get; set; }

        /// <summary>
        /// Action id for the user that initiated the action
        /// </summary>
        public int ActionUserId { get; set; }

        /// <summary>
        /// Status of a friendship connection
        /// </summary>
        public enum Confirmer
        {
            Pending,
            Confirmed,
            Declined,
            Blocked
        }
    }
}
