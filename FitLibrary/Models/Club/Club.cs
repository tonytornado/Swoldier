using System;
using System.Collections.Generic;
using System.Text;

namespace FitLibrary.Models.Community
{
    public class Club
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ForumBoard Forum { get; set; }
        //public List<ProfileBase> MyProperty { get; set; }
    }

    public class ForumBoard
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<ForumPosts> ForumPosts { get; set; }
    }

    public class ForumPosts
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public int ProfileId { get; set; }
        //public int MyProperty { get; set; }
    }
}
