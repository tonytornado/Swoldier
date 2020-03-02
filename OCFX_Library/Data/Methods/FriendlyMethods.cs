using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Data.Methods
{
    public static class FriendlyMethods
    {

        /// <summary>
        /// Gets a list of friends for a specified user
        /// </summary>
        /// <param name="context">DBContext used</param>
        /// <param name="userId">User's Profile Id</param>
        /// <returns>List</returns>
        public static List<FriendSheet> GetFriendList(OCFXContext context,
                                         int userId )
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            // Get ALL the friends!
            var friend = context.Friends
                            .Where(b => b.Follower.FitUser.ProfileId == userId && b.FriendshipConfirmer == FriendSheet.Confirmer.Confirmed)
                            .Include(f => f.Follower)
                                .ThenInclude(p => p.Photos)
                            .Include(f => f.Following)
                                .ThenInclude(p => p.Photos)
                            .ToList();

            Random random = new Random();

            List<FriendSheet> friendos = friend.OrderBy(item => random.Next()).ToList();

            return friendos;
        }

        /// <summary>
        /// Gets a list of friend requests for a specified user
        /// </summary>
        /// <param name="context">DBContext used</param>
        /// <param name="userId">User's Profile Id</param>
        /// <returns>List</returns>
        public static async Task<List<FriendSheet>> GetFriendRequestsAsync(OCFXContext context, int userId)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            List<FriendSheet> friendRequests = await context.Friends
                            .Where(c => c.ActionUserId != userId && c.Follower.FitUser.ProfileId == userId)
                            .Include(f => f.Following)
                                .ThenInclude(p => p.Photos)
                            .ToListAsync();
            return friendRequests;
        }

        /// <summary>
        /// Generates a new friend connection
        /// </summary>
        /// <param name="context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void AddFriend(OCFXContext context, int pitcher, int catcher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            // Make sure the friend request isn't already happening
            CheckFriend(context, pitcher, catcher);

            // Process the friend request
            var friendRequest = new FriendSheet()
            {
                ActionUserId = pitcher,
                ProfileId = pitcher,
                FriendId = catcher,
                FriendshipStart = DateTime.Now,
                FriendshipConfirmer = FriendSheet.Confirmer.Pending
            };
            context.Friends.Add(friendRequest);
            context.SaveChanges();
        }

        /// <summary>
        /// Checks to see if there's already a request in the system.
        /// </summary>
        /// <param name="context">DB Context</param>
        /// <param name="pitcher"></param>
        /// <param name="catcher"></param>
        private static void CheckFriend(OCFXContext context, int pitcher, int catcher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var check = context.Friends.FirstOrDefault(c => c.ProfileId == pitcher && c.FriendId == catcher);
            if (check == default)
            {
                return;
            }
            throw new Exception($"Yeah, that ain't happening. I'm shutting this shit down.");
        }

        /// <summary>
        /// Confirms a friend acceptance row
        /// </summary>
        /// <param name="context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void AcceptFriend(OCFXContext context, int pitcher, int catcher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            FriendSheet requestCheck = context.Friends.SingleOrDefault(f => f.Following.Id == catcher && f.Follower.Id == pitcher && f.ActionUserId == catcher);

            // See if there's another user already trying to request a connection
            if (requestCheck != null)
            {
                FriendSheet personalAcceptance = new FriendSheet()
                {
                    ActionUserId = pitcher,
                    ProfileId = pitcher,
                    FriendId = catcher,
                    FriendshipStart = DateTime.Now,
                    FriendshipConfirmer = FriendSheet.Confirmer.Confirmed
                };

                context.Friends.Add(personalAcceptance);
                context.SaveChanges();
            }

            // Update the old request
            if (requestCheck != null)
            {
                requestCheck.FriendshipConfirmer = FriendSheet.Confirmer.Confirmed;
                requestCheck.FriendshipStart = DateTime.Now;
            }

            context.SaveChanges();

        }

        /// <summary>
        /// Removes a friend connection from the DB
        /// </summary>
        /// <param name="context">DB Context Association</param>
        /// <param name="pitcher">The sender</param>
        /// <param name="catcher">The receiver</param>
        public static void RemoveFriend(OCFXContext context, int pitcher, int catcher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            FriendSheet query = context.Friends.SingleOrDefault(f => f.Following.Id == pitcher && f.Follower.Id == catcher && f.ActionUserId == pitcher);
            FriendSheet associateQuery = context.Friends.SingleOrDefault(f => f.Following.Id == catcher && f.Follower.Id == pitcher && f.ActionUserId == catcher);

            context.Friends.Remove(query ?? throw new Exception("What friend?"));
            context.Friends.Remove(associateQuery ?? throw new Exception("Again, what friend?"));
            context.SaveChanges();
        }

        /// <summary>
        /// Blocks a friend connection and banishes them to the shadow realm, unable to be friended again.
        /// </summary>
        /// <param name="context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void BlockFriend(OCFXContext context, int pitcher, int catcher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            FriendSheet query = context.Friends.SingleOrDefault(f => f.Following.Id == pitcher && f.Follower.Id == catcher);

            if (query != null)
            {
                query.FriendshipConfirmer = FriendSheet.Confirmer.Blocked;
                query.FriendshipStart = DateTime.Now;
            }
            context.SaveChanges();
        }
    }
}
