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
        /// <param name="_context">DBContext used</param>
        /// <param name="UserId">User's Profile Id</param>
        /// <returns>List</returns>
        public static List<Friend> GetFriendList(OCFXContext _context,
                                         int UserId )
        {
            // Get ALL the friends!
            List<Friend> friend = _context.Friends
                            .Where(b => b.Follower.FitUser.ProfileId == UserId && b.FriendshipConfirmer == Friend.Confirmer.Confirmed)
                            .Include(f => f.Follower)
                                .ThenInclude(p => p.Photos)
                            .Include(f => f.Following)
                                .ThenInclude(p => p.Photos)
                            .ToList();

            Random random = new Random();

            List<Friend> friendos = friend.OrderBy(item => random.Next()).ToList();

            return friendos;
        }

        /// <summary>
        /// Gets a list of friend requests for a specified user
        /// </summary>
        /// <param name="_context">DBContext used</param>
        /// <param name="UserId">User's Profile Id</param>
        /// <returns>List</returns>
        public static async Task<List<Friend>> GetFriendRequestsAsync(OCFXContext _context, int UserId)
        {
            List<Friend> friendRequests = await _context.Friends
                            .Where(c => c.ActionUserId != UserId && c.Follower.FitUser.ProfileId == UserId)
                            .Include(f => f.Following)
                                .ThenInclude(p => p.Photos)
                            .ToListAsync();
            return friendRequests;
        }

        /// <summary>
        /// Generates a new friend connection
        /// </summary>
        /// <param name="_context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void AddFriend(OCFXContext _context, int pitcher, int catcher)
        {
            // Make sure the friend request isn't already happening
            CheckFriend(_context, pitcher, catcher);

            // Process the friend request
            Friend friendRequest = new Friend()
            {
                ActionUserId = pitcher,
                ProfileId = pitcher,
                FriendId = catcher,
                FriendshipStart = DateTime.Now,
                FriendshipConfirmer = Friend.Confirmer.Pending
            };
            _context.Friends.Add(friendRequest);
            _context.SaveChanges();
        }

        /// <summary>
        /// Checks to see if there's already a request in the system.
        /// </summary>
        /// <param name="_context">DB Context</param>
        /// <param name="pitcher"></param>
        /// <param name="catcher"></param>
        private static void CheckFriend(OCFXContext _context, int pitcher, int catcher)
        {
            var check = _context.Friends.FirstOrDefault(c => c.ProfileId == pitcher && c.FriendId == catcher);
            if (check == (default) || check == null)
            {
                return;
            }
            throw new Exception($"Yeah, that ain't happening. I'm shutting this shit down.");
        }

        /// <summary>
        /// Confirms a friend acceptance row
        /// </summary>
        /// <param name="_context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void AcceptFriend(OCFXContext _context, int pitcher, int catcher)
        {
            Friend requestCheck = _context.Friends.SingleOrDefault(f => f.Following.Id == catcher && f.Follower.Id == pitcher && f.ActionUserId == catcher);

            // See if there's another user already trying to request a connection
            if (requestCheck != null)
            {
                Friend personalAcceptance = new Friend()
                {
                    ActionUserId = pitcher,
                    ProfileId = pitcher,
                    FriendId = catcher,
                    FriendshipStart = DateTime.Now,
                    FriendshipConfirmer = Friend.Confirmer.Confirmed
                };

                _context.Friends.Add(personalAcceptance);
                _context.SaveChanges();
            }

            // Update the old request
            requestCheck.FriendshipConfirmer = Friend.Confirmer.Confirmed;
            requestCheck.FriendshipStart = DateTime.Now;
            _context.SaveChanges();

        }

        /// <summary>
        /// Removes a friend connection from the DB
        /// </summary>
        /// <param name="_context">DB Context Association</param>
        /// <param name="pitcher">The sender</param>
        /// <param name="catcher">The receiver</param>
        public static void RemoveFriend(OCFXContext _context, int pitcher, int catcher)
        {
            Friend query = _context.Friends.SingleOrDefault(f => f.Following.Id == pitcher && f.Follower.Id == catcher && f.ActionUserId == pitcher);
            Friend associateQuery = _context.Friends.SingleOrDefault(f => f.Following.Id == catcher && f.Follower.Id == pitcher && f.ActionUserId == catcher);

            _context.Friends.Remove(query);
            _context.Friends.Remove(associateQuery);
            _context.SaveChanges();
        }

        /// <summary>
        /// Blocks a friend connection and banishes them to the shadow realm, unable to be friended again.
        /// </summary>
        /// <param name="_context">DB Context Association</param>
        /// <param name="pitcher">User id for the sender</param>
        /// <param name="catcher">User id for the receiver</param>
        public static void BlockFriend(OCFXContext _context, int pitcher, int catcher)
        {
            Friend query = _context.Friends.SingleOrDefault(f => f.Following.Id == pitcher && f.Follower.Id == catcher);

            if (query != null)
            {
                query.FriendshipConfirmer = Friend.Confirmer.Blocked;
                query.FriendshipStart = DateTime.Now;
            }
            _context.SaveChanges();
        }
    }
}
